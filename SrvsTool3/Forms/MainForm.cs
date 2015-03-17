using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using System.Security.Principal;
using HenIT.Security;
using System.Text;

namespace SrvsTool
{
    public partial class MainForm : FadeSnapForm
    {
        #region Private properties
        private System.Windows.Forms.Timer mainTimer = new System.Windows.Forms.Timer();
        private ServiceQueryType serviceQueryType = ServiceQueryType.ServiceProcess;
        private List<ServiceDisplayItem> currentServices = new List<ServiceDisplayItem>();
        private ServiceQueryEngineAsync serviceQueryEngineAsync = new ServiceQueryEngineAsync();
        private string DefaultServiceListFile = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LastSrvsToolList.srvlst";
        private TaskbarManager windowsTaskbar;
        private JumpList jumpList;
        //private string appId = "Services monitor";
        public string StartupServicesListFile { get; set; }
        public string StartUpAction { get; set; }
        public List<string> StartUpActions { get; set; }
        public string StartUpService { get; set; }
        public string StartUpMachine { get; set; }
        #endregion

        #region Constructors
        public MainForm()
        {
            InitializeComponent();
            StartupServicesListFile = "";
            StartUpAction = "";
            StartUpService = "";
            StartUpMachine = "";
            if (CoreHelpers.RunningOnWin7)
            {
                windowsTaskbar = TaskbarManager.Instance;                
            }
        }
        public MainForm(string startupServicesListFile)
        {
            InitializeComponent();
            if (startupServicesListFile.Length > 0)
                Properties.Settings.Default.LastServiceListFile = startupServicesListFile;
            if (CoreHelpers.RunningOnWin7)
            {
                windowsTaskbar = TaskbarManager.Instance;
                // Set the application specific id
                //windowsTaskbar.ApplicationId = appId;
            }
        } 
        #endregion

