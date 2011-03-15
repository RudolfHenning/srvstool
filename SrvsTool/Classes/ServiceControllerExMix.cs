using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Management;

namespace SrvsTool
{
    class ServiceControllerExMix : IServiceControllerEx, IDisposable
    {
        private ServiceController service;
        private string machineName = ".";
        private ServiceStart serviceStartup = ServiceStart.Unknown;
        private string account = string.Empty;
        private string description = string.Empty;
        private string executablePath = string.Empty;

        #region Constructors
        public ServiceControllerExMix(ManagementObject managementObject)
        {
            this.service = new ServiceController(managementObject["DisplayName"].ToString(), managementObject["SystemName"].ToString());
            machineName = managementObject["SystemName"].ToString();
            if (managementObject["StartMode"].ToString() == "Auto")
                serviceStartup = ServiceStart.Automatic;
            else if (managementObject["StartMode"].ToString() == "Manual")
                serviceStartup = ServiceStart.Manual;
            else if (managementObject["StartMode"].ToString() == "Disabled")
                serviceStartup = ServiceStart.Disabled;
            else
                serviceStartup = ServiceStart.Unknown;
            account = managementObject["StartName"].ToString();
            if (managementObject["Description"] != null)
                description = managementObject["Description"].ToString();
            executablePath = managementObject["PathName"].ToString();
        }
        #endregion

        #region Properties
        public string Account
        {
            get { return account; }
        }
        public string Description
        {
            get { return description; }
        }
        public string DisplayName
        {
            get { return service.DisplayName; }
        }
        public string ExecutablePath
        {
            get { return executablePath; }
        }
        public string MachineName
        {
            get { return machineName; }
        }
        public string ServiceName
        {
            get { return service.ServiceName; }
        }
        public ServiceStart ServiceStartup
        {
            get { return serviceStartup; }
        }
        public System.ServiceProcess.ServiceControllerStatus Status
        {
            get { return service.Status; }
        }
        public bool CanPause
        {
            get { return service.CanPauseAndContinue && (service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.Paused); }
        }
        public bool CanStart
        {
            get { return ((service.Status == ServiceControllerStatus.Stopped) && (ServiceStartup != ServiceStart.Disabled)); }
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
        #endregion

        #region Public methods
        public void Pause()
        {
            service.Pause();
        }
        public void Resume()
        {
            service.Continue();
        }
        public void Start()
        {
            service.Start();
        }
        public void Stop()
        {
            service.Stop();
        }
        public void Refresh()
        {
            service.Refresh();
        }
        public override string ToString()
        {
            return ServiceName;
        }
        #endregion

        #region Static methods for getting List of services
        public static ServiceControllerExCollection GetServices(string machineName, bool impersonate, string user, string pass)
        {
            ServiceControllerExCollection list = new ServiceControllerExCollection();
            if ((machineName.Length == 0) || (machineName == "localhost"))
                machineName = ".";
            ManagementObjectCollection servicesQuery = GetServiceCollection(machineName, impersonate, user, pass);
            foreach (ManagementObject mo in servicesQuery)
            {
                list.Add(new ServiceControllerExMix(mo));
            }
            return list;
        }
        private static ManagementObjectCollection GetServiceCollection(string machineName, bool impersonate, string user, string pass)
        {
            string stringQuery = "SELECT * FROM Win32_Service";
            ManagementObjectSearcher query;
            ManagementObjectCollection queryCollection = null;
            System.Management.ObjectQuery oq;

            //Connect to the remote computer
            ConnectionOptions co = new ConnectionOptions();

            if (machineName != ".")
            {
                if (System.Net.Dns.GetHostAddresses(machineName).Length == 0)
                {
                    throw new Exception("Machine name not found on the network!");
                }
            }

            //get user and password
            if (impersonate)
            {
                co.Username = user;
                co.Password = pass;
            }

            //Point to machine
            ManagementScope ms = new ManagementScope("\\\\" + machineName + "\\root\\cimv2", co);

            //Query remote computer across the connection
            oq = new System.Management.ObjectQuery(stringQuery);
            query = new ManagementObjectSearcher(ms, oq);

            try
            {
                queryCollection = query.Get();
            }
            catch (Exception e1)
            {
                throw new Exception("Error getting list of services!", e1);
            }
            return queryCollection;
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
