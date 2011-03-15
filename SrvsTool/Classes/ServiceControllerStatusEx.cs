
namespace SrvsTool
{
    public enum ServiceControllerStatusEx
    {
        // Summary:
        //     The service is not running. This corresponds to the Win32 SERVICE_STOPPED
        //     constant, which is defined as 0x00000001.
        Stopped = 1,
        //
        // Summary:
        //     The service is starting. This corresponds to the Win32 SERVICE_START_PENDING
        //     constant, which is defined as 0x00000002.
        StartPending = 2,
        //
        // Summary:
        //     The service is stopping. This corresponds to the Win32 SERVICE_STOP_PENDING
        //     constant, which is defined as 0x00000003.
        StopPending = 3,
        //
        // Summary:
        //     The service is running. This corresponds to the Win32 SERVICE_RUNNING constant,
        //     which is defined as 0x00000004.
        Running = 4,
        //
        // Summary:
        //     The service continue is pending. This corresponds to the Win32 SERVICE_CONTINUE_PENDING
        //     constant, which is defined as 0x00000005.
        ContinuePending = 5,
        //
        // Summary:
        //     The service pause is pending. This corresponds to the Win32 SERVICE_PAUSE_PENDING
        //     constant, which is defined as 0x00000006.
        PausePending = 6,
        //
        // Summary:
        //     The service is paused. This corresponds to the Win32 SERVICE_PAUSED constant,
        //     which is defined as 0x00000007.
        Paused = 7,
        //
        // Summary:
        //     The service is in an unknown state. Added for WindowsServiceMonitor app
        Unknown = 8,
    }
}
