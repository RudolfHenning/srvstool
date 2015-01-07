using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrvsTool
{
    public static class PingUtil
    {
        public static bool Ping(string hostName)
        {
            bool isPingable = false;
            if (hostName.ToLower() == "localhost" || hostName.ToLower() == System.Net.Dns.GetHostName().ToLower())
            {
                isPingable = true;
            }
            else
            {
                try
                {
                    using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
                    {
                        System.Net.NetworkInformation.PingReply reply = ping.Send(hostName, 2000);
                        if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                            isPingable = true;
                    }
                }
                catch { }
            }
            return isPingable;
        }
    }
}
