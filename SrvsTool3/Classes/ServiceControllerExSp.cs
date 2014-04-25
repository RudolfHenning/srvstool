using System;
using System.ServiceProcess;
using Microsoft.Win32;

namespace SrvsTool
{
    class ServiceControllerExSp : IServiceControllerEx, IDisposable 
    {
        private ServiceController service;
        private string machineName = ".";

        #region Constructors
        public ServiceControllerExSp(ServiceController service)
        {
            this.service = service;
            machineName = service.MachineName;
        }
        public ServiceControllerExSp(string serviceName)
        {
            service = new ServiceController(serviceName);
            machineName = ".";
        }
        public ServiceControllerExSp(string serviceName, string machineName)
        {
            service = new ServiceController(serviceName, machineName);
            this.machineName = machineName;
        }
        #endregion

        #region Properties
        public string Account
        {
            get
            {
                if (machineName == ".")
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\" + service.ServiceName);
                    string objectName = (string)key.GetValue("ObjectName");
                    key.Close();
                    key = null;
                    return objectName;
                }
                else
                    return "";
            }
        }
        public string Description
        {
            get
            {
                if (machineName == ".")
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\" + service.ServiceName);
                    string description = (string)key.GetValue("Description");
                    key.Close();
                    key = null;
                    return description;
                }
                else
                    return "";
            }
        }
        public string MachineName
        {
            get { return service.MachineName; }
        }
        public string DisplayName
        {
            get
            {
                return service.DisplayName;
            }
        }
        public string ServiceName
        {
            get
            {
                return service.ServiceName;
            }
        }
        public string ExecutablePath
        {
            get
            {
                if (machineName == ".")
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\" + service.ServiceName);
                    string imagePath = (string)key.GetValue("ImagePath");
                    key.Close();
                    key = null;
                    return imagePath;
                }
                else
                    return "";
            }
        }

        public void Start()
        {
            service.Start();
        }
        public void Stop()
        {
            service.Stop();
        }
        public void Pause()
        {
            service.Pause();
        }
        public void Resume()
        {
            service.Continue();
        }
        public bool CanStart
        {
            get
            {
                return ((service.Status == ServiceControllerStatus.Stopped) && (ServiceStartup != ServiceStart.Disabled));
            }
        }
        public bool CanStop
        {
            get
            {
                return (service.CanStop &&
                            (
                                (service.Status == ServiceControllerStatus.Running) ||
                                (service.Status == ServiceControllerStatus.Paused)
                            )
                        );
            }
        }
        public bool CanPause
        {
            get
            {
                return service.CanPauseAndContinue;
            }
        }
        public ServiceControllerStatus Status
        {
            get
            {
                return service.Status;
            }
        }
        public ServiceStart ServiceStartup
        {
            get
            {
                if (machineName == ".")
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\" + service.ServiceName);
                    ServiceStart start = (ServiceStart)key.GetValue("Start");
                    key.Close();
                    key = null;
                    return start;
                }
                else
                {
                    return ServiceStart.Unknown;
                }
            }
        }
        #endregion

        #region Public methods
        public void Refresh()
        {
            service.Refresh();
        }
        public override string ToString()
        {
            return service.ServiceName;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        #endregion
    }
}
