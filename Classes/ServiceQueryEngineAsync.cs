using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace SrvsTool
{
    public delegate void ServicesStatusCheckDelegate();
    public delegate void ServicesStatusCheckErrorDelegate(string message);
    public class ServiceQueryEngineAsync
    {
        public ServiceQueryEngineAsync()
        {
            IsBusy = false;
        }
        public bool IsBusy { get; set; }

        #region Events
        public event ServicesStatusCheckDelegate ServicesStatusCheckComplete;
        private void RaiseServicesStatusCheckComplete()
        {
            if (ServicesStatusCheckComplete != null)
                ServicesStatusCheckComplete();
        }
        public event ServicesStatusCheckErrorDelegate ServicesStatusCheckError;
        private void RaiseServicesStatusCheckError(string message)
        {
            if (ServicesStatusCheckError != null)
                ServicesStatusCheckError(message);
        } 
        #endregion

        public void RefreshServiceListAsync(List<ServiceDisplayItem> serviceList)
        {
            IsBusy = true;
            //Fire and forget ;) Use threadpool to schedule execution of refresh
            System.Threading.ThreadPool.QueueUserWorkItem(
                    new System.Threading.WaitCallback(ServicesStatusCheck), serviceList);
        }

        private void ServicesStatusCheck(object objList)
        {            
            List<ServiceDisplayItem> serviceList = (List<ServiceDisplayItem>)objList;            
            foreach (ServiceDisplayItem sdi in serviceList)
            {
                if (sdi != null)    
                    sdi.LastStatus = ServiceControllerStatusEx.Unknown;
            }

            foreach (string hostName in (from ServiceDisplayItem s in serviceList
                                         where s != null && s.Enabled
                                         group s by s.HostName into h
                                         select h.Key))
            {
                try
                {
                    //bool isPingable = false;
                    //if (hostName.ToLower() == "localhost" || hostName.ToLower() == System.Net.Dns.GetHostName().ToLower())
                    //{
                    //    isPingable = true;
                    //}
                    //else 
                    //{
                    //    try
                    //    {
                    //        using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
                    //        {
                    //            System.Net.NetworkInformation.PingReply reply = ping.Send(hostName, 2000);
                    //            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    //                isPingable = true;
                    //        }
                    //    }
                    //    catch { }
                    //}
                    if (PingUtil.Ping(hostName))
                    {
                        ServiceController[] queriedServices = ServiceController.GetServices(hostName);
                        foreach (ServiceDisplayItem sdi in (from s in serviceList
                                                            where s.HostName == hostName
                                                            select s))
                        {
                            if ((from qs in queriedServices
                                 where qs.DisplayName == sdi.DisplayName
                                 select qs).Count() == 1)
                            {
                                ServiceController service = (from qs in queriedServices
                                                             where qs.DisplayName == sdi.DisplayName
                                                             select qs).First();
                                sdi.LastStatus = (ServiceControllerStatusEx)service.Status;
                            }
                        }
                    }
                    else
                    {
                        foreach (ServiceDisplayItem sdi in (from s in serviceList
                                                            where s.HostName == hostName
                                                            select s))
                        {
                            sdi.LastStatus = ServiceControllerStatusEx.Unknown;
                        }
                    }
                }
                catch (Exception hostQueryEx)
                {
                    RaiseServicesStatusCheckError(hostQueryEx.Message);
                }
            }            

            IsBusy = false;
            RaiseServicesStatusCheckComplete();
        }
    }
}
