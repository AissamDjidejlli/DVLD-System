namespace DVLD.Application.Local_Driving_License
{
    partial class frmDrivingLicenseInfo
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
            this.ctrlLocalDrivingApplicationInfo1 = new DVLD.Application.Controls.ctrlLocalDrivingApplicationInfo();
            this.SuspendLayout();
            // 
            // ctrlLocalDrivingApplicationInfo1
            // 
            this.ctrlLocalDrivingApplicationInfo1.Location = new System.Drawing.Point(3, -4);
            this.ctrlLocalDrivingApplicationInfo1.Name = "ctrlLocalDrivingApplicationInfo1";
            this.ctrlLocalDrivingApplicationInfo1.Size = new System.Drawing.Size(929, 417);
            this.ctrlLocalDrivingApplicationInfo1.TabIndex = 0;
            this.ctrlLocalDrivingApplicationInfo1.Load += new System.EventHandler(this.ctrlLocalDrivingApplicationInfo1_Load);
            // 
            // frmDrivingLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 401);
            this.Controls.Add(this.ctrlLocalDrivingApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmDrivingLicenseInfo";
            this.Text = "Driving License Info";
            this.Load += new System.EventHandler(this.frmDrivingLicenseInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlLocalDrivingApplicationInfo ctrlLocalDrivingApplicationInfo1;
    }
}