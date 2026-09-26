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

namespace DVLD.Application.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        private clsApplication _Application;
        public clsApplication Application
        {
            get { return _Application; }
        }

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        public ctrlApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        public void LoadData(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            _Application = clsApplication.FindByID(_LocalDrivingLicenseApplication.ApplicationID);

            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.ApplicationStatus.ToString();
            lblFees.Text = _Application.Fees.ToString();
            lblType.Text = _Application.ApplicationTypeInfo.Title;
            lblApplicant.Text = _Application.ApplicantFullName;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedByUser.Text = _Application.UserInfo.UserName;

        }
        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfoCard frm = new frmShowPersonInfoCard(_Application.PersonID);
            frm.ShowDialog();
        }
    }
}
