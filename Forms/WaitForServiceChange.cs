using System;
using System.Drawing;
using System.Windows.Forms;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace SrvsTool
{
    public partial class WaitForServiceChange : Form
    {
        private DateTime startTime;
        private readonly int BORDERSIZE = 20;
        private TaskbarManager windowsTaskbar = TaskbarManager.Instance;

        public WaitForServiceChange()
        {
            InitializeComponent();
        }

        public void Show(string caption, string description)
        {
            this.Text = caption;
            this.pblStatus.Text = description;
            startTime = DateTime.Now;
            this.Show();
        }

        #region Form events
        private void WaitForServiceChange_Load(object sender, EventArgs e)
        {
            timerProgress.Enabled = true;
            EnableServiceStateChangeProgress();
        } 
        private void WaitForServiceChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        #endregion

        #region Timer event
        private void timerProgress_Tick(object sender, EventArgs e)
        {
            int timePassedPerc = (int)((DateTime.Now.Subtract(startTime).TotalSeconds * 100) / 25); //Only go to 25 seconds
            lblTimePassed.Text = timePassedPerc.ToString(); //only used while debugging

            if (timePassedPerc < 100)
                pblStatus.Percentage = timePassedPerc;
            else
                pblStatus.Percentage = 100;
            pblStatus.Refresh();
            Application.DoEvents();
        } 
        #endregion

        #region Private methods
        public void SetWindowSize(string description)
        {
            Graphics g = System.Drawing.Graphics.FromHwnd(this.Handle);
            float width = g.MeasureString(description, pblStatus.Font).Width;
            if (width > (300 - BORDERSIZE))
            {
                this.Width = (int)(width + BORDERSIZE);
            }
            this.Location = new Point(
                (System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Width - this.Width) / 2,
                (System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - this.Height) / 2);
        }

        private void EnableServiceStateChangeProgress()
        {
            if (CoreHelpers.RunningOnWin7)
            {
                TaskbarProgressBarState state = TaskbarProgressBarState.Indeterminate;
                windowsTaskbar.SetProgressState(state);
            }
        }
        private void DisableServiceStateChangeProgress()
        {
            if (CoreHelpers.RunningOnWin7)
            {
                TaskbarProgressBarState state = TaskbarProgressBarState.NoProgress;
                windowsTaskbar.SetProgressState(state);
            }
        }
        #endregion

        private void WaitForServiceChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisableServiceStateChangeProgress();
        }
    }
}
