using DVLD.Validate;
using DVLD_Buisness;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        private  clsUser _User;
        private int _UserID = -1;

        private clsPerson _Person;
        private enum enMode { Add = 0, Edit = 1 };
        enMode Mode;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            
            Mode = enMode.Edit;
        }

        private void _DefaultValues()
        {

            if (Mode == enMode.Add)
            {

                lblTitle.Text = "Add New User";
                _User = new clsUser();

            }
            else
            {
                lblTitle.Text = "Update User";
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;


        }


        private void _LoadUserInfo()
        {
            _Person = ctrlPersonCardWithFilter1.PersonInfo;
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;   
        }

        private void _LoadUserInfoTo()
        {
            _User = clsUser.Find(_UserID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;

            ctrlPersonCardWithFilter1.LoadData(_User.PersonID);

            btnSave.Enabled = true;
        }


        private void CheckIfBoxesFill(object sender, CancelEventArgs e)
        {
            if (!clsValidate.IsValidFill((TextBox)sender, errorProvider1))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError((TextBox)sender, null);
            }

            
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _DefaultValues();

            if (Mode == enMode.Edit)
            {
                _LoadUserInfoTo();
            }

            if (!this.ValidateChildren())
            {
                btnSave.Enabled = true;
                return;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Mode == enMode.Add && clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
            {
                MessageBox.Show("User was already exist", "User Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _LoadUserInfo();


                if (_User.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this, _User.UserID);
                    lblUserID.Text = _User.UserID.ToString();    
                }
                else
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            tcUserInfo.SelectedIndex = 1;
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "ConfirmPassword Must match with password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcUserInfo.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tpPersonalInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
