using DVLD.GlobalClasses;
using DVLD.Properties;
using DVLD.Validate;
using DVLD_Buisness;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace DVLD.People
{
    public partial class frmAddUpdatePeople : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        private clsPerson _Person;
        private int _PersonID = -1;

        private enum enMode { Add = 0, Edit = 1 };
        enMode Mode;

        public frmAddUpdatePeople()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }

        public frmAddUpdatePeople(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;    
            Mode = enMode.Edit;         
        }

        private short _ReturnGendor()
        {
            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
                return 0;
               
            }
            else
            {
                pbPersonImage.Image = Image.FromFile("C:\\Users\\HP 840 G6\\Desktop\\Road Map\\Course 19\\Doctor Solution\\Icons\\Female 512.png");
                return 1;
                
            }
        }

        private DataTable _ListCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            return dt;
        }
        private void _FillComboBox()
        {
            cbCountry.DataSource = _ListCountries();
            cbCountry.DisplayMember = "CountryName";
        }

        private void _LoadPersonInfo()
        {

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.FullName();
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = _ReturnGendor();
            _Person.Address = txtAddress.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagPath = pbPersonImage.ImageLocation;
            else
                _Person.ImagPath = "";



        }

        private void _LoadPersonInfoTo()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtNationalNo.Text = _Person.NationalNo;

            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);

            if(_Person.ImagPath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagPath;
            }

            llRemoveImage.Visible = _Person.ImagPath != "";
        }

        private void _DefaultValues()
        {

            if (Mode == enMode.Add)
            {

                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
                
            }
            else
            {
                lblTitle.Text = "Update Person";
            }


            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            cbCountry.SelectedIndex = cbCountry.FindString("Algeria");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            
            rbMale.Checked = true;
            _ReturnGendor();
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

            
        }


        private bool _HandlePersonImage()
        {
            if(pbPersonImage.ImageLocation != _Person.ImagPath)
            {
                if(_Person.ImagPath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagPath);
                    }
                    catch (IOException)
                    {

                    }
                }


                if(pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation;

                    if(clsUtil.CopyImageToProjectImageFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }



        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CheckIfBoxesFill(object sender, CancelEventArgs e)
        {
            if (!clsValidate.IsValidFill((TextBox)sender, errorProvider1))
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError((TextBox)sender, null);
            }
        }
        

        private void txtFirstName_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidate.IsValidEmail(txtEmail, errorProvider1))
            {
                e.Cancel = true; 
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null); //
            }
        }

        private void dtpDateOfBirth_Validating(object sender, CancelEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmAddUpdatePeople_Load(object sender, EventArgs e)
        {
            clsValidate.IsValidAge(dtpDateOfBirth);
            _FillComboBox();

            _DefaultValues();

            if (Mode == enMode.Edit)
            {
                _LoadPersonInfoTo();
                txtNationalNo.Enabled = false;
            }


        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _ReturnGendor();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            _ReturnGendor();
        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

           

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
                return;

            _LoadPersonInfo();


            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            pbPersonImage.SizeMode = PictureBoxSizeMode.StretchImage;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               pbPersonImage.Load(openFileDialog1.FileName);
                llRemoveImage.Visible = true;
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image = null;
            _ReturnGendor();
            llRemoveImage.Visible = false;
        }

        private void CheckIfBoxesFill(object sender, EventArgs e)
        {

        }
    }
}
