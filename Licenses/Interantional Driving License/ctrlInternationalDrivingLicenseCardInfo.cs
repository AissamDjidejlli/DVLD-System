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

namespace DVLD.Licenses.Interantional_Driving_License
{
    public partial class ctrlInternationalDrivingLicenseCardInfo : UserControl
    {
        public clsInternationalDrivingLicense InternationalDrivingLicense;
        public ctrlInternationalDrivingLicenseCardInfo()
        {
            InitializeComponent();
        }


        public void LoadData(int InternationalLicenseID)
        {
           

            InternationalDrivingLicense = clsInternationalDrivingLicense.FindByID(InternationalLicenseID);


            if (InternationalDrivingLicense == null)
            {
                MessageBox.Show("International License Doesn't Exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblInternationalLicenseID.Text = InternationalDrivingLicense.InternationalLicenseID.ToString();
            lblFullName.Text = InternationalDrivingLicense.Driver.PersonInfo.FullName();
            lblDateOfBirth.Text = InternationalDrivingLicense.Driver.PersonInfo.DateOfBirth.ToString();
            lblApplicationID.Text = InternationalDrivingLicense.ApplicationID.ToString();
            lblLocalLicenseID.Text = InternationalDrivingLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = InternationalDrivingLicense.Driver.PersonInfo.NationalNo.ToString();
            if (InternationalDrivingLicense.Driver.PersonInfo.Gendor == 1)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            lblIssueDate.Text = InternationalDrivingLicense.IssueDate.ToString();
            lblIsActive.Text = InternationalDrivingLicense.IsActive.ToString();
            lblDriverID.Text = InternationalDrivingLicense.Driver.DriverID.ToString();
            lblExpirationDate.Text = InternationalDrivingLicense.ExpirationDate.ToString();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblGendor_Click(object sender, EventArgs e)
        {

        }
    }
}
