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

namespace DVLD.Application.Detain_and_Release_Licenses
{
    public partial class frmReleaseLicense : Form
    {
        public delegate void DataBackEventHandler(object sender, int DetainID);
        public event DataBackEventHandler DataBack;

        private int _CurrentLicenseID = -1;

        private clsLicenses _CurrentLicense;

        private clsDetainReleaseLicense _DetainedLicense;


        private clsApplication _ReleaseApplication;
        public frmReleaseLicense()
        {
            InitializeComponent();
        }

        public frmReleaseLicense(int CurrentLicenseID)
        {
            InitializeComponent();
            _CurrentLicenseID = CurrentLicenseID;
        }

        private void gpDetain_Enter(object sender, EventArgs e)
        {

        }

        private void _LoadDetainInfo()
        {

            _CurrentLicense = ctrlLocalDrivingInfo1.License;

            if (_CurrentLicense == null)
            {
                return;
            }


            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            _DetainedLicense = clsDetainReleaseLicense.FindByLicenseID(_CurrentLicense.LicenseID);

            if(_DetainedLicense == null)
            {
                return;
            }

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString();
            lblLicenseID.Text = _CurrentLicense.LicenseID.ToString();

            lblApplicationFees.Text = clsLicenseClasses.Find(_CurrentLicense.LicenseClassID).Fees.ToString();
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();


             if (_DetainedLicense.IsReleased)
             {
                 MessageBox.Show("Error:License Is already released.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                 btnRelease.Enabled = false;

             }
            else
            {
                btnRelease.Enabled = true;
            }
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            if(_CurrentLicenseID != -1)
            {
                txtLicenseID.Text = _CurrentLicenseID.ToString();
                ctrlLocalDrivingInfo1.LoadData(Convert.ToInt16(txtLicenseID.Text));
                gbFilters.Enabled = false;
                _LoadDetainInfo();
            }


            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
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
            

            _LoadDetainInfo();


        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _ReleaseApplication = new clsApplication();

       

            _ReleaseApplication.PersonID = _CurrentLicense.Application.PersonID;
            _ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            _ReleaseApplication.Fees = clsApplicationType.Find(_ReleaseApplication.ApplicationTypeID).Fees;
            _ReleaseApplication.UserID = clsGlobalSetting.CurrentUser.UserID;


            _DetainedLicense.ReleaseDate = DateTime.Now;
            _DetainedLicense.IsReleased = true;
            _DetainedLicense.ReleasedByUserID = clsGlobalSetting.CurrentUser.UserID;

            if (_ReleaseApplication.Save())
            {
                _DetainedLicense.ReleaseApplicationID = _ReleaseApplication.ApplicationID;

                if (_DetainedLicense.Save())
                {
                    MessageBox.Show("License Released Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this, _DetainedLicense.DetainID);

                    lblApplicationID.Text = _ReleaseApplication.ApplicationID.ToString();

                    gbFilters.Enabled = false;
                    btnRelease.Enabled = false;

                }
            }

            else
            {
                MessageBox.Show("Error:License  Is not Released Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
