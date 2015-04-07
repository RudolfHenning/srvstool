namespace SrvsTool
{
    partial class RecentFileList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecentFileList));
            this.lvwServiceListFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialogAdd = new System.Windows.Forms.OpenFileDialog();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvwServiceListFiles
            // 
            this.lvwServiceListFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwServiceListFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwServiceListFiles.Location = new System.Drawing.Point(0, 0);
            this.lvwServiceListFiles.Name = "lvwServiceListFiles";
            this.lvwServiceListFiles.Size = new System.Drawing.Size(417, 225);
            this.lvwServiceListFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwServiceListFiles.TabIndex = 0;
            this.lvwServiceListFiles.UseCompatibleStateImageBehavior = false;
            this.lvwServiceListFiles.View = System.Windows.Forms.View.Details;
            this.lvwServiceListFiles.SelectedIndexChanged += new System.EventHandler(this.lvwServiceListFiles_SelectedIndexChanged);
            this.lvwServiceListFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvwServiceListFiles_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            this.columnHeader1.Width = 100;
            // 
            // openFileDialogAdd
            // 
            this.openFileDialogAdd.DefaultExt = "xml";
            this.openFileDialogAdd.Filter = "Service List Files|*.srvlst";
            this.openFileDialogAdd.Multiselect = true;
            this.openFileDialogAdd.Title = "Add service list file";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCancel.Location = new System.Drawing.Point(330, 231);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAdd.Location = new System.Drawing.Point(12, 231);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = false;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdOK.Enabled = false;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdOK.Location = new System.Drawing.Point(249, 231);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "Load";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // RecentFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(417, 266);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lvwServiceListFiles);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 250);
            this.Name = "RecentFileList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recent service file list";
            this.Load += new System.EventHandler(this.RecentFileList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ListView lvwServiceListFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialogAdd;
    }
}