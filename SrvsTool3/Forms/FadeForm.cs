using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SrvsTool
{
    public class FadeForm : Form
    {
        #region Private constants
        private const int AW_HIDE = 0X10000;
        private const int AW_ACTIVATE = 0X20000;
        private const int AW_HOR_POSITIVE = 0X1;
        private const int AW_HOR_NEGATIVE = 0X2;
        private const int AW_SLIDE = 0X40000;
        private const int AW_BLEND = 0X80000;
        private const int FADEINTTIME = 250;
        private const int FADEOUTTIME = 200; 
        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int AnimateWindow
        (IntPtr hwand, int dwTime, int dwFlags);

        private bool useSlideAnimation;
        public FadeForm() : this(false) { }
        public FadeForm(bool useSlideAnimation)
        {
            this.useSlideAnimation = useSlideAnimation;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AnimateWindow(this.Handle, FADEINTTIME, AW_ACTIVATE | (useSlideAnimation ?
                          AW_HOR_POSITIVE | AW_SLIDE : AW_BLEND));
            this.Refresh();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel == false)
            {
                AnimateWindow(this.Handle, FADEOUTTIME, AW_HIDE | (useSlideAnimation ?
                              AW_HOR_NEGATIVE | AW_SLIDE : AW_BLEND));
            }
        }
    }
}
