using System;
using System.Linq;
using System.Windows.Forms;

namespace SrvsTool
{
    public partial class RecentFileList : FadeForm
    {
        public RecentFileList()
        {
            InitializeComponent();
        }

        #region Properties
        public string SelectedServiceListFile { get; set; } 
        #endregion

        #region Form events
        private void RecentFileList_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(RecentFileList_Resize);
            RecentFileList_Resize(sender, e);


            foreach (string serviceListFile in (from string s in Properties.Settings.Default.RecentFileList
                                                where !s.EndsWith("LastSrvsToolList.srvlst")
                                                select s))
            {
                lvwServiceListFiles.Items.Add(new ListViewItem(serviceListFile) { ImageIndex = 0 });
            }
        }
        private void RecentFileList_Resize(object sender, EventArgs e)
        {
            lvwServiceListFiles.Columns[0].Width = lvwServiceListFiles.ClientSize.Width;
        } 
        #endregion

        #region ListView events
        private void lvwServiceListFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = (lvwServiceListFiles.SelectedItems.Count > 0);
        }
        private void lvwServiceListFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveRecentFileEntry();
            }
        } 
        #endregion

        #region Button events
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialogAdd.ShowDialog() == DialogResult.OK)
            {
                foreach (string serviceListFile in openFileDialogAdd.FileNames)
                {
                    lvwServiceListFiles.Items.Add(new ListViewItem(serviceListFile) { ImageIndex = 0 });
                }
            }
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (lvwServiceListFiles.SelectedItems.Count > 0)
            {
                SelectedServiceListFile = lvwServiceListFiles.SelectedItems[0].Text;
            }
            Properties.Settings.Default.RecentFileList = new System.Collections.Specialized.StringCollection();
            foreach (ListViewItem lvi in lvwServiceListFiles.Items)
            {
                Properties.Settings.Default.RecentFileList.Add(lvi.Text);
            }
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        } 
        #endregion

        #region Private methods
        private void RemoveRecentFileEntry()
        {
            if (MessageBox.Show("Are you sure you want to remove this item(s)?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in lvwServiceListFiles.SelectedItems)
                {
                    lvwServiceListFiles.Items.Remove(lvi);
                }
            }
        } 
        #endregion

    }
}
