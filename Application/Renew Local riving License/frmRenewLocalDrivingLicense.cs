using DVLD.Licenses;
using DVLD.Licenses.Controls;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Application.Renew_Local_riving_License
{
    public partial class frmRenewLocalDrivingLicense : Form
    {

        private clsLicenses _OldLicense;

        private clsLicenses _CurrentLicense;

        private clsLicenses _RenewLicense;

        private clsApplication _Application;
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void lblOldLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalFees_Click(object sender, EventArgs e)
        {

        }

        private void lblRenewedLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void lblExpirationDate_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblCreatedByUser_Click(object sender, EventArgs e)
        {

        }

        private void gpApplicationInfo_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlLocalDrivingLicenseByFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString();

            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_CurrentLicense.Driver.PersonInfo.NationalNo);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverCardInfo frm = new frmShowDriverCardInfo(_CurrentLicense.LicenseID);
            frm.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            ctrlLocalDrivingInfo1.LoadData(Convert.ToInt16(txtLicenseID.Text));
            _CurrentLicense = ctrlLocalDrivingInfo1.License;

            if (_CurrentLicense == null)
            {
                return;
            }

            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            lblOldLicenseID.Text = _CurrentLicense.LicenseID.ToString();

            lblLicenseFees.Text = clsLicenseClasses.Find(_CurrentLicense.LicenseClassID).Fees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();


            if (_CurrentLicense.IsActive)
            {
                MessageBox.Show("Error:License Is Active you can't renew it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;

            }

            else if (!_CurrentLicense.IsActive)
            {
                MessageBox.Show("Error:License Is Desactivated you can't edit it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
            }


            else if (_CurrentLicense.ExpirationDate > DateTime.Now.Date)
            {
                MessageBox.Show("Error:License Is Active you can't renew it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
            }


            else
            {
                 _OldLicense = _CurrentLicense;
            
                 btnRenewLicense.Enabled = true;
            
                
            
                 lblApplicationID.Text = _OldLicense.ApplicationID.ToString();
                 lblRenewedLicenseID.Text = _OldLicense.LicenseID.ToString();
                
            }
        

        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            _RenewLicense = new clsLicenses();
            _Application = new clsApplication();


           
            _RenewLicense.DriverID = _OldLicense.DriverID;
            _RenewLicense.LicenseClassID = _OldLicense.LicenseClassID;
            _RenewLicense.Notes = txtNotes.Text;
            _RenewLicense.PaidFees = Convert.ToInt16(lblTotalFees.Text);
            _RenewLicense.IssueReason = clsLicenses.enIssueReason.Renew;
            _RenewLicense.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            _Application.PersonID = _OldLicense.Application.PersonID;
            _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            _Application.Fees = clsApplicationType.Find(_Application.ApplicationTypeID).Fees;
            _Application.UserID = clsGlobalSetting.CurrentUser.UserID;


            if (_Application.Save())
            {
                _RenewLicense.ApplicationID = _Application.ApplicationID;

                if (_RenewLicense.Save())
                {
                    MessageBox.Show("License Renewed Successfully with ID =" + _RenewLicense.LicenseID.ToString(), "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _OldLicense.DisactivateLicense();

                    _CurrentLicense = _RenewLicense;

                    ctrlLocalDrivingInfo1.LoadData(_CurrentLicense.LicenseID);

                    lblApplicationID.Text = _RenewLicense.ApplicationID.ToString();

                    lblRenewedLicenseID.Text = _RenewLicense.LicenseID.ToString();


                    btnRenewLicense.Enabled = false;
                    llShowLicenseInfo.Enabled = true;


                }
            }

            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
