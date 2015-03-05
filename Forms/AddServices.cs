using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;

namespace SrvsTool
{
    public partial class AddServices : FadeForm
    {
        public AddServices()
        {
            InitializeComponent();
        }

        #region Properties
        private string machine = ".";
        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }
        private System.Collections.Specialized.StringCollection selectedServices;
        public System.Collections.Specialized.StringCollection SelectedServices
        {
            get { return selectedServices; }
        }
        private List<ServiceDisplayItem> excludeList = new List<ServiceDisplayItem>();
        public List<ServiceDisplayItem> ExcludeList
        {
            get { return excludeList; }
        }

        #endregion

        public DialogResult ShowDialog(string machine)
        {
            this.machine = machine;
            return this.ShowDialog();
        }

        private void txtMachine_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadServiceList();
            }
        }

        private void LoadServiceList(string machineName)
        {
            machineName = machineName.ToUpper();
            lstServices.Items.AddRange
                    (
                        (from ServiceController controller in ServiceController.GetServices(machineName)
                         where (
                                    txtFilter.Text.Length == 0 ||
                                    controller.DisplayName.ToUpper().Contains(txtFilter.Text.ToUpper())
                                ) &&
                                !ExcludeItem(controller.MachineName, controller.DisplayName)
                         select controller.MachineName + "\\" + controller.DisplayName).ToArray()
                    );
        }

        private void LoadServiceList()
        {
            cmdOK.Enabled = false;
            if (txtMachine.Text.Length == 0)
                txtMachine.Text = System.Net.Dns.GetHostName();
            if ((txtMachine.Text == ".") || (txtMachine.Text == "localhost"))
                txtMachine.Text = System.Net.Dns.GetHostName();
            machine = txtMachine.Text.ToUpper();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lstServices.Items.Clear();
                foreach (string machineName in machine.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    LoadServiceList(machineName.Trim());
                }
                //lstServices.Items.AddRange
                //    (
                //        (from ServiceController controller in ServiceController.GetServices(machine)
                //         where (
                //                    txtFilter.Text.Length == 0 ||
                //                    controller.DisplayName.ToUpper().Contains(txtFilter.Text.ToUpper())
                //                ) &&
                //                !ExcludeItem(controller.MachineName, controller.DisplayName)
                //         select controller.DisplayName).ToArray()
                //    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ExcludeItem(string machineName, string displayName)
        {
            return (from ex in excludeList
                    where ex.HostName.Equals(machineName, StringComparison.InvariantCultureIgnoreCase) && ex.DisplayName == displayName
                    select ex).Count() > 0;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (lstServices.SelectedItems.Count > 0)
            {
                selectedServices = new System.Collections.Specialized.StringCollection();
                selectedServices.AddRange
                    (
                        (from object lstItem in lstServices.SelectedItems
                         select lstItem.ToString()).ToArray()
                    );
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdLoadList_Click(object sender, EventArgs e)
        {
            LoadServiceList();
        }

        private void AddServices_Load(object sender, EventArgs e)
        {
            txtMachine.Text = machine;
            //Only start monitoring after load event is done
            new System.Threading.Thread(delegate()
            {
                lstServices.Invoke((MethodInvoker)delegate()
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);

                    LoadServiceList();
                }
                );
            }
            ).Start();
        }

        private void lstServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = (lstServices.SelectedItems.Count > 0);

        }

        private void txtMachine_EnterKeyPressed()
        {
            LoadServiceList();
        }

        private void txtFilter_EnterKeyPressed()
        {
            LoadServiceList();
        }
    }
}