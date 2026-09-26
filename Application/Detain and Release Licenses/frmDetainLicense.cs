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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Application.Detain_and_Release_Licenses
{
    
    public partial class frmDetainLicense : Form
    {
        public delegate void DataBackEventHandler(object sender, int DetainID);
        public event DataBackEventHandler DataBack;

        private clsLicenses _CurrentLicense;

        private clsDetainReleaseLicense _DetainLicense;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;
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

            if (clsDetainReleaseLicense.IsLicenseDetained(_CurrentLicense.LicenseID))
            {
                MessageBox.Show("Error: License Is already Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                _DetainLicense = clsDetainReleaseLicense.FindByLicenseID(_CurrentLicense.LicenseID);
                lblDetainID.Text = _DetainLicense.DetainID.ToString();
            }

            else
            {
                btnDetain.Enabled = true;
            }
               
            lblLicenseID.Text = _CurrentLicense.LicenseID.ToString();
            
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _DetainLicense = new clsDetainReleaseLicense();

            _DetainLicense.LicenseID = _CurrentLicense.LicenseID;
            _DetainLicense.FineFees = Convert.ToSingle(txtFineFees.Text);
            _DetainLicense.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;

            if (_DetainLicense.Save())
            {
                MessageBox.Show("License Detained Successfully with ID =" + _DetainLicense.DetainID.ToString(), "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblDetainID.Text = _DetainLicense.DetainID.ToString();
                DataBack?.Invoke(this, _DetainLicense.DetainID);
                gbFilters.Enabled = false;

                btnDetain.Enabled = false;
                llShowLicenseInfo.Enabled = true;
            }

            else
            {
                MessageBox.Show("Error: License Is not Detained Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlLocalDrivingInfo1_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
