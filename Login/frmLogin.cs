//using DVLD.Validate;
//using DVLD_Buisness;
//using System;
//using System.ComponentModel;
//using System.Windows.Forms;
//using System.IO;
//using static System.Net.Mime.MediaTypeNames;


//namespace DVLD.Login
//{
//    public partial class frmLogin : Form
//    {
      
//        public frmLogin()
//        {
//            InitializeComponent();
           
//        }

        
//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            Close();
//        }
        
//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            if (!this.ValidateChildren())
//            {
//                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return; 
//            }

//             clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text, txtPassword.Text);

//            if (User != null)
//            {
//                if (!User.IsActive)
//                {
//                    MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (chkRememberMe.Checked)
//                {
//                    clsGlobalSetting.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);
//                }
//                else
//                {
//                    clsGlobalSetting.RememberUsernameAndPassword("","");
//                }

               
//                clsGlobalSetting.CurrentUser = User;
//                this.Hide();
//                frmMain frmMain = new frmMain(this);
//                frmMain.ShowDialog();
                
//            }
//            else
//            {
//                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

            
//        }

//        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
//        {
           
//        }

//        private void frmLogin_Load(object sender, EventArgs e)
//        {
//            string UserName = "", Password = "";
//            if (clsGlobalSetting.GetStoredCredential(ref UserName, ref Password))
//            {
//                txtUserName.Text = UserName;
//                txtPassword.Text = Password;
//                chkRememberMe.Checked = true;
//            }
//            else
//            {
//                chkRememberMe.Checked = false;
//            }
//        }

//        private void txtUserName_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void txtUserName_Validating(object sender, CancelEventArgs e)
//        {
           
//        }

//        private void txtPassword_Validating(object sender, CancelEventArgs e)
//        {
            
//        }
//    }
//}




using DVLD.Validate;
using DVLD_Buisness;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD.Login
{
    public partial class frmLogin : Form
    {

        string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\UserInfo";

        string UserName = "User Name";

        string Password = "Password";

        public frmLogin()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text, txtPassword.Text);

                if (User != null)
                {
                    if (!User.IsActive)
                    {
                        MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

  

                    if (chkRememberMe.Checked)
                    {
                        Registry.SetValue(keyPath, UserName, txtUserName.Text, RegistryValueKind.String);
                        Registry.SetValue(keyPath, Password, txtPassword.Text, RegistryValueKind.String);
                    }
                    else
                    {
                        Registry.SetValue(keyPath, UserName, null, RegistryValueKind.String);
                        Registry.SetValue(keyPath, Password, null, RegistryValueKind.String);
                    }
                }


                clsGlobalSetting.CurrentUser = User;
                this.Hide();
                frmMain frmMain = new frmMain(this);
                frmMain.ShowDialog();


            }

               
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

             
            
          
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserNameV = Registry.GetValue(keyPath, UserName, null) as string;
            string PasswordV = Registry.GetValue(keyPath, Password, null) as string;
            if (UserName != null && Password != null)
            {
                txtUserName.Text = UserNameV;
                txtPassword.Text = PasswordV;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
