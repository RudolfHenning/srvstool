using System;
using System.Windows.Forms;

namespace SrvsTool
{
    public partial class OptionsWindow : FadeForm
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        #region Properties
        private int pollingFrequency = 10;
        public int PollingFrequency
        {
            get { return pollingFrequency; }
            set { pollingFrequency = value; }
        }

        private bool vistaTheme = false;
        public bool VistaTheme
        {
            get { return vistaTheme; }
            set { vistaTheme = value; }
        }

        private bool hideonMinimized = false;
        public bool HideonMinimized
        {
            get { return hideonMinimized; }
            set { hideonMinimized = value; }
        } 
        #endregion

        #region Form events
        private void OptionsWindow_Load(object sender, EventArgs e)
        {
            numericUpDownPollingFrequency.Value = pollingFrequency;
            chkHideOnMinimized.Checked = hideonMinimized;
        } 
        #endregion

        #region Button events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            pollingFrequency = (int)numericUpDownPollingFrequency.Value;
            hideonMinimized = chkHideOnMinimized.Checked;
            DialogResult = DialogResult.OK;
            Close();
        } 
        #endregion
    }
}
