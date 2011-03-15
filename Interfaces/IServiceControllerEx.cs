using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace SrvsTool
{
    interface IServiceControllerEx
    {
        string Account { get; }
        bool CanPause { get; }
        bool CanStart { get; }
        bool CanStop { get; }
        string Description { get; }
        string DisplayName { get; }
        string ExecutablePath { get; }
        string MachineName { get; }
        string ServiceName { get; }
        ServiceStart ServiceStartup { get; }
        ServiceControllerStatus Status { get; }

        void Pause();
        void Refresh();
        void Resume();
        void Start();
        void Stop();
        string ToString();
    }
}