        #region Events
        #region Form events
        private void Form1_Load(object sender, EventArgs e)
        {
            mainTimer.Enabled = false;
            mainNotifyIcon.Visible = true;
            restartInAdminModeToolStripMenuItem.Visible = !IsAdmin();
            toolStripMenuItemAdminSeparator.Visible = !IsAdmin();
            tsbAdmin.Visible = IsAdmin();

            if (StartupServicesListFile.Length > 0)
                Properties.Settings.Default.LastServiceListFile = StartupServicesListFile;

            if (Properties.Settings.Default.RecentFileList == null)
                Properties.Settings.Default.RecentFileList = new System.Collections.Specialized.StringCollection();
            if ((Properties.Settings.Default.MainWindowLocation.X == 0) && (Properties.Settings.Default.MainWindowLocation.Y == 0))
            {
                this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            }
            else
            {
                this.Location = Properties.Settings.Default.MainWindowLocation;
                this.Size = Properties.Settings.Default.MainWindowSize;
            }

            if ((Properties.Settings.Default.LastServiceListFile.Length == 0) ||
                (!System.IO.File.Exists(Properties.Settings.Default.LastServiceListFile)))
            {
                Properties.Settings.Default.LastServiceListFile = DefaultServiceListFile;
                AddServiceListFileToRecentList(Properties.Settings.Default.LastServiceListFile);
                Properties.Settings.Default.Save();
            }

            SetListWidth();
            AttachEvents();

            //Only start monitoring after load event is done
            DelayExecute.Execute(this, 1000, (MethodInvoker)delegate()
                {
                    if (StartUpService.Length > 0)
                    {
                        Properties.Settings.Default.LastSelectedService = StartUpService;
                        if (StartUpMachine.Length > 0)
                        {
                            Properties.Settings.Default.LastSelectedServiceHost = StartUpMachine;
                        }
                    }

                    LoadServiceListFile(Properties.Settings.Default.LastServiceListFile);
                    Application.DoEvents();
                    Thread.Sleep(1000);

                    try
                    {
                        EnableServiceStateChangeProgress();
                        DisableServiceStateChangeProgress();

                        RefreshSrvsInList();
                        LoadQuickLoadList();
                        //if (StartUpAction.Length > 0)
                        //{
                        //    PerformStartupAction();
                        //}
                    }
                    catch { }

                    try
                    {
                        if (StartUpActions!= null && StartUpActions.Count > 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.TopMost = true;
                                Application.DoEvents();
                                this.TopMost = false;
                            });
                            List<ServiceDisplayItem> startServices = new List<ServiceDisplayItem>();
                            foreach (string sua in StartUpActions)
                            {
                                if (sua.Contains('|'))
                                {
                                    string[] sarr = sua.Split('|');
                                    if (sarr.Length == 3)
                                    {
                                        ServiceDisplayItem sdi = new ServiceDisplayItem();
                                        sdi.NextAction = sarr[0];
                                        sdi.HostName = sarr[1];
                                        sdi.ServiceName = sarr[2];
                                        sdi.Enabled = true;
                                        startServices.Add(sdi);
                                    }
                                }
                            }
                            PerformServicesAction(startServices);
                        }
                    }
                    catch { }
                });
            SnappingEnabled = true;            
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainNotifyIcon.Visible = false;
            SaveAllSettingForClose();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            SetListWidth();
            if (this.WindowState == FormWindowState.Minimized && Properties.Settings.Default.HideOnMinimized)
            {
                HideConsole(sender, e);
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (System.Environment.OSVersion.Version.Major > 6 || (System.Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor == 1))
                {
                    // Path to Windows system folder
                    string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    JumpListCustomCategory jumplistCatTools = new JumpListCustomCategory("Extra Tools");

                    // create a new taskbar jump list for the main window
                    jumpList = JumpList.CreateJumpList();
                    jumpList.AddCustomCategories(jumplistCatTools);

                    // Add tools
                    jumplistCatTools.AddJumpListItems(new JumpListLink(System.IO.Path.Combine(systemFolder, "eventvwr.msc"), "Event viewer")
                    {
                        IconReference = new Microsoft.WindowsAPICodePack.Shell.IconReference(System.IO.Path.Combine(systemFolder, "mmc.exe"), 0)
                    });
                    jumplistCatTools.AddJumpListItems(new JumpListLink(System.IO.Path.Combine(systemFolder, "services.msc"), "Services manager")
                    {
                        IconReference = new Microsoft.WindowsAPICodePack.Shell.IconReference(System.IO.Path.Combine(systemFolder, "mmc.exe"), 0)
                    });

                    jumpList.AddUserTasks(new JumpListSeparator());
                    jumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
                    jumpList.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Menu events
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwServices.Items.Clear();
            Properties.Settings.Default.LastServiceListFile = DefaultServiceListFile;
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogLoad.ShowDialog() == DialogResult.OK)
            {
                LoadServiceListFile(openFileDialogLoad.FileName);
                //UpdateServiceStatusses();
                RefreshSrvsInList();
            }
        }
        private void recentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecentFileList recentFileList = new RecentFileList();
            if (recentFileList.ShowDialog() == DialogResult.OK)
            {
                LoadServiceListFile(recentFileList.SelectedServiceListFile);
                //UpdateServiceStatusses();
                RefreshSrvsInList();
                Properties.Settings.Default.LastServiceListFile = recentFileList.SelectedServiceListFile;
                LoadQuickLoadList();
            }
        }                
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LastServiceListFile != null)
                saveFileDialogSave.FileName = Properties.Settings.Default.LastServiceListFile;
            if (saveFileDialogSave.ShowDialog() == DialogResult.OK)
            {
                SaveServiceListFile(saveFileDialogSave.FileName);
            }
        }
        private void restartInAdminModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!AdminModeTools.IsInAdminMode())
            {
                Properties.Settings.Default.Save();
                AdminModeTools.RestartInAdminMode();    
            }

            //if (!IsAdmin())
            //    RestartInAdminMode("","","");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshSrvsInList();
        }
        private void autoRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTimer.Enabled = autoRefreshToolStripMenuItem.Checked;
        }
        private void servicesMmcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process services = new Process();
            services.StartInfo.FileName = Environment.SystemDirectory + @"\services.msc";
            services.StartInfo.Arguments = "/s";
            services.Start();
        }
        private void eventlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process newProcess = new Process();
            newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo("mmc.exe");
            newProcess.StartInfo.Arguments = System.Environment.SystemDirectory + @"\eventvwr.msc /s";
            newProcess.Start();
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.PollingFrequency = Properties.Settings.Default.MainTimerFrequencySec;
            optionsWindow.HideonMinimized = Properties.Settings.Default.HideOnMinimized;
            if (optionsWindow.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.MainTimerFrequencySec = optionsWindow.PollingFrequency;
                Properties.Settings.Default.HideOnMinimized = optionsWindow.HideonMinimized;
                mainTimer.Interval = Properties.Settings.Default.MainTimerFrequencySec * 1000;
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
        #endregion

        #region Timer events
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            RefreshSrvsInList();
        }
        private void timerQuickload_Tick(object sender, EventArgs e)
        {
            timerQuickload.Enabled = false;
            if (cboQuickLoad.SelectedItem != null)
            {
                Properties.Settings.Default.LastServiceListFile = ((RecentFile)cboQuickLoad.SelectedItem).FilePath;
                LoadServiceListFile(Properties.Settings.Default.LastServiceListFile);
                
                cboQuickLoad.SelectedIndex = 0;

                if (mainTimer.Enabled)
                    DelayExecute.Execute(this, 500, (MethodInvoker)RefreshSrvsInList);
            }
        }  
        #endregion

        #region ListView events
        private void lvwServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvwServices.SelectedItems.Count > 0)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvwServices.SelectedItems[0].Tag;
                    Properties.Settings.Default.LastSelectedService = sdi.ServiceName;
                    Properties.Settings.Default.LastSelectedServiceHost = sdi.HostName;
                    this.Text = "Services monitor - " + Properties.Settings.Default.LastSelectedService;
                }
                SetServiceMenuItemsEnable();
                RefreshNotifyIcon();
                UpdateStatusbarText();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvwServices_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                removeToolStripMenuItem_Click(sender, e);
            }
        }
        #endregion

        #region Context menu events
        private void SetListWidth()
        {
            lvwServices.Columns[0].Width = lvwServices.ClientSize.Width; ;
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool duplicate = false;
            AddServices addServices = new AddServices();
            foreach (ListViewItem lvi in lvwServices.Items)
            {
                addServices.ExcludeList.Add((ServiceDisplayItem)lvi.Tag);
            }
            if (lvwServices.SelectedItems.Count > 0)
            {
                addServices.Machine = ((ServiceDisplayItem)lvwServices.SelectedItems[0].Tag).HostName;
            }
            if (addServices.ShowDialog() == DialogResult.OK)
            {
                foreach (string machineservice in addServices.SelectedServices)
                {
                    string machine = ".";
                    string service = "";
                    if (machineservice.Contains('\\'))
                    {
                        machine = machineservice.Split('\\')[0];
                        service = machineservice.Split('\\')[1];
                    }
                    else
                        service = machineservice;
                    duplicate = false;
                    foreach (ListViewItem lvi in lvwServices.Items)
                    {
                        ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                        if ((sdi.ServiceName == service) && (sdi.HostName == machine))
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (!duplicate)
                    {
                        ServiceDisplayItem newItem = new ServiceDisplayItem();
                        newItem.HostName = machine;
                        newItem.ServiceName = service;
                        newItem.DisplayName = service;
                        newItem.Enabled = true;
                        currentServices.Add(newItem);
                        AddServiceToListView(newItem);
                    }
                }
                UpdateListViewFooters();
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove this item(s)?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in lvwServices.SelectedItems)
                {
                    if (lvi.Tag != null)
                    {
                        ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                        if ((from c in currentServices
                             where c.HostName == sdi.HostName &&
                                                  c.DisplayName == sdi.DisplayName
                             select c).Count() == 1)
                        {
                            currentServices.Remove((from c in currentServices
                                                    where c.HostName == sdi.HostName &&
                                                                         c.DisplayName == sdi.DisplayName
                                                    select c).First());
                        }
                    }
                    
                    lvwServices.Items.Remove(lvi);
                }
                UpdateListViewFooters();
            }
        }
        private void showServiceMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowConsole(sender, e);
        }
        private void EnableServiceStateChangeProgress()
        {
            if (System.Environment.OSVersion.Version.Major > 6 || (System.Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor == 1))
            {
                TaskbarProgressBarState state = TaskbarProgressBarState.Indeterminate;
                windowsTaskbar.SetProgressState(state);
            }
        }
        private void DisableServiceStateChangeProgress()
        {
            if (System.Environment.OSVersion.Version.Major > 6 || (System.Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor == 1))
            {
                TaskbarProgressBarState state = TaskbarProgressBarState.NoProgress;
                windowsTaskbar.SetProgressState(state);
            }
        }
        private void startServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwServices.SelectedItems.Count == 0)
                return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<ServiceDisplayItem> servicesToAction = new List<ServiceDisplayItem>();
                foreach (ListViewItem lvi in lvwServices.SelectedItems)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                    sdi.NextAction = "start";
                    servicesToAction.Add(sdi);
                }

                PerformServicesAction(servicesToAction);
                Application.DoEvents();

//                WaitForServiceChange waitForServiceChange;
//                EnableServiceStateChangeProgress();
//                foreach (ListViewItem lvi in lvwServices.SelectedItems)
//                {
//                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
//                    IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
//                    SetNotifyIcon(Status.Busy, "Starting " + service.DisplayName);

//                    waitForServiceChange = new WaitForServiceChange();
//                    waitForServiceChange.SetWindowSize("Starting: " + service.DisplayName);
//                    waitForServiceChange.Show("Starting service", "Starting: " + service.DisplayName);

//                    if (service.Status == ServiceControllerStatus.Stopped)
//                    {
//                        SetListViewIcon(lvi, Status.Busy);

