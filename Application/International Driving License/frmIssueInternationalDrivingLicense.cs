using DVLD.Licenses;
using DVLD.Licenses.Interantional_Driving_License;
using DVLD.Licenses.Local_Driving_License;
using DVLD.Licenses.Local_Driving_License.Controls;
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

namespace DVLD.Application.International_Driving_License.Controls
{
    public partial class frmIssueInternationalDrivingLicense : Form
    {
        private clsLicenses _License;

        private clsInternationalDrivingLicense _InternationalDrivingLicense;

        private clsApplication _Application;

        public frmIssueInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private void gpApplicationInfo_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlLocalDrivingLicenseByFilter1_Load(object sender, EventArgs e)
        {

        }
        

        private void frmIssueInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString();
            
            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
        }



        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (!clsInternationalDrivingLicense.HasDriverInternationalLicense(_License.DriverID))
            {
                btnIssueLicense.Enabled = true;
                _InternationalDrivingLicense = new clsInternationalDrivingLicense();
                _Application = new clsApplication();
            }
            else
            {
                MessageBox.Show("International License Already Exist for this this License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!_License.IsActive || !clsLicenses.HasPersonLicenseFromSameClasse(_License.Driver.PersonID,3))
            {
                MessageBox.Show("License Didn't Work", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _InternationalDrivingLicense.DriverID = _License.DriverID;
            _InternationalDrivingLicense.IssuedUsingLocalLicenseID = _License.LicenseID;
            _InternationalDrivingLicense.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            _Application.PersonID = _License.Application.PersonID;
            _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            _Application.Fees = clsApplicationType.Find(_Application.ApplicationTypeID).Fees;
            _Application.UserID = clsGlobalSetting.CurrentUser.UserID;

            if(_Application.Save())
            {
                _InternationalDrivingLicense.ApplicationID = _Application.ApplicationID;

                if (_InternationalDrivingLicense.Save())
                {
                    MessageBox.Show("License Issued Successfully with ID =" + _InternationalDrivingLicense.InternationalLicenseID.ToString(), "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblApplicationID.Text = _InternationalDrivingLicense.ApplicationID.ToString();
                   

                    lblInternationalLicenseID.Text = _InternationalDrivingLicense.InternationalLicenseID.ToString();

                    btnIssueLicense.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                }
            }

            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void btnFind_Click(object sender, EventArgs e)
        {
        
        
            ctrlLocalDrivingInfo1.LoadData(Convert.ToInt16(txtLicenseID.Text));
        
            
            _License = ctrlLocalDrivingInfo1.License;



            if (_License == null)
            {
                return;
                
            }

            btnIssueLicense.Enabled = true;
            lblLocalLicenseID.Text = _License.LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            if (clsInternationalDrivingLicense.HasDriverInternationalLicense(_License.DriverID))
            {
                llShowLicenseInfo.Enabled = true;
                _InternationalDrivingLicense = clsInternationalDrivingLicense.FindByDriverID(_License.DriverID);
                lblInternationalLicenseID.Text = _InternationalDrivingLicense.InternationalLicenseID.ToString();
                lblApplicationID.Text = _InternationalDrivingLicense.ApplicationID.ToString();
            }
        }

        private void lblFees_Click(object sender, EventArgs e)
        {

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalDrivingLicenseInfocs frm = new frmShowInternationalDrivingLicenseInfocs(_InternationalDrivingLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_InternationalDrivingLicense.IssuedUsingLocalLicense.Application.PersonInfo.NationalNo);
            frm.ShowDialog();
        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }
    }
}
