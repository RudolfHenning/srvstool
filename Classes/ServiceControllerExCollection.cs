using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrvsTool
{
    class ServiceControllerExCollection : List<IServiceControllerEx>
    {
        /// <summary>
        /// Get an instance of an IServiceControllerEx based on the name
        /// </summary>
        /// <param name="serviceName">Display name of service</param>
        /// <returns>Instance of an IServiceControllerEx</returns>
        public IServiceControllerEx GetServiceByName(string serviceName)
        {
            return (from IServiceControllerEx service in this
                    where service.DisplayName == serviceName
                    select service).First();
        }
    }
}
