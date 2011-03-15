using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrvsTool
{
    public class ServiceDisplayItem : IComparable
    {
        public ServiceDisplayItem()
        {
            Enabled = false;
            LastStatus = ServiceControllerStatusEx.Unknown;
        }

        public string ServiceName { get; set; }
        public string HostName { get; set; }
        public bool Enabled { get; set; }
        public string DisplayName { get; set; }

        public ServiceControllerStatusEx LastStatus { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }

        #region IComparable Members
        public int CompareTo(object obj)
        {
            ServiceDisplayItem compareToObj = (ServiceDisplayItem)obj;
            string string1 = HostName + DisplayName;
            string string2 = compareToObj.HostName + compareToObj.DisplayName;
            return string1.CompareTo(string2);
        }
        public override bool Equals(object obj)
        {
            ServiceDisplayItem compareToObj = (ServiceDisplayItem)obj;
            string string1 = HostName + DisplayName;
            string string2 = compareToObj.HostName + compareToObj.DisplayName;
            return string1.Equals(string2);
        }
        public static bool operator == (ServiceDisplayItem sdi1, ServiceDisplayItem sdi2)
        {
            string string1 = sdi1.HostName + sdi1.DisplayName;
            string string2 = sdi2.HostName + sdi2.DisplayName;

            return string1 == string2;
        }
        public static bool operator != (ServiceDisplayItem sdi1, ServiceDisplayItem sdi2)
        {
            string string1 = sdi1.HostName + sdi1.DisplayName;
            string string2 = sdi2.HostName + sdi2.DisplayName;

            return string1 != string2;
        }
        public override int GetHashCode()
        {
            string str = HostName + DisplayName;
            return str.GetHashCode();
        }
        #endregion
    }
}
