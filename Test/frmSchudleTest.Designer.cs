namespace DVLD.Test
{
    partial class frmSchudleTest
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
            this.ctrlShudleTest1 = new DVLD.Test.Controls.ctrlShudleTest();
            this.SuspendLayout();
            // 
            // ctrlShudleTest1
            // 
            this.ctrlShudleTest1.Location = new System.Drawing.Point(0, 0);
            this.ctrlShudleTest1.Name = "ctrlShudleTest1";
            this.ctrlShudleTest1.Size = new System.Drawing.Size(550, 675);
            this.ctrlShudleTest1.TabIndex = 0;
            this.ctrlShudleTest1.Load += new System.EventHandler(this.ctrlShudleTest1_Load);
            // 
            // frmSchudleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 672);
            this.Controls.Add(this.ctrlShudleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSchudleTest";
            this.Text = "frmSchudleTest";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlShudleTest ctrlShudleTest1;
    }
}