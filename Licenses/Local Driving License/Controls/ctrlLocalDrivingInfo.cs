using DVLD.Application.Controls;
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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlLocalDrivingInfo : UserControl
    {
        private string _NationalNo = string.Empty;

        private clsPerson _Person;
        public clsPerson Person
        {
            get { return _Person; }
        }


        private clsDrivers _Driver;
        public clsDrivers Driver
        {
            get { return _Driver; }
        }



        private clsLicenses _License;
        public clsLicenses License
        {
            get { return _License; }
        }

        public ctrlLocalDrivingInfo()
        {
            InitializeComponent();
        }

        public ctrlLocalDrivingInfo(string NationalNo)
        {
            InitializeComponent();
            _NationalNo = NationalNo;
        }


        public void LoadData(string NationalNo)
        {
            _NationalNo = NationalNo;
            _Person = clsPerson.Find(_NationalNo);
            _Driver = clsDrivers.FindByPersonID(_Person.PersonID);
            _License = clsLicenses.FindByDriverID(_Driver.DriverID);


            lblClass.Text = _License.LicenseClass.Name;
            lblFullName.Text = _License.Driver.PersonInfo.FullName();
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = NationalNo;

            if (_License.Driver.PersonInfo.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            lblIssueDate.Text = _License.IssueDate.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();  
            lblNotes.Text = _License.Notes.ToString();
            lblIsActive.Text = _License.IsActive.ToString();
            lblDateOfBirth.Text = _License.Driver.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _License.Driver.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();
            lblIsDetained.Text = "No";
        }

        public void LoadData(int LicenseID)
        {

            _License = clsLicenses.FindByID(LicenseID);
            

            if (_License == null)
            {
                MessageBox.Show("License Doesn't Exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            lblClass.Text = _License.LicenseClass.Name;
            lblFullName.Text = _License.Driver.PersonInfo.FullName();
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.Driver.PersonInfo.NationalNo.ToString();

            if (_License.Driver.PersonInfo.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            lblIssueDate.Text = _License.IssueDate.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblNotes.Text = _License.Notes.ToString();
            lblIsActive.Text = _License.IsActive.ToString();
            lblDateOfBirth.Text = _License.Driver.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _License.Driver.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();

            if(clsDetainReleaseLicense.IsLicenseDetained(_License.LicenseID))
                lblIsDetained.Text = "Yes";
            else
                lblIsDetained.Text = "No";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlLocalDrivingInfo_Load(object sender, EventArgs e)
        {

        }

        private void lblLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblIsDetained_Click(object sender, EventArgs e)
        {

        }
    }
}
