using System;
using System.Windows.Forms;
using System.Linq;
using HenIT.Security;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;

namespace SrvsTool
{
    static class Program
    {
        private static TaskbarManager windowsTaskbar;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Properties.Settings.Default.NewVersion)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.NewVersion = false;
                Properties.Settings.Default.Save();
            }

            try
            {
                if (AdminModeTools.IsInAdminMode())
                {
                    //if (!AdminModeTools.CheckIfAdminLaunchTaskExist())
                    {
                        AdminModeTools.CreateAdminLaunchTask();
                    }
                }
            }
            catch { }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();            
            if (args.Length > 0)
            {
                if (args[0].ToLower().EndsWith(".srvlst"))
                {
                    if (System.IO.File.Exists(args[0]))
                    {
                        mainForm.StartupServicesListFile = args[0];
                    }
                }
                else if (ContainsAction(args[0]))
                {
                    mainForm.StartUpAction = args[0].ToLower();
                    if (args.Length > 1)
                    {
                        mainForm.StartUpService = args[1];
                    }
                    if (args.Length > 2)
                    {
                        mainForm.StartUpMachine = args[2];
                    }
                }
            }
            else
            {
                try
                {
                    mainForm.StartUpActions = new System.Collections.Generic.List<string>();
                    string restartActionsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Hen IT\\SrvsTool3\\SrvsTool3RestartActions.txt");
                    if (System.IO.File.Exists(restartActionsPath))
                    {
                        string[] restartActions = System.IO.File.ReadAllLines(restartActionsPath);
                        if (restartActions != null && restartActions.Length > 0)
                        {
                            foreach (string restartAction in restartActions)
                            {
                                string[] parts = restartAction.Split('|');
                                if (parts.Length == 3)
                                {
                                    mainForm.StartUpActions.Add(restartAction);
                                }
                            }
                        }
                        if (System.IO.File.Exists(restartActionsPath.Replace("SrvsTool3RestartActions.txt" , "SrvsTool3RestartActions.bak")))
                            System.IO.File.Delete(restartActionsPath.Replace("SrvsTool3RestartActions.txt", "SrvsTool3RestartActions.bak"));
                        System.IO.File.Move(restartActionsPath, restartActionsPath.Replace("SrvsTool3RestartActions.txt", "SrvsTool3RestartActions.bak"));
                    }
                }
                catch { }

                
                //if (Properties.Settings.Default.AfterRestartInAdminModeActions != null && Properties.Settings.Default.AfterRestartInAdminModeActions.Count > 0)
                //{                    
                //    mainForm.StartUpActions.AddRange((from string s in Properties.Settings.Default.AfterRestartInAdminModeActions
                //                                      select s).ToArray());
                //    Properties.Settings.Default.AfterRestartInAdminModeActions = new System.Collections.Specialized.StringCollection();
                //    Properties.Settings.Default.Save();
                //}
            }

            if (CoreHelpers.RunningOnWin7)
            {
                windowsTaskbar = TaskbarManager.Instance;
            }
           
            Application.Run(mainForm);
        }

        private static bool ContainsAction(string action)
        {
            return action.ToLower() == "start" ||
                action.ToLower() == "stop" ||
                action.ToLower() == "restart";
        }
    }
}
