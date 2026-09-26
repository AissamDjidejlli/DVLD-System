using DVLD.Validate;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        public delegate void DataBackEventHandler(object sender, int UserID);
        public event DataBackEventHandler DataBack;
        
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            ctrlUserCard1.LoadData(UserID);
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {

        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassword.Text != ctrlUserCard1.User.Password.ToString())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "ConfirmPassword Must match with Newpassword!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void frmChangePassword_Load_1(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlUserCard1.User.Password = txtNewPassword.Text;


            if (ctrlUserCard1.User.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, ctrlUserCard1.User.UserID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlUserCard1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
