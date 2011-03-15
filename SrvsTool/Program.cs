using System;
using System.Windows.Forms;
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
