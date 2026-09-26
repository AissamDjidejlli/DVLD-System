using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_License;
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

namespace DVLD.Application.Replacement_for_Damaged_or_Lost_License
{
    public partial class frmReplacementforDamagedOrLost : Form
    {
        private clsLicenses _OldLicense;

        private clsLicenses _CurrentLicense;

        private clsLicenses _ReplaceLicense;

        private clsApplication _Application;
        private enum enReplaceMode { Damage = 1,Lost = 2};
        enReplaceMode ReplaceMode;
        public frmReplacementforDamagedOrLost()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            _ReplaceLicense = new clsLicenses();
            _Application = new clsApplication();


           
            _ReplaceLicense.DriverID = _OldLicense.DriverID;
            _ReplaceLicense.LicenseClassID = _OldLicense.LicenseClassID;
            _ReplaceLicense.IssueDate = _OldLicense.IssueDate;
            _ReplaceLicense.ExpirationDate = _OldLicense.ExpirationDate;
            _ReplaceLicense.Notes = _OldLicense.Notes;
            _ReplaceLicense.PaidFees = Convert.ToInt16(lblApplicationFees.Text);

            if (ReplaceMode == enReplaceMode.Damage) { _ReplaceLicense.IssueReason = clsLicenses.enIssueReason.Damage; }
            else { _ReplaceLicense.IssueReason = clsLicenses.enIssueReason.Lost; }

            _ReplaceLicense.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            _Application.PersonID = _OldLicense.Application.PersonID;

            if (ReplaceMode == enReplaceMode.Damage) { _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense; } 
            else { _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense; }

            _Application.Fees = clsApplicationType.Find(_Application.ApplicationTypeID).Fees;
            _Application.UserID = clsGlobalSetting.CurrentUser.UserID;


            if (_Application.Save())
            {
                _ReplaceLicense.ApplicationID = _Application.ApplicationID;

                if (_ReplaceLicense.Save())
                {
                    MessageBox.Show("License Replaced Successfully with ID =" + _ReplaceLicense.LicenseID.ToString(), "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _OldLicense.DisactivateLicense();

                    _CurrentLicense = _ReplaceLicense;

                    ctrlLocalDrivingInfo1.LoadData(_ReplaceLicense.LicenseID);
                    txtLicenseID.Text = _ReplaceLicense.LicenseID.ToString();

                    lblApplicationID.Text = _ReplaceLicense.ApplicationID.ToString();

                    lblRreplacedLicenseID.Text = _ReplaceLicense.LicenseID.ToString();

                    gbFilters.Enabled = false;
                    gbReplacementFor.Enabled = false;
                    btnIssueReplacement.Enabled = false;
                    llShowLicenseInfo.Enabled = true;


                }
            }

            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReplacementforDamagedOrLost_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            lblApplicationDate.Text = DateTime.Now.ToString();
           
            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;

             
            

           
        }

        private void lblRreplacedLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void gpApplicationInfo_Enter(object sender, EventArgs e)
        {

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverCardInfo frm = new frmShowDriverCardInfo(_CurrentLicense.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_CurrentLicense.Driver.PersonInfo.NationalNo);
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

            _OldLicense = _CurrentLicense;

           lblOldLicenseID.Text = _OldLicense.LicenseID.ToString();

           btnIssueReplacement.Enabled = true;

        }

        private void gbReplacementFor_Enter(object sender, EventArgs e)
        {

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                ReplaceMode = enReplaceMode.Damage;
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLostLicense.Checked)
            {
                ReplaceMode = enReplaceMode.Lost;
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
            }

        }
    }
}