//                        tsbStatus.Text = "Starting " + service.DisplayName;
//                        Application.DoEvents();
//                        Cursor.Current = Cursors.WaitCursor;
//                        try
//                        {
//                            service.Start();
//                            DateTime begin = DateTime.Now;
//                            while ((service.Status != ServiceControllerStatus.Running) &&
//                                    (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
//                            {
//                                service.Refresh();
//                                Application.DoEvents();
//                                Cursor.Current = Cursors.WaitCursor;
//                                Thread.Sleep(500);
//                            }
//                            SetListViewIcon(lvi, Status.Running);
//                        }
//                        catch (InvalidOperationException iex)
//                        {
//                            try { waitForServiceChange.Close(); }
//                            catch { }
//#if DEBUG
//                            MessageBox.Show(iex.ToString());
//#endif                            
//                            if (iex.Message.Contains("Cannot open"))
//                            {
//                                RestartInAdminMode("start", service.DisplayName, service.MachineName);
//                                return;
//                            }
//                            else
//                                SetListViewIcon(lvi, Status.Unknown);
//                        }
//                        catch (UnauthorizedAccessException)
//                        {
//                            RestartInAdminMode("start", service.DisplayName, service.MachineName);
//                        }
//                        catch (Exception ex)
//                        {
//#if DEBUG
//                            MessageBox.Show(ex.ToString());
//#endif
//                            SetListViewIcon(lvi, Status.Unknown);
//                        }
//                    }
//                    tsbStatus.Text = "Started " + service.DisplayName;
//                    if (waitForServiceChange.Visible)
//                        waitForServiceChange.Close();
//                    waitForServiceChange = null;
//                }
                //UpdateServiceStatusses();
                RefreshSrvsInList();
                SetServiceMenuItemsEnable();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisableServiceStateChangeProgress();
                Cursor.Current = Cursors.Default;
            }
        }
        private void stopServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwServices.SelectedItems.Count == 0)
                return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<ServiceDisplayItem> servicesToAction = new List<ServiceDisplayItem>();
                foreach (ListViewItem lvi in lvwServices.SelectedItems)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                    sdi.NextAction = "stop";
                    servicesToAction.Add(sdi);
                }

                PerformServicesAction(servicesToAction);
                Application.DoEvents();

//            if (lvwServices.SelectedItems.Count == 0)
//                return;
//            try
//            {
//                Cursor.Current = Cursors.WaitCursor;
//                WaitForServiceChange waitForServiceChange;
//                EnableServiceStateChangeProgress();
//                foreach (ListViewItem lvi in lvwServices.SelectedItems)
//                {
//                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
//                    IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
//                    SetNotifyIcon(Status.Busy, "Stopping " + service.DisplayName);
//                    waitForServiceChange = new WaitForServiceChange();
//                    waitForServiceChange.SetWindowSize("Stopping: " + service.DisplayName);
//                    waitForServiceChange.Show("Stopping service", "Stopping: " + service.DisplayName);
//                    //serviceStatusChange = new ServiceStatusChange();
//                    //serviceStatusChange.Show("Stopping service", "Service: " + service.DisplayName);

//                    if (service.Status == ServiceControllerStatus.Running)
//                    {
//                        SetListViewIcon(lvi, Status.Busy);

//                        tsbStatus.Text = "Stopping " + service.DisplayName;
//                        Application.DoEvents();
//                        Cursor.Current = Cursors.WaitCursor;
//                        try
//                        {
//                            service.Stop();
//                            DateTime begin = DateTime.Now;
//                            while ((service.Status != ServiceControllerStatus.Stopped) &&
//                                    (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
//                            {
//                                service.Refresh();
//                                Application.DoEvents();
//                                Cursor.Current = Cursors.WaitCursor;
//                                Thread.Sleep(500);
//                            }
//                            SetListViewIcon(lvi, Status.Stopped);
//                        }
//                        catch (InvalidOperationException iex)
//                        {
//                            try { waitForServiceChange.Close(); }
//                            catch { }
//#if DEBUG
//                            MessageBox.Show(iex.ToString());
//#endif
                           
//                            if (iex.Message.Contains("Cannot open"))
//                            {
//                                RestartInAdminMode("stop", service.DisplayName, service.MachineName);
//                                return;
//                            }
//                            else
//                                SetListViewIcon(lvi, Status.Unknown);
//                        }
//                        catch (UnauthorizedAccessException)
//                        {
//                            RestartInAdminMode("stop", service.DisplayName, service.MachineName);
//                        }
//                        catch (Exception ex)
//                        {
//#if DEBUG
//                            MessageBox.Show(ex.ToString());
//#endif
//                            SetListViewIcon(lvi, Status.Unknown);
//                        }
//                    }
//                    tsbStatus.Text = "Stopped " + service.DisplayName;
//                    if (waitForServiceChange.Visible)
//                        waitForServiceChange.Close();
//                    waitForServiceChange = null;
//                }
                //UpdateServiceStatusses();
                RefreshSrvsInList();
                SetServiceMenuItemsEnable();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisableServiceStateChangeProgress();
                Cursor.Current = Cursors.Default;
            }
        }
        private void restartServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (lvwServices.SelectedItems.Count == 0)
                return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<ServiceDisplayItem> servicesToAction = new List<ServiceDisplayItem>();
                foreach (ListViewItem lvi in lvwServices.SelectedItems)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                    sdi.NextAction = "restart";
                    servicesToAction.Add(sdi);
                }

                PerformServicesAction(servicesToAction);
                Application.DoEvents();


//            if (lvwServices.SelectedItems.Count == 0)
//                return;
//            try
//            {
//                Cursor.Current = Cursors.WaitCursor;
//                WaitForServiceChange waitForServiceChange;
//                EnableServiceStateChangeProgress();
//                foreach (ListViewItem lvi in lvwServices.SelectedItems)
//                {
//                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
//                    IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
//                    SetNotifyIcon(Status.Busy, "Stopping " + service.DisplayName);
//                    waitForServiceChange = new WaitForServiceChange();
//                    waitForServiceChange.SetWindowSize("Stopping: " + service.DisplayName);
//                    waitForServiceChange.Show("Stopping service", "Stopping: " + service.DisplayName);

//                    if (service.Status == ServiceControllerStatus.Running)
//                    {
//                        SetListViewIcon(lvi, Status.Busy);

//                        tsbStatus.Text = "Stopping " + service.DisplayName;
//                        Application.DoEvents();
//                        Cursor.Current = Cursors.WaitCursor;
//                        try
//                        {
//                            service.Stop();
//                            DateTime begin = DateTime.Now;
//                            while ((service.Status != ServiceControllerStatus.Stopped) &&
//                                    (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
//                            {
//                                service.Refresh();
//                                Application.DoEvents();
//                                Cursor.Current = Cursors.WaitCursor;
//                                Thread.Sleep(500);
//                            }
//                            SetListViewIcon(lvi, Status.Stopped);
//                        }
//                        catch (InvalidOperationException iex)
//                        {
//                            try { waitForServiceChange.Close(); }
//                            catch { }
//#if DEBUG
//                            MessageBox.Show(iex.ToString());
//#endif

//                            if (iex.Message.Contains("Cannot open"))
//                            {
//                                RestartInAdminMode("restart", service.DisplayName, service.MachineName);
//                                return;
//                            }
//                            else
//                                SetListViewIcon(lvi, Status.Unknown);
//                        }
//                        catch (UnauthorizedAccessException)
//                        {
//                            RestartInAdminMode("restart", service.DisplayName, service.MachineName);
//                        }
//                        catch
//                        {
//                            SetListViewIcon(lvi, Status.Unknown);
//                        }
//                    }
//                    tsbStatus.Text = "Stopped " + service.DisplayName;
//                    if (waitForServiceChange.Visible)
//                        waitForServiceChange.Close();
//                    waitForServiceChange = null;

//                    Thread.Sleep(3000);
//                    Application.DoEvents();
//                    SetNotifyIcon(Status.Busy, "Starting " + service.DisplayName);
//                    waitForServiceChange = new WaitForServiceChange();
//                    waitForServiceChange.SetWindowSize("Starting: " + service.DisplayName);
//                    waitForServiceChange.Show("Starting service", "Starting: " + service.DisplayName);
//                    if (service.Status == ServiceControllerStatus.Stopped)
//                    {
//                        SetListViewIcon(lvi, Status.Busy);

//                        tsbStatus.Text = "Starting " + service.DisplayName;
//                        Application.DoEvents();
//                        Cursor.Current = Cursors.WaitCursor;
//                        try
//                        {
//                            service.Start();
//                            DateTime begin = DateTime.Now;
//                            while ((service.Status != ServiceControllerStatus.Running) &&
//                                    (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
//                            {
//                                service.Refresh();
//                                Application.DoEvents();
//                                Cursor.Current = Cursors.WaitCursor;
//                                Thread.Sleep(500);
//                            }
//                            SetListViewIcon(lvi, Status.Running);
//                        }
//                        catch (InvalidOperationException iex)
//                        {
//                            try { waitForServiceChange.Close(); }
//                            catch { }
//#if DEBUG
//                            MessageBox.Show(iex.ToString());
//#endif

//                            if (iex.Message.Contains("Cannot open"))
//                            {
//                                RestartInAdminMode("restart", service.DisplayName, service.MachineName);
//                                return;
//                            }
//                            else
//                                SetListViewIcon(lvi, Status.Unknown);
//                        }
//                        catch (UnauthorizedAccessException)
//                        {
//                            RestartInAdminMode("restart", service.DisplayName, service.MachineName);
//                        }
//                        catch
//                        {
//                            SetListViewIcon(lvi, Status.Unknown);
//                        }
//                    }
//                    tsbStatus.Text = "Started " + service.DisplayName;
//                    if (waitForServiceChange.Visible)
//                        waitForServiceChange.Close();
//                    waitForServiceChange = null;
//                }
//                //UpdateServiceStatusses();
                RefreshSrvsInList();
                SetServiceMenuItemsEnable();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisableServiceStateChangeProgress();
                Cursor.Current = Cursors.Default;
            }
        }
        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwServices.SelectedItems)
            {
                if ((from s in currentServices
                     where s.HostName == lvi.Group.Header &&
                        s.DisplayName == lvi.Text
                     select s).Count() == 1)
                {
                    ServiceDisplayItem sdi = (from s in currentServices
                                              where s.HostName == lvi.Group.Header &&
                                                 s.DisplayName == lvi.Text
                                              select s).First();// (ServiceDisplayItem)lvi.Tag;
                    sdi.Enabled = true;
                    lvi.Tag = null;
                    lvi.Tag = sdi;
                    SetListViewIcon(lvi, Status.Unknown);
                }
            }
            SetServiceMenuItemsEnable();
        }
        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwServices.SelectedItems)
            {
                if ((from s in currentServices
                     where s.HostName == lvi.Group.Header &&
                        s.DisplayName == lvi.Text
                     select s).Count() == 1)
                {
                    ServiceDisplayItem sdi = (from s in currentServices
                                              where s.HostName == lvi.Group.Header &&
                                                 s.DisplayName == lvi.Text
                                              select s).First();// (ServiceDisplayItem)lvi.Tag;
                    sdi.Enabled = false;
                    lvi.Tag = null;
                    lvi.Tag = sdi;
                    SetListViewIcon(lvi, Status.Disabled);
                }
            }
            SetServiceMenuItemsEnable();
        }
        private void startAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                WaitForServiceChange waitForServiceChange;
                EnableServiceStateChangeProgress();
                foreach (ListViewItem lvi in lvwServices.Items)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                    if (sdi.Enabled)
                    {
                        IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
                        SetNotifyIcon(Status.Busy, "Starting " + service.DisplayName);

                        waitForServiceChange = new WaitForServiceChange();
                        waitForServiceChange.SetWindowSize("Starting: " + service.DisplayName);
                        waitForServiceChange.Show("Starting service", "Starting: " + service.DisplayName);

                        if (service.Status == ServiceControllerStatus.Stopped)
                        {
                            SetListViewIcon(lvi, Status.Busy);

                            tsbStatus.Text = "Starting " + service.DisplayName;
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                service.Start();
                                DateTime begin = DateTime.Now;
                                while ((service.Status != ServiceControllerStatus.Running) &&
                                        (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                                {
                                    service.Refresh();
                                    Application.DoEvents();
                                    Cursor.Current = Cursors.WaitCursor;
                                    Thread.Sleep(500);
                                }
                                SetListViewIcon(lvi, Status.Running);
                            }
                            catch
                            {
                                SetListViewIcon(lvi, Status.Unknown);
                            }
                        }
                        tsbStatus.Text = "Started " + service.DisplayName;
                        if (waitForServiceChange.Visible)
                            waitForServiceChange.Close();
                        waitForServiceChange = null;
                    }
                }
                //UpdateServiceStatusses();
                RefreshSrvsInList();
                SetServiceMenuItemsEnable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisableServiceStateChangeProgress();
                Cursor.Current = Cursors.Default;
            }
        }
        private void stopAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                WaitForServiceChange waitForServiceChange;
                EnableServiceStateChangeProgress();
                foreach (ListViewItem lvi in lvwServices.Items)
                {
                    ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                    if (sdi.Enabled)
                    {
                        IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
                        SetNotifyIcon(Status.Busy, "Stopping " + service.DisplayName);
                        waitForServiceChange = new WaitForServiceChange();
                        waitForServiceChange.SetWindowSize("Stopping: " + service.DisplayName);
                        waitForServiceChange.Show("Stopping service", "Stopping: " + service.DisplayName);

                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            SetListViewIcon(lvi, Status.Busy);

                            tsbStatus.Text = "Stopping " + service.DisplayName;
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                service.Stop();
                                DateTime begin = DateTime.Now;
                                while ((service.Status != ServiceControllerStatus.Stopped) &&
                                        (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                                {
                                    service.Refresh();
                                    Application.DoEvents();
                                    Cursor.Current = Cursors.WaitCursor;
                                    Thread.Sleep(500);
                                }
                                SetListViewIcon(lvi, Status.Stopped);
                            }
                            catch
                            {
                                SetListViewIcon(lvi, Status.Unknown);
                            }
                        }
                        tsbStatus.Text = "Stopped " + service.DisplayName;
                        if (waitForServiceChange.Visible)
                            waitForServiceChange.Close();
                        waitForServiceChange = null;
                    }
                }
                //UpdateServiceStatusses();
                RefreshSrvsInList();
                SetServiceMenuItemsEnable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisableServiceStateChangeProgress();
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion 

        #region Quickload
        private void cboQuickLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerQuickload.Enabled = false;
            if (cboQuickLoad.SelectedIndex > 0)
                timerQuickload.Enabled = true;
        }
        #endregion
        #endregion

        #region Notify icon stuff
        private void mainNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowConsole(sender, e);
        }
        private void mainNotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.Visible)
                {
                    this.Visible = true;
                    ShowConsole(sender, e);
                }
            }
        }
        private void ShowConsole(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Visible = true;
                Show();
                WindowState = FormWindowState.Normal;
                Focus();
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                Focus();
                this.TopMost = true;
                this.TopMost = false;
            }

            //Only start monitoring after load event is done
            new System.Threading.Thread(delegate()
            {
                lvwServices.Invoke((MethodInvoker)delegate()
                {
                    Application.DoEvents();
                    RefreshSrvsInList();
                }
                );
            }
            ).Start();
        }
        private void HideConsole(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void RefreshNotifyIcon()
        {
            string statusText = "No services selected";
            Status reportStatus = Status.Unknown;
            int totalCount = lvwServices.Items.Count;
            int runningCount = 0;
            int stoppedCount = 0;
            int selectedCount = lvwServices.SelectedItems.Count;            

            if (selectedCount != 1)
            {                
                statusText = "Multiple services selected";

                foreach (ListViewItem lvi in lvwServices.Items)
                {
                    if (lvi.ImageIndex == (int)Status.Running)
                    {
                        if (selectedCount == 0 || lvi.Selected)
                            runningCount++;
                    }
                    else if (lvi.ImageIndex == (int)Status.Stopped)
                    {
                        if (selectedCount == 0 || lvi.Selected)
                            stoppedCount++;
                    }
                }

                if ((runningCount > 0) && (stoppedCount == 0))
                {
                    reportStatus = Status.Running;
                    statusText = "All services running";
                }
                else if ((runningCount > 0) && (stoppedCount > 0))
                {
                    reportStatus = Status.Mix;
                    statusText = "Some services stopped";
                }
                else if ((runningCount == 0) && (stoppedCount > 0))
                {
                    reportStatus = Status.Stopped;
                    statusText = "All services stopped";
                }

                tsbStatus.Text = statusText;
                tsbStatus.ToolTipText = statusText;
                SetNotifyIcon(reportStatus, totalCount.ToString() + " service(s), " + selectedCount.ToString() + " selected, " + runningCount.ToString() + " running, " + stoppedCount.ToString() + " stopped");
            }
            else
            {
                ServiceDisplayItem sdi = (ServiceDisplayItem)lvwServices.SelectedItems[0].Tag;
                if (PingUtil.Ping(sdi.HostName))
                {
                    IServiceControllerEx srv = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
                    if (!sdi.Enabled)
                    {
                        SetNotifyIcon(Status.Disabled, "Monitoring disabled - " + lvwServices.SelectedItems[0].Text);
                        tsbStatus.Text = "Monitoring disabled";
                    }
                    else if (srv.Status == ServiceControllerStatus.Running)
                    {
                        SetNotifyIcon(Status.Running, "Service is running - " + lvwServices.SelectedItems[0].Text);
                        tsbStatus.Text = "Service is running";
                    }
                    else if (srv.Status == ServiceControllerStatus.Stopped)
                    {
                        SetNotifyIcon(Status.Stopped, "Service is stopped - " + lvwServices.SelectedItems[0].Text);
                        tsbStatus.Text = "Service is stopped";
                    }
                    else if (srv.Status == ServiceControllerStatus.Paused)
                    {
                        SetNotifyIcon(Status.Disabled, "Service is paused - " + lvwServices.SelectedItems[0].Text);
                        tsbStatus.Text = "Service is paused";
                    }
                    else
                    {
                        SetNotifyIcon(Status.Busy, "Service is being stopped or started - " + lvwServices.SelectedItems[0].Text);
                        tsbStatus.Text = "Service is being stopped or started";
                    }
                }
                else
                {
                    SetNotifyIcon(Status.Unknown, "Host not pingable - " + lvwServices.SelectedItems[0].Text);
                    tsbStatus.Text = "Host not pingable";
                }
            }
        }
        private void SetNotifyIcon(Status iImage, string theText)
        {
            Icon myIcon;
            try
            {
                if (iImage == Status.Running)
                    myIcon = new Icon(GetType(), "icons.GRunning.ico");
                else if (iImage == Status.Busy)
                    myIcon = new Icon(GetType(), "icons.GBusy.ico");
                else if (iImage == Status.Stopped)
                    myIcon = new Icon(GetType(), "icons.GStopped.ico");
                else if (iImage == Status.Paused)
                    myIcon = new Icon(GetType(), "icons.GPaused.ico");
                else if (iImage == Status.Disabled)
                    myIcon = new Icon(GetType(), "icons.NOSMOKE.ico");
                else if (iImage == Status.Mix)
                    myIcon = new Icon(GetType(), "icons.GMix.ico");
                else
                    myIcon = new Icon(GetType(), "icons.GUnknown.ico");

                //NotifyIcon text must be less than 64 characters long... NotifyIcon limitation
                if (theText.Length >= 61)
                    theText = theText.Substring(0, 60) + "...";
                mainNotifyIcon.Text = theText; 
                mainNotifyIcon.Icon = myIcon;
                if (CoreHelpers.RunningOnWin7)
                {
                    windowsTaskbar.SetOverlayIcon(myIcon, theText);
                }
                else
                    this.Icon = myIcon;
            }
            catch (Exception ex)
            {
                mainNotifyIcon.Text = ex.Message;
            }
        }    
        #endregion

        #region Private methods
        private void RefreshSrvsInList()
        {
            if (!serviceQueryEngineAsync.IsBusy)
            {
                mainTimer.Enabled = false;
                SetIconsToBusyUpdating();                
                Cursor.Current = Cursors.WaitCursor;
                //Call asynchronous refreshing method
                serviceQueryEngineAsync.RefreshServiceListAsync(currentServices);
                tsbStatus.Text = "Refreshing";
            }
        }

        private void SetIconsToBusyUpdating()
        {
            foreach (ListViewItem lvi in lvwServices.Items)
            {
                if (lvi.ImageIndex == 0)
                {
                    lvi.ImageIndex = 5;
                }
                else if (lvi.ImageIndex == 1)
                {
                    lvi.ImageIndex = 6;
                }
                else if (lvi.ImageIndex == 2)
                {
                    lvi.ImageIndex = 7;
                }
                else if (lvi.ImageIndex == 3)
                {
                    lvi.ImageIndex = 8;
                }
                else if (lvi.ImageIndex == 5)
                {
                    lvi.ImageIndex = 9;
                }
            }
            Application.DoEvents();         
        }
        /// <summary>
        /// Update Listview with latest statusses in currentServices
        /// </summary>
        private void UpdateSrvsList()
        {
            try
            {
                lvwServices.BeginUpdate();
                foreach (ListViewGroup grp in lvwServices.Groups)
                {
                    if ((from ListViewItem lviq in grp.Items
                         where lviq.Tag != null &&
                           ((ServiceDisplayItem)lviq.Tag).Enabled
                         select lviq).Count() > 0)
                    {
                        //Loop by each machine
                        foreach (ListViewItem lvi in grp.Items)
                        {
                            try
                            {
                                ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                                ServiceDisplayItem currentItem = null;
                                if (sdi.Enabled)
                                {
                                    if ((from c in currentServices
                                         where c.HostName == sdi.HostName &&
                                              c.DisplayName == sdi.DisplayName
                                         select c).Count() == 1)
                                    {
                                        currentItem = (from c in currentServices
                                                       where c.HostName == sdi.HostName &&
                                                            c.DisplayName == sdi.DisplayName
                                                       select c).First();
                                        if (currentItem.LastStatus == ServiceControllerStatusEx.Running)
                                        {
                                            SetListViewIcon(lvi, Status.Running);
                                        }
                                        else if (currentItem.LastStatus == ServiceControllerStatusEx.Stopped)
                                        {
                                            SetListViewIcon(lvi, Status.Stopped);
                                        }
                                        else if (currentItem.LastStatus == ServiceControllerStatusEx.Paused)
                                        {
                                            SetListViewIcon(lvi, Status.Disabled);
                                        }
                                        else if ((currentItem.LastStatus == ServiceControllerStatusEx.StartPending) ||
                                                (currentItem.LastStatus == ServiceControllerStatusEx.StopPending) ||
                                                (currentItem.LastStatus == ServiceControllerStatusEx.ContinuePending))
                                            SetListViewIcon(lvi, Status.Busy);
                                        else
                                            SetListViewIcon(lvi, Status.Unknown);
                                        lvi.Tag = null;
                                        sdi = null;
                                        lvi.Tag = currentItem;
                                    }
                                    else
                                    {
                                        sdi.Enabled = false;
                                        SetListViewIcon(lvi, Status.Disabled);
                                    }                                    
                                }
                            }
                            catch
                            {
                                SetListViewIcon(lvi, Status.Unknown);
                            }
                        }
                    }
                }
                RefreshNotifyIcon();
                UpdateStatusbarText();
                SetServiceMenuItemsEnable();
            }
            catch (Exception ex)
            {
                tsbStatus.Text = "Error!";
                tsbStatus.ToolTipText = ex.Message;
            }
            finally
            {
                lvwServices.EndUpdate();
                mainTimer.Enabled = autoRefreshToolStripMenuItem.Checked;
            }
        }
        private void AttachEvents()
        {
            mainTimer.Interval = Properties.Settings.Default.MainTimerFrequencySec * 1000;
            mainTimer.Tick += new EventHandler(mainTimer_Tick);
            mainNotifyIcon.DoubleClick += new EventHandler(mainNotifyIcon_DoubleClick);
            mainNotifyIcon.MouseUp += new MouseEventHandler(mainNotifyIcon_MouseUp);
            this.Resize += new EventHandler(Form1_Resize);
            serviceQueryEngineAsync.ServicesStatusCheckComplete += new ServicesStatusCheckDelegate(serviceQueryEngineAsync_ServicesStatusCheckComplete);
            serviceQueryEngineAsync.ServicesStatusCheckError += new ServicesStatusCheckErrorDelegate(serviceQueryEngineAsync_ServicesStatusCheckError);
        }        
        private void UpdateListViewFooters()
        {
            lvwServices.SetGroupState(SrvsTool.Controls.ListViewGroupState.Collapsible);
            foreach (ListViewGroup lvg in lvwServices.Groups)
                lvwServices.SetGroupFooter(lvg, lvg.Items.Count + " service(s)");
        }
        private void AddServiceToListView(ServiceDisplayItem sdi)
        {
            if (sdi != null)
            {
                ListViewGroup group = null;
                //try
                //{
                //    if (sdi == null)
                //        return;
                //}
                //catch { }


                foreach (ListViewGroup lvg in lvwServices.Groups)
                {
                    if (lvg.Name.ToUpper() == sdi.HostName.ToUpper())
                    {
                        group = lvg;
                        break;
                    }
                }
                if (group == null)
                {
                    group = new ListViewGroup(sdi.HostName.ToUpper(), sdi.HostName.ToUpper());
                    lvwServices.Groups.Add(group);
                }
                ListViewItem lvi = new ListViewItem(sdi.DisplayName);
                lvi.Group = group;
                if (sdi.Enabled)
                {
                    lvi.ImageIndex = 0;
                }
                else
                    SetListViewIcon(lvi, Status.Disabled);

                lvi.Tag = sdi;
                if ((Properties.Settings.Default.LastSelectedService.ToLower() == sdi.ServiceName.ToLower()) && (Properties.Settings.Default.LastSelectedServiceHost.ToLower() == sdi.HostName.ToLower()))
                    lvi.Selected = true;
                lvwServices.Items.Add(lvi);
            }
        }
        private void AddServiceToListView(string host, string serviceName)
        {
            ListViewGroup group = null;
            foreach (ListViewGroup lvg in lvwServices.Groups)
            {
                if (lvg.Name == host)
                {
                    group = lvg;
                }
            }
            if (group == null)
                group = new ListViewGroup(host, host);
            ListViewItem lvi = new ListViewItem(serviceName);
            lvi.Group = group;
            lvi.ImageIndex = 0;
            lvwServices.Items.Add(lvi);
        }
        private void UpdateServiceStatusses()
        {
            IServiceControllerEx currentItem;
            try
            {
                mainTimer.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                lvwServices.BeginUpdate();

                foreach (ListViewGroup grp in lvwServices.Groups)
                {
                    if ((from ListViewItem lviq in grp.Items
                         where lviq.Tag != null &&
                           ((ServiceDisplayItem)lviq.Tag).Enabled
                         select lviq).Count() > 0)
                    {
                        ServiceControllerExCollection serviceList = ServiceQueryFactory.GetServices(serviceQueryType, grp.Name);
                        foreach (ListViewItem lvi in grp.Items)
                        {
                            try
                            {
                                ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                                if (sdi.Enabled)
                                {
                                    currentItem = serviceList.GetServiceByName(sdi.ServiceName);
                                    if (currentItem == null)
                                    {
                                        sdi.Enabled = false;
                                        SetListViewIcon(lvi, Status.Disabled);
                                    }
                                    else if (currentItem.Status == ServiceControllerStatus.Running)
                                    {
                                        SetListViewIcon(lvi, Status.Running);
                                    }
                                    else if (currentItem.Status == ServiceControllerStatus.Stopped)
                                    {
                                        SetListViewIcon(lvi, Status.Stopped);
                                    }
                                    else if (currentItem.Status == ServiceControllerStatus.Paused)
                                    {
                                        SetListViewIcon(lvi, Status.Disabled);
                                    }
                                    else if ((currentItem.Status == ServiceControllerStatus.StartPending) ||
                                            (currentItem.Status == ServiceControllerStatus.StopPending) ||
                                            (currentItem.Status == ServiceControllerStatus.ContinuePending))
                                        SetListViewIcon(lvi, Status.Busy);
                                    else
                                        SetListViewIcon(lvi, Status.Unknown);
                                    lvi.Tag = null;
                                    lvi.Tag = sdi;
                                }
                            }
                            catch
                            {
                                SetListViewIcon(lvi, Status.Unknown);
                            }
                        }
                    }
                }
                RefreshNotifyIcon();
                UpdateStatusbarText();
                SetServiceMenuItemsEnable();
            }
            catch (Exception ex)
            {
                tsbStatus.Text = "Error!";
                tsbStatus.ToolTipText = ex.Message;
            }
            finally
            {
                lvwServices.EndUpdate();
                Cursor.Current = Cursors.Default;

                mainTimer.Enabled = true;
            }
            lvwServices.Columns[0].Width = lvwServices.ClientSize.Width;
        }
        private void UpdateStatusbarText()
        {
            int runningCount = 0;
            int stoppedCount = 0;
            foreach (ListViewItem lvi in lvwServices.Items)
            {
                if (lvi.ImageIndex == (int)Status.Running)
                {
                    runningCount++;
                }
                else if (lvi.ImageIndex == (int)Status.Stopped)
                {
                    stoppedCount++;
                }

            }
            tsbStatistics.Text = runningCount.ToString() + " running, " + stoppedCount.ToString() + " stopped, " + lvwServices.SelectedItems.Count.ToString() + " selected";
        }
        private void SetListViewIcon(ListViewItem lvi, Status status)
        {
            int statusInt = (int)status;
            if (lvi.ImageIndex != statusInt)
                lvi.ImageIndex = statusInt;
            
        }
        private void SetServiceMenuItemsEnable()
        {
            bool itemSelected = (lvwServices.SelectedItems.Count > 0);
            bool canStop = false;
            bool canStart = false;
            bool canRestart = false;
            bool canEnable = false;
            bool canDisable = false;
            if (itemSelected)
            {
                ServiceDisplayItem sdi = (ServiceDisplayItem)lvwServices.SelectedItems[0].Tag;
                canDisable = sdi.Enabled;
                canEnable = !sdi.Enabled;
                if (lvwServices.SelectedItems[0].ImageIndex == (int)Status.Running)
                {
                    canRestart = true;
                    canStop = true;
                }
                else if (lvwServices.SelectedItems[0].ImageIndex == (int)Status.Stopped)
                {
                    canStart = true;
                }
            }
            removeToolStripMenuItem.Enabled = itemSelected;
            removeToolStripMenuItem1.Enabled = itemSelected;
            toolStripButtonRemove.Enabled = itemSelected;
            toolStripButtonStart.Enabled = itemSelected && canStart;
            toolStripButtonStop.Enabled = itemSelected && canStop;
            toolStripButtonRestart.Enabled = itemSelected && canRestart;
            startServiceToolStripMenuItem.Enabled = itemSelected && canStart;
            startServiceToolStripMenuItem1.Enabled = itemSelected && canStart;
            stopServiceToolStripMenuItem.Enabled = itemSelected && canStop;
            stopServiceToolStripMenuItem1.Enabled = itemSelected && canStop;
            restartServiceToolStripMenuItem.Enabled = itemSelected && canRestart;
            restartServiceToolStripMenuItem1.Enabled = itemSelected && canRestart;
            startToolStripMenuItem.Enabled = itemSelected && canStart;
            stopToolStripMenuItem.Enabled = itemSelected && canStop;
            restartToolStripMenuItem.Enabled = itemSelected && canRestart;

            enableToolStripMenuItem.Enabled = canEnable;
            enableToolStripMenuItem1.Enabled = canEnable;
            disableToolStripMenuItem.Enabled = canDisable;
            disableToolStripMenuItem1.Enabled = canDisable;
        }
        private void LoadServiceListFile(string filename)
        {
            try
            {
                lvwServices.Groups.Clear();
                lvwServices.Items.Clear();
                if (System.IO.File.Exists(filename))
                {
                    currentServices = (List<ServiceDisplayItem>)SerializationUtils.DeserializeXMLFile(filename, typeof(List<ServiceDisplayItem>));
                    currentServices.Sort();
                    foreach (ServiceDisplayItem sdi in currentServices)
                    {
                        if (sdi != null)
                            AddServiceToListView(sdi);
                    }
                    AddServiceListFileToRecentList(filename);
                    UpdateListViewFooters();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveServiceListFile(string filename)
        {
            currentServices = new List<ServiceDisplayItem>();
            foreach (ListViewItem lvi in lvwServices.Items)
            {
                ServiceDisplayItem currentService = (ServiceDisplayItem)lvi.Tag;
                currentServices.Add(currentService);
            }
            SerializationUtils.SerializeXMLAndSave(currentServices, filename);
            AddServiceListFileToRecentList(filename);
        }
        private void AddServiceListFileToRecentList(string filename)
        {
            if (Properties.Settings.Default.RecentFileList == null)
                Properties.Settings.Default.RecentFileList = new System.Collections.Specialized.StringCollection();
            if (! Properties.Settings.Default.RecentFileList.Contains(filename))
                Properties.Settings.Default.RecentFileList.Add(filename);
            Properties.Settings.Default.LastServiceListFile = filename;
            if (CoreHelpers.RunningOnWin7 && jumpList != null)
            {
                jumpList.AddToRecent(filename);
                jumpList.Refresh();
            }
            LoadQuickLoadList();
        }
        private void LoadQuickLoadList()
        {
            cboQuickLoad.Items.Clear();
            cboQuickLoad.Items.Add("Quick load list...");
            foreach (string serviceListFile in (from string s in Properties.Settings.Default.RecentFileList
                                                where !s.EndsWith("LastSrvsToolList.srvlst")
                                                orderby s
                                                select s).Distinct())
            {
                cboQuickLoad.Items.Add(new RecentFile(serviceListFile));
            }
            cboQuickLoad.SelectedIndex = 0;
        }
        private void PerformStartupAction()
        {
            if (lvwServices.SelectedItems.Count == 1)
            {
                if (lvwServices.SelectedItems[0].Text.ToLower() == StartUpService.ToLower())
                {
                    if (StartUpMachine.Length > 0 &&
                        lvwServices.SelectedItems[0].Group.Name.ToLower() != StartUpMachine.ToLower())
                        return;
                    if (StartUpAction == "start")
                        startServiceToolStripMenuItem_Click(null, null);
                    else if (StartUpAction == "stop")
                        stopServiceToolStripMenuItem_Click(null, null);
                    else if (StartUpAction == "restart")
                        restartServiceToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void SaveAllSettingForClose()
        {
            try
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.MainWindowLocation = this.Location;
                    Properties.Settings.Default.MainWindowSize = this.Size;
                }
                Properties.Settings.Default.Save();

                if (lvwServices.Items.Count > 0)
                {
                    SaveServiceListFile(Properties.Settings.Default.LastServiceListFile);
                }
            }
            catch { }
        }
        #endregion     

        #region Asynchronous refreshing of service statusses
        private void serviceQueryEngineAsync_ServicesStatusCheckError(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    tsbStatus.Text = message;
                    tsbStatus.ToolTipText = message;
                }
                );
            }
            else
            {
                tsbStatus.Text = message;
                tsbStatus.ToolTipText = message;
            }

        }
        private void serviceQueryEngineAsync_ServicesStatusCheckComplete()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateSrvsList();
                    Cursor.Current = Cursors.Default;
                }
                );
            }
            else
            {
                UpdateSrvsList();
                Cursor.Current = Cursors.Default;
            }
        }

        private void PerformServicesAction(List<ServiceDisplayItem> actionServices)
        {
            if (actionServices != null && actionServices.Count > 0)
            {
                if (!AdminModeTools.IsInAdminMode() &&
                         (from sdi in actionServices
                          where sdi.HostName.ToLower() == System.Net.Dns.GetHostName().ToLower()
                          select sdi).Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ServiceDisplayItem sdi in actionServices)
                    {
                        sb.AppendLine(sdi.NextAction + "|" + sdi.HostName + "|" + sdi.ServiceName);
                    }
                    string restartActionsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Hen IT\\SrvsTool3\\SrvsTool3RestartActions.txt");
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(restartActionsPath)))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(restartActionsPath));
                    }
                    System.IO.File.WriteAllText(restartActionsPath, sb.ToString());
                    SaveAllSettingForClose();
                    try
                    {
                        AdminModeTools.RestartInAdminMode();
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Restart in Admin mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Threading.Tasks.ParallelOptions po = new System.Threading.Tasks.ParallelOptions()
                    {
                        MaxDegreeOfParallelism = Properties.Settings.Default.ConcurrencyLevel
                    };
                    System.Threading.Tasks.ParallelLoopResult parResult = System.Threading.Tasks.Parallel.ForEach(actionServices, po, sdi => PerformServiceAction(sdi));
                    if (!parResult.IsCompleted)
                    {
                        tsbStatus.Text = "Error action services in parralel";
                    }
                    RefreshSrvsInList();
                }
            }
        }
        private void PerformServiceAction(ServiceDisplayItem sdi)
        {
            try
            {
                WaitForServiceChange waitForServiceChange;
                DateTime begin = DateTime.Now;
                waitForServiceChange = new WaitForServiceChange();
                IServiceControllerEx service = ServiceQueryFactory.GetService(serviceQueryType, sdi.HostName, sdi.ServiceName);
                waitForServiceChange.SetWindowSize("Action: " + sdi.NextAction + " => " + service.DisplayName);
                if (sdi.NextAction.ToLower() == "start")
                {
                    try
                    {
                        waitForServiceChange.Show("Starting service " + service.DisplayName, "Starting " + service.DisplayName);
                        service.Start();
                        Application.DoEvents();
                        while ((service.Status != ServiceControllerStatus.Running) &&
                                   (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                        {
                            service.Refresh();
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            Thread.Sleep(500);
                        }
                    }
                    catch { }
                }
                else if (sdi.NextAction.ToLower() == "stop")
                {
                    try
                    {
                        waitForServiceChange.Show("Stopping service " + service.DisplayName, "Stopping " + service.DisplayName);
                        service.Stop();
                        Application.DoEvents();
                        while ((service.Status != ServiceControllerStatus.Stopped) &&
                                   (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                        {
                            service.Refresh();
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            Thread.Sleep(500);
                        }
                    }
                    catch { }
                }
                else if (sdi.NextAction.ToLower() == "restart")
                {
                    try
                    {
                        waitForServiceChange.Show("Stopping service " + service.DisplayName, "Stopping " + service.DisplayName);
                        service.Stop();
                        Application.DoEvents();
                        while ((service.Status != ServiceControllerStatus.Stopped) &&
                                   (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                        {
                            service.Refresh();
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            Thread.Sleep(500);
                        }
                    }
                    catch { }
                    try
                    {
                        waitForServiceChange.Show("Starting service " + service.DisplayName, "Starting " + service.DisplayName);
                        service.Start();
                        Application.DoEvents();
                        while ((service.Status != ServiceControllerStatus.Running) &&
                                   (((TimeSpan)DateTime.Now.Subtract(begin)).TotalSeconds < 30))
                        {
                            service.Refresh();
                            Application.DoEvents();
                            Cursor.Current = Cursors.WaitCursor;
                            Thread.Sleep(500);
                        }
                    }
                    catch { }
                }

                Thread.Sleep(500);
                if (waitForServiceChange.Visible)
                    waitForServiceChange.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Adminmode
        private void RestartInAdminMode(string action)
        {
            StringBuilder sb = new StringBuilder();
            foreach(ListViewItem lvi in lvwServices.SelectedItems)
            {
                ServiceDisplayItem sdi = (ServiceDisplayItem)lvi.Tag;
                sb.AppendLine("action|" + sdi.HostName + "|" + sdi.ServiceName);
            }
            try
            {
                string restartActionsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Hen IT\\SrvsTool3\\SrvsTool3RestartActions.txt");
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(restartActionsPath)))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(restartActionsPath));
                }
                System.IO.File.WriteAllText(restartActionsPath, sb.ToString());
                Properties.Settings.Default.Save();
                AdminModeTools.RestartInAdminMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Restart in Admin mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestartInAdminMode(string action, string serviceName, string hostName)
        {


            //if (noprompt || MessageBox.Show("This action requires administrative rights!\r\nRestart application in Administrative mode?", "Admin mode", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                Properties.Settings.Default.Save();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                if (action.Length > 0)
                    startInfo.Arguments = string.Format("{0} \"{1}\" {2}", action, serviceName, hostName);
                startInfo.Verb = "runas";
                try
                {
                    Process p = Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception) // ex)
                {
                    return;
                }

                Application.Exit();
            }
        }
        bool IsAdmin()
        {
            string strIdentity;
            try
            {
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                WindowsPrincipal wp = new WindowsPrincipal(wi);
                strIdentity = wp.Identity.Name;

                if (wp.IsInRole(WindowsBuiltInRole.Administrator))
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }

        }
        #endregion

        private void txtQuickSelect_TextChanged(object sender, EventArgs e)
        {
            if (txtQuickSelect.Text.Trim().Length > 2)
            {
                lvwServices.SelectedItems.Clear();
                foreach (ListViewItem lvi in (from ListViewItem l in lvwServices.Items
                                               where l.Text.ToLower().Contains(txtQuickSelect.Text.ToLower() )
                                               select l))
                {
                    lvi.Selected = true;
                }
            }
        }

    }
}