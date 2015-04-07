using SrvsTool.Controls;
namespace SrvsTool
{
    partial class AddServices
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServices));
            this.label1 = new System.Windows.Forms.Label();
            this.txtMachine = new SrvsTool.Controls.TextBoxEx();
            this.lstServices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdLoadList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilter = new SrvsTool.Controls.TextBoxEx();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Machines";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTip1.SetToolTip(this.label1, "Use comma to separate machine names");
            // 
            // txtMachine
            // 
            this.txtMachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachine.Location = new System.Drawing.Point(72, 6);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(262, 20);
            this.txtMachine.TabIndex = 1;
            this.txtMachine.EnterKeyPressed += new System.Windows.Forms.MethodInvoker(this.txtMachine_EnterKeyPressed);
            this.txtMachine.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMachine_KeyUp);
            // 
            // lstServices
            // 
            this.lstServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServices.FormattingEnabled = true;
            this.lstServices.Location = new System.Drawing.Point(72, 58);
            this.lstServices.Name = "lstServices";
            this.lstServices.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstServices.Size = new System.Drawing.Size(310, 186);
            this.lstServices.Sorted = true;
            this.lstServices.TabIndex = 6;
            this.lstServices.SelectedIndexChanged += new System.EventHandler(this.lstServices_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Services";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdOK.Enabled = false;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdOK.Location = new System.Drawing.Point(226, 258);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 7;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCancel.Location = new System.Drawing.Point(307, 258);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            // 
            // cmdLoadList
            // 
            this.cmdLoadList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdLoadList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdLoadList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLoadList.Location = new System.Drawing.Point(340, 6);
            this.cmdLoadList.Name = "cmdLoadList";
            this.cmdLoadList.Size = new System.Drawing.Size(42, 46);
            this.cmdLoadList.TabIndex = 4;
            this.cmdLoadList.Text = "Go";
            this.cmdLoadList.UseVisualStyleBackColor = false;
            this.cmdLoadList.Click += new System.EventHandler(this.cmdLoadList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(23, 35);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Filter";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(72, 32);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(262, 20);
            this.txtFilter.TabIndex = 3;
            this.txtFilter.EnterKeyPressed += new System.Windows.Forms.MethodInvoker(this.txtFilter_EnterKeyPressed);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // AddServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(394, 293);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lstServices);
            this.Controls.Add(this.cmdLoadList);
            this.Controls.Add(this.txtMachine);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddServices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Services";
            this.Load += new System.EventHandler(this.AddServices_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TextBoxEx txtMachine;
        private System.Windows.Forms.ListBox lstServices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdLoadList;
        private System.Windows.Forms.Label label3;
        private TextBoxEx txtFilter;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}