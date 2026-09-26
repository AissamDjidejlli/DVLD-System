namespace DVLD.Licenses.Interantional_Driving_License
{
    partial class frmShowInternationalDrivingLicenseInfocs
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
            this.ctrlInternationalDrivingLicenseCardInfo1 = new DVLD.Licenses.Interantional_Driving_License.ctrlInternationalDrivingLicenseCardInfo();
            this.SuspendLayout();
            // 
            // ctrlInternationalDrivingLicenseCardInfo1
            // 
            this.ctrlInternationalDrivingLicenseCardInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlInternationalDrivingLicenseCardInfo1.Name = "ctrlInternationalDrivingLicenseCardInfo1";
            this.ctrlInternationalDrivingLicenseCardInfo1.Size = new System.Drawing.Size(938, 276);
            this.ctrlInternationalDrivingLicenseCardInfo1.TabIndex = 0;
            this.ctrlInternationalDrivingLicenseCardInfo1.Load += new System.EventHandler(this.ctrlInternationalDrivingLicenseCardInfo1_Load);
            // 
            // frmShowInternationalDrivingLicenseInfocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 299);
            this.Controls.Add(this.ctrlInternationalDrivingLicenseCardInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowInternationalDrivingLicenseInfocs";
            this.Text = "Show International Driving License Info";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlInternationalDrivingLicenseCardInfo ctrlInternationalDrivingLicenseCardInfo1;
    }
}