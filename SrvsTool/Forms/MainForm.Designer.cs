namespace SrvsTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Host", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Loading...",
            "RHenning"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxNotifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showServiceMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.lvwServices = new SrvsTool.Controls.ListViewEx();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.servicesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.batchActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.imageListServiceStates = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsbStatistics = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdminSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialogLoad = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSave = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cboQuickLoad = new System.Windows.Forms.ComboBox();
            this.timerQuickload = new System.Windows.Forms.Timer(this.components);
            this.startServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadFavourites = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRestart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.tsbAdmin = new System.Windows.Forms.ToolStripStatusLabel();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartInAdminModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServiceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServiceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartServiceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesMmcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxNotifyIconMenu.SuspendLayout();
            this.servicesContextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.ctxNotifyIconMenu;
            this.mainNotifyIcon.Text = "Services tool";
            this.mainNotifyIcon.Visible = true;
            // 
            // ctxNotifyIconMenu
            // 
            this.ctxNotifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showServiceMonitorToolStripMenuItem,
            this.toolStripMenuItem4,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem1});
            this.ctxNotifyIconMenu.Name = "ctxNotifyIconMenu";
            this.ctxNotifyIconMenu.Size = new System.Drawing.Size(195, 126);
            // 
            // showServiceMonitorToolStripMenuItem
            // 
            this.showServiceMonitorToolStripMenuItem.Name = "showServiceMonitorToolStripMenuItem";
            this.showServiceMonitorToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.showServiceMonitorToolStripMenuItem.Text = "Show Services monitor";
            this.showServiceMonitorToolStripMenuItem.Click += new System.EventHandler(this.showServiceMonitorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(191, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(191, 6);
            // 
            // lvwServices
            // 
            this.lvwServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName});
            this.lvwServices.ContextMenuStrip = this.servicesContextMenuStrip;
            this.lvwServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwServices.FullRowSelect = true;
            listViewGroup1.Header = "Host";
            listViewGroup1.Name = "listViewGroupHosts";
            this.lvwServices.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            listViewItem1.Group = listViewGroup1;
            this.lvwServices.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvwServices.Location = new System.Drawing.Point(0, 80);
            this.lvwServices.Name = "lvwServices";
            this.lvwServices.Size = new System.Drawing.Size(344, 191);
            this.lvwServices.SmallImageList = this.imageListServiceStates;
            this.lvwServices.TabIndex = 0;
            this.lvwServices.UseCompatibleStateImageBehavior = false;
            this.lvwServices.View = System.Windows.Forms.View.Details;
            this.lvwServices.SelectedIndexChanged += new System.EventHandler(this.lvwServices_SelectedIndexChanged);
            this.lvwServices.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvwServices_KeyUp);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 312;
            // 
            // servicesContextMenuStrip
            // 
            this.servicesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServiceToolStripMenuItem,
            this.stopServiceToolStripMenuItem,
            this.restartServiceToolStripMenuItem,
            this.batchActionsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.servicesContextMenuStrip.Name = "servicesContextMenuStrip";
            this.servicesContextMenuStrip.Size = new System.Drawing.Size(150, 186);
            // 
            // batchActionsToolStripMenuItem
            // 
            this.batchActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startAllToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            this.batchActionsToolStripMenuItem.Name = "batchActionsToolStripMenuItem";
            this.batchActionsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.batchActionsToolStripMenuItem.Text = "Batch actions";
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.startAllToolStripMenuItem.Text = "Start all";
            this.startAllToolStripMenuItem.Click += new System.EventHandler(this.startAllToolStripMenuItem_Click);
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.stopAllToolStripMenuItem.Text = "Stop all";
            this.stopAllToolStripMenuItem.Click += new System.EventHandler(this.stopAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(146, 6);
            // 
            // imageListServiceStates
            // 
            this.imageListServiceStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListServiceStates.ImageStream")));
            this.imageListServiceStates.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListServiceStates.Images.SetKeyName(0, "Unknown.ico");
            this.imageListServiceStates.Images.SetKeyName(1, "Running.ico");
            this.imageListServiceStates.Images.SetKeyName(2, "Stopped.ico");
            this.imageListServiceStates.Images.SetKeyName(3, "Busy.ico");
            this.imageListServiceStates.Images.SetKeyName(4, "Paused.ico");
            this.imageListServiceStates.Images.SetKeyName(5, "NOSMOKE.ico");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdmin,
            this.tsbStatus,
            this.tsbStatistics});
            this.statusStrip1.Location = new System.Drawing.Point(0, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(344, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsbStatus
            // 
            this.tsbStatus.AutoSize = false;
            this.tsbStatus.Name = "tsbStatus";
            this.tsbStatus.Size = new System.Drawing.Size(150, 17);
            this.tsbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsbStatistics
            // 
            this.tsbStatistics.AutoSize = false;
            this.tsbStatistics.Name = "tsbStatistics";
            this.tsbStatistics.Size = new System.Drawing.Size(179, 17);
            this.tsbStatistics.Spring = true;
            this.tsbStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem7,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(344, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItemAdminSeparator,
            this.restartInAdminModeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItemAdminSeparator
            // 
            this.toolStripMenuItemAdminSeparator.Name = "toolStripMenuItemAdminSeparator";
            this.toolStripMenuItemAdminSeparator.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServiceToolStripMenuItem1,
            this.stopServiceToolStripMenuItem1,
            this.restartServiceToolStripMenuItem1,
            this.toolStripMenuItem8,
            this.addToolStripMenuItem1,
            this.removeToolStripMenuItem1,
            this.enableToolStripMenuItem1,
            this.disableToolStripMenuItem1});
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem7.Text = "&Edit";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(153, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.refreshToolStripMenuItem,
            this.autoRefreshToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 6);
            // 
            // autoRefreshToolStripMenuItem
            // 
            this.autoRefreshToolStripMenuItem.Checked = true;
            this.autoRefreshToolStripMenuItem.CheckOnClick = true;
            this.autoRefreshToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
            this.autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.autoRefreshToolStripMenuItem.Text = "Auto refresh";
            this.autoRefreshToolStripMenuItem.Click += new System.EventHandler(this.autoRefreshToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servicesMmcToolStripMenuItem,
            this.eventlogToolStripMenuItem,
            this.toolStripMenuItem6,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // eventlogToolStripMenuItem
            // 
            this.eventlogToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Eventlog;
            this.eventlogToolStripMenuItem.Name = "eventlogToolStripMenuItem";
            this.eventlogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eventlogToolStripMenuItem.Text = "Eventlog";
            this.eventlogToolStripMenuItem.Click += new System.EventHandler(this.eventlogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoad,
            this.toolStripButtonLoadFavourites,
            this.toolStripButtonSaveAs,
            this.toolStripButtonRefresh,
            this.toolStripSplitButton1,
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripButtonRestart,
            this.toolStripButton4,
            this.toolStripButtonAdd,
            this.toolStripButtonRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 1, 2);
            this.toolStrip1.Size = new System.Drawing.Size(344, 35);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(6, 31);
            // 
            // openFileDialogLoad
            // 
            this.openFileDialogLoad.DefaultExt = "srvlst";
            this.openFileDialogLoad.Filter = "Service List Files|*.srvlst";
            this.openFileDialogLoad.Title = "Load service list";
            // 
            // saveFileDialogSave
            // 
            this.saveFileDialogSave.DefaultExt = "srvlst";
            this.saveFileDialogSave.Filter = "Service List Files|*.srvlst";
            this.saveFileDialogSave.Title = "Save service list";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Services tool";
            // 
            // cboQuickLoad
            // 
            this.cboQuickLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboQuickLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuickLoad.FormattingEnabled = true;
            this.cboQuickLoad.Location = new System.Drawing.Point(0, 59);
            this.cboQuickLoad.Name = "cboQuickLoad";
            this.cboQuickLoad.Size = new System.Drawing.Size(344, 21);
            this.cboQuickLoad.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cboQuickLoad, "Quick load");
            this.cboQuickLoad.SelectedIndexChanged += new System.EventHandler(this.cboQuickLoad_SelectedIndexChanged);
            // 
            // timerQuickload
            // 
            this.timerQuickload.Interval = 500;
            this.timerQuickload.Tick += new System.EventHandler(this.timerQuickload_Tick);
            // 
            // startServiceToolStripMenuItem
            // 
            this.startServiceToolStripMenuItem.Enabled = false;
            this.startServiceToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.PLAY;
            this.startServiceToolStripMenuItem.Name = "startServiceToolStripMenuItem";
            this.startServiceToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.startServiceToolStripMenuItem.Text = "Start service";
            this.startServiceToolStripMenuItem.Click += new System.EventHandler(this.startServiceToolStripMenuItem_Click);
            // 
            // stopServiceToolStripMenuItem
            // 
            this.stopServiceToolStripMenuItem.Enabled = false;
            this.stopServiceToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.stop;
            this.stopServiceToolStripMenuItem.Name = "stopServiceToolStripMenuItem";
            this.stopServiceToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.stopServiceToolStripMenuItem.Text = "Stop service";
            this.stopServiceToolStripMenuItem.Click += new System.EventHandler(this.stopServiceToolStripMenuItem_Click);
            // 
            // restartServiceToolStripMenuItem
            // 
            this.restartServiceToolStripMenuItem.Enabled = false;
            this.restartServiceToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Circular_Intersection;
            this.restartServiceToolStripMenuItem.Name = "restartServiceToolStripMenuItem";
            this.restartServiceToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.restartServiceToolStripMenuItem.Text = "Restart service";
            this.restartServiceToolStripMenuItem.Click += new System.EventHandler(this.restartServiceToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Enabled = false;
            this.enableToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.CHECKMRK;
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Enabled = false;
            this.disableToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.TRFFC13;
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoad.Image = global::SrvsTool.Properties.Resources.Folder_Yellow;
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLoad.Text = "Load";
            this.toolStripButtonLoad.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripButtonLoadFavourites
            // 
            this.toolStripButtonLoadFavourites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadFavourites.Image = global::SrvsTool.Properties.Resources.star;
            this.toolStripButtonLoadFavourites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadFavourites.Name = "toolStripButtonLoadFavourites";
            this.toolStripButtonLoadFavourites.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLoadFavourites.Text = "Recent";
            this.toolStripButtonLoadFavourites.Click += new System.EventHandler(this.recentToolStripMenuItem_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = global::SrvsTool.Properties.Resources.Floppy_Disk_Blue;
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSaveAs.Text = "Save as";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::SrvsTool.Properties.Resources.recycle;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Enabled = false;
            this.toolStripButtonStart.Image = global::SrvsTool.Properties.Resources.PLAY;
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonStart.Text = "Start";
            this.toolStripButtonStart.Click += new System.EventHandler(this.startServiceToolStripMenuItem_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = global::SrvsTool.Properties.Resources.stop;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.stopServiceToolStripMenuItem_Click);
            // 
            // toolStripButtonRestart
            // 
            this.toolStripButtonRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRestart.Enabled = false;
            this.toolStripButtonRestart.Image = global::SrvsTool.Properties.Resources.Circular_Intersection;
            this.toolStripButtonRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRestart.Name = "toolStripButtonRestart";
            this.toolStripButtonRestart.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRestart.Text = "Restart";
            this.toolStripButtonRestart.Click += new System.EventHandler(this.restartServiceToolStripMenuItem_Click);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = global::SrvsTool.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAdd.Text = "Add";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemove.Enabled = false;
            this.toolStripButtonRemove.Image = global::SrvsTool.Properties.Resources.Remove;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRemove.Text = "Remove";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // tsbAdmin
            // 
            this.tsbAdmin.AutoSize = false;
            this.tsbAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdmin.Image = global::SrvsTool.Properties.Resources.OUTLLIBR_9825;
            this.tsbAdmin.Name = "tsbAdmin";
            this.tsbAdmin.Size = new System.Drawing.Size(20, 17);
            this.tsbAdmin.ToolTipText = "Admin mode";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::SrvsTool.Properties.Resources._105_59;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Folder_Yellow;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Favourite;
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.recentToolStripMenuItem.Text = "Recent";
            this.recentToolStripMenuItem.Click += new System.EventHandler(this.recentToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Floppy_Disk_Blue;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // restartInAdminModeToolStripMenuItem
            // 
            this.restartInAdminModeToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.OUTLLIBR_9825;
            this.restartInAdminModeToolStripMenuItem.Name = "restartInAdminModeToolStripMenuItem";
            this.restartInAdminModeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.restartInAdminModeToolStripMenuItem.Text = "Restart in Admin mode";
            this.restartInAdminModeToolStripMenuItem.Click += new System.EventHandler(this.restartInAdminModeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Delete1;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // startServiceToolStripMenuItem1
            // 
            this.startServiceToolStripMenuItem1.Enabled = false;
            this.startServiceToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.PLAY;
            this.startServiceToolStripMenuItem1.Name = "startServiceToolStripMenuItem1";
            this.startServiceToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.startServiceToolStripMenuItem1.Text = "Start service";
            this.startServiceToolStripMenuItem1.Click += new System.EventHandler(this.startServiceToolStripMenuItem_Click);
            // 
            // stopServiceToolStripMenuItem1
            // 
            this.stopServiceToolStripMenuItem1.Enabled = false;
            this.stopServiceToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.stop;
            this.stopServiceToolStripMenuItem1.Name = "stopServiceToolStripMenuItem1";
            this.stopServiceToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.stopServiceToolStripMenuItem1.Text = "Stop service";
            this.stopServiceToolStripMenuItem1.Click += new System.EventHandler(this.stopServiceToolStripMenuItem_Click);
            // 
            // restartServiceToolStripMenuItem1
            // 
            this.restartServiceToolStripMenuItem1.Enabled = false;
            this.restartServiceToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.Refresh;
            this.restartServiceToolStripMenuItem1.Name = "restartServiceToolStripMenuItem1";
            this.restartServiceToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.restartServiceToolStripMenuItem1.Text = "Restart service";
            this.restartServiceToolStripMenuItem1.Click += new System.EventHandler(this.restartServiceToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.Add;
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.addToolStripMenuItem1.Text = "Add service";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.Remove;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.removeToolStripMenuItem1.Text = "Remove service";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // enableToolStripMenuItem1
            // 
            this.enableToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.CHECKMRK;
            this.enableToolStripMenuItem1.Name = "enableToolStripMenuItem1";
            this.enableToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.enableToolStripMenuItem1.Text = "Enable polling";
            this.enableToolStripMenuItem1.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
            // 
            // disableToolStripMenuItem1
            // 
            this.disableToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.TRFFC13;
            this.disableToolStripMenuItem1.Name = "disableToolStripMenuItem1";
            this.disableToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.disableToolStripMenuItem1.Text = "Disable polling";
            this.disableToolStripMenuItem1.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.recycle;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // servicesMmcToolStripMenuItem
            // 
            this.servicesMmcToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.options;
            this.servicesMmcToolStripMenuItem.Name = "servicesMmcToolStripMenuItem";
            this.servicesMmcToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.servicesMmcToolStripMenuItem.Text = "Services mmc";
            this.servicesMmcToolStripMenuItem.Click += new System.EventHandler(this.servicesMmcToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Wrench;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.PLAY;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.startToolStripMenuItem.Text = "Start service";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startServiceToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.stop;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.stopToolStripMenuItem.Text = "Stop service";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopServiceToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Enabled = false;
            this.restartToolStripMenuItem.Image = global::SrvsTool.Properties.Resources.Circular_Intersection;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.restartToolStripMenuItem.Text = "Restart service";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartServiceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Image = global::SrvsTool.Properties.Resources.Delete1;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 293);
            this.Controls.Add(this.lvwServices);
            this.Controls.Add(this.cboQuickLoad);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Services monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ctxNotifyIconMenu.ResumeLayout(false);
            this.servicesContextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonRestart;
        private System.Windows.Forms.ImageList imageListServiceStates;
        private System.Windows.Forms.ContextMenuStrip servicesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem startServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogLoad;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSave;
        private System.Windows.Forms.ToolStripStatusLabel tsbStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsbStatistics;
        private System.Windows.Forms.ContextMenuStrip ctxNotifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem showServiceMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSplitButton1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadFavourites;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesMmcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventlogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem startServiceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopServiceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restartServiceToolStripMenuItem1;
        private SrvsTool.Controls.ListViewEx lvwServices;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.ComboBox cboQuickLoad;
        private System.Windows.Forms.Timer timerQuickload;
        private System.Windows.Forms.ToolStripMenuItem batchActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartInAdminModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemAdminSeparator;
        private System.Windows.Forms.ToolStripStatusLabel tsbAdmin;
    }
}

