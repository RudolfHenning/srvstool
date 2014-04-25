using System.Linq;
using System.ServiceProcess;

namespace SrvsTool
{
    static class ServiceQueryFactory
    {
        public static ServiceControllerExCollection GetServices(ServiceQueryType serviceQueryType, string machineName)
        {
            if ((machineName.Length == 0) || (machineName == "localhost"))
                machineName = System.Net.Dns.GetHostName();

            if (serviceQueryType == ServiceQueryType.ServiceProcess)
            {
                ServiceControllerExCollection list = new ServiceControllerExCollection();
                try
                {
                    list.AddRange
                        (
                            (
                                from ServiceController myController in ServiceController.GetServices(machineName)
                                select new ServiceControllerExSp(myController)
                             ).ToArray()
                        );
                }
                catch (System.InvalidOperationException invalidEx)
                {
                    if (invalidEx.Message.Contains("Cannot open Service Control Manager on computer"))
                    {
                        string fullDNSName = System.Net.Dns.GetHostEntry(machineName).HostName;
                        list.AddRange
                            (
                                (
                                    from ServiceController myController in ServiceController.GetServices(fullDNSName)
                                    select new ServiceControllerExSp(myController)
                                 ).ToArray()
                            );
                    }
                    else
                        throw;
                }
                return list;
            }
            else if (serviceQueryType == ServiceQueryType.WMI)
            {
                return ServiceControllerExWMI.GetServices(machineName, false, "", "");
            }
            else
            {
                return ServiceControllerExMix.GetServices(machineName, false, "", "");
            }
        }

        public static IServiceControllerEx GetService(ServiceQueryType serviceQueryType, string machineName, string serviceName)
        {
            IServiceControllerEx service = null;
            if ((machineName.Length == 0) || (machineName == "localhost"))
                machineName = System.Net.Dns.GetHostName();
            if (serviceQueryType == ServiceQueryType.ServiceProcess)
            {
                service = new ServiceControllerExSp(new ServiceController(serviceName, machineName));
            }
            else if (serviceQueryType == ServiceQueryType.WMI)
            {
                service = (from IServiceControllerEx srvc in ServiceControllerExWMI.GetServices(machineName, false, "", "")
                           where srvc.ServiceName == serviceName
                           select srvc).First();
            }
            else
            {
                service = (from IServiceControllerEx srvc in ServiceControllerExMix.GetServices(machineName, false, "", "")
                           where srvc.ServiceName == serviceName
                           select srvc).First();
            }
            return service;
        }
    }
}
