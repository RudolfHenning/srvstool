using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Management;

namespace SrvsTool
{
    class ServiceControllerExWMI : IServiceControllerEx, IDisposable    
    {
        private string wmiManagementPath;
        private ManagementObject managementObject = null;

        #region Properties
        private string machineName = ".";
        public string MachineName
        {
            get { return machineName; }
        }
        private string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }
        private string serviceName;
        public string ServiceName
        {
            get
            {
                return serviceName;
            }
        }
        private string description = string.Empty;
        public string Description
        {
            get { return description; }
        }
        private string executablePath;
        public string ExecutablePath
        {
            get { return executablePath; }
        }
        private string account;
        public string Account
        {
            get { return account; }
        }


        public bool CanStart
        {
            get
            {
                return ((status == ServiceControllerStatus.Stopped) && (serviceStartup != ServiceStart.Disabled));
            }
        }
        private bool canStop;
        public bool CanStop
        {
            get
            {
                return ((status == ServiceControllerStatus.Running) && canStop);
            }
        }
        private bool canPause;
        public bool CanPause
        {
            get
            {
                return ((status == ServiceControllerStatus.Running) && canPause);
            }
        }
        private ServiceControllerStatus status;
        public ServiceControllerStatus Status
        {
            get
            {
                return status;
            }
        }
        private ServiceStart serviceStartup;
        public ServiceStart ServiceStartup
        {
            get
            {
                return serviceStartup;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance based on the WMI path
        /// </summary>
        /// <param name="wmiManagementPath"></param>
        public ServiceControllerExWMI(string wmiManagementPath)
        {
            this.wmiManagementPath = wmiManagementPath;
            RefreshInternal();
        }
        /// <summary>
        /// Create a new instance based on an existng WMI Management object
        /// </summary>
        /// <param name="managementObject"></param>
        public ServiceControllerExWMI(ManagementObject managementObject)
        {
            this.managementObject = managementObject;
            wmiManagementPath = managementObject.Path.Path;
            RefreshInternal();
        }
        #endregion

        #region Public Methods
        public void Start()
        {
            if (CanStart)
            {
                managementObject.InvokeMethod("StartService", null);
                //status = ServiceControllerStatus.Running;
            }
        }
        public void Stop()
        {
            if (CanStop)
            {
                managementObject.InvokeMethod("StopService", null);
                //status = ServiceControllerStatus.Stopped;
            }
        }
        public void Pause()
        {
            if (CanPause)
            {
                managementObject.InvokeMethod("PauseService", null);
                //status = ServiceControllerStatus.Paused;
            }
        }
        public void Resume()
        {
            if (status == ServiceControllerStatus.Paused)
            {
                managementObject.InvokeMethod("ResumeService", null);
                //status = ServiceControllerStatus.Running;
            }
        }
        public void Refresh()
        {
            if (managementObject != null)
                managementObject.Dispose();
            managementObject = new ManagementObject(wmiManagementPath);
            RefreshInternal();
        }
        public override string ToString()
        {
            return serviceName;
        }
        #endregion

        #region Private Methods
        private void RefreshInternal()
        {
            displayName = managementObject["DisplayName"].ToString();
            serviceName = managementObject["Name"].ToString();
            if (managementObject["Description"] != null)
                description = managementObject["Description"].ToString();
            if (managementObject["State"].ToString() == "Running")
                status = ServiceControllerStatus.Running;
            else if (managementObject["State"].ToString() == "Stopped")
                status = ServiceControllerStatus.Stopped;
            else if (managementObject["State"].ToString() == "Paused")
                status = ServiceControllerStatus.Paused;
            else if (managementObject["State"].ToString() == "Start Pending")
                status = ServiceControllerStatus.StartPending;
            else if (managementObject["State"].ToString() == "Stop Pending")
                status = ServiceControllerStatus.StopPending;
            else if (managementObject["State"].ToString() == "Pause Pending")
                status = ServiceControllerStatus.PausePending;
            else
                status = ServiceControllerStatus.ContinuePending;
            if (managementObject["StartMode"].ToString() == "Auto")
                serviceStartup = ServiceStart.Automatic;
            else if (managementObject["StartMode"].ToString() == "Manual")
                serviceStartup = ServiceStart.Manual;
            else if (managementObject["StartMode"].ToString() == "Disabled")
                serviceStartup = ServiceStart.Disabled;
            else
                serviceStartup = ServiceStart.Unknown;
            canStop = (bool)managementObject["AcceptStop"];
            canPause = (bool)managementObject["AcceptPause"];
            executablePath = managementObject["PathName"].ToString();
            account = managementObject["StartName"].ToString();
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
                list.Add(new ServiceControllerExWMI(mo));
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
            if (managementObject != null)
            {
                managementObject.Dispose();
            }
        }

        #endregion
    }
}
