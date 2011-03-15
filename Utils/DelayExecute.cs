using System;
using System.Windows.Forms;
using System.Threading;

namespace SrvsTool
{
    public static class DelayExecute
    {
        ///
        /// An instance of DelegateWrapper which calls InvokeWrappedDelegate,
        /// which in turn calls the DynamicInvoke method of the wrapped
        /// delegate.
        ///
        private static DelegateWrapper wrapperInstance = DelayExecute.InvokeWrappedDelegate;

        ///
        /// Callback used to call EndInvoke on the asynchronously
        /// invoked DelegateWrapper.
        ///
        private static AsyncCallback callback = DelayExecute.EndWrapperInvoke;

        ///
        /// Delegate to wrap another delegate and its arguments
        ///
        private delegate void DelegateWrapper(Form hostForm, int delayTimeout, Delegate d, object[] args);

        ///
        /// Executes the specified delegate with the specified arguments
        /// asynchronously on a thread pool thread.
        ///
        public static void Execute(Form hostForm, int delayTimeout, Delegate d, params object[] args)
        {
            // Invoke the wrapper asynchronously, which will then
            // execute the wrapped delegate synchronously (in the thread pool thread)
            wrapperInstance.BeginInvoke(hostForm, delayTimeout, d, args, callback, null);
        }

        public static void Execute(Form hostForm, int delayTimeout, Delegate d)
        {
            // Invoke the wrapper asynchronously, which will then
            // execute the wrapped delegate synchronously (in the thread pool thread)
            wrapperInstance.BeginInvoke(hostForm, delayTimeout, d, null, callback, null);
        }

        ///
        /// Invokes the wrapped delegate synchronously
        ///
        private static void InvokeWrappedDelegate(Form hostForm, int delayTimeout, Delegate d, object[] args)
        {
            Thread.Sleep(delayTimeout);

            if (hostForm.InvokeRequired)
            {
                hostForm.Invoke(d, args);
            }
            else
            {
                d.DynamicInvoke(args);
            }
        }

        ///
        /// Calls EndInvoke on the wrapper and Close on the resulting WaitHandle
        /// to prevent resource leaks.
        ///
        private static void EndWrapperInvoke(IAsyncResult ar)
        {
            wrapperInstance.EndInvoke(ar);
            ar.AsyncWaitHandle.Close();
        }
    }
}
