namespace SrvsTool
{
    partial class WaitForServiceChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitForServiceChange));
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.lblTimePassed = new System.Windows.Forms.Label();
            this.pblStatus = new SrvsTool.Controls.PBLabel();
            this.SuspendLayout();
            // 
            // timerProgress
            // 
            this.timerProgress.Interval = 1000;
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // lblTimePassed
            // 
            this.lblTimePassed.AutoSize = true;
            this.lblTimePassed.Location = new System.Drawing.Point(0, 0);
            this.lblTimePassed.Name = "lblTimePassed";
            this.lblTimePassed.Size = new System.Drawing.Size(26, 13);
            this.lblTimePassed.TabIndex = 1;
            this.lblTimePassed.Text = "time";
            this.lblTimePassed.Visible = false;
            // 
            // pblStatus
            // 
            this.pblStatus.BackColor = System.Drawing.Color.White;
            this.pblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pblStatus.EndColor = System.Drawing.Color.PaleGreen;
            this.pblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pblStatus.Location = new System.Drawing.Point(0, 0);
            this.pblStatus.MiddelColor = System.Drawing.Color.LimeGreen;
            this.pblStatus.Name = "pblStatus";
            this.pblStatus.Percentage = 0;
            this.pblStatus.Size = new System.Drawing.Size(292, 47);
            this.pblStatus.StartColor = System.Drawing.Color.SpringGreen;
            this.pblStatus.TabIndex = 0;
            this.pblStatus.Text = "Service...";
            // 
            // WaitForServiceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 47);
            this.ControlBox = false;
            this.Controls.Add(this.lblTimePassed);
            this.Controls.Add(this.pblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitForServiceChange";
            this.Text = "WaitForServiceChange";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaitForServiceChange_FormClosed);
            this.Load += new System.EventHandler(this.WaitForServiceChange_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WaitForServiceChange_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SrvsTool.Controls.PBLabel pblStatus;
        private System.Windows.Forms.Timer timerProgress;
        private System.Windows.Forms.Label lblTimePassed;
    }
}