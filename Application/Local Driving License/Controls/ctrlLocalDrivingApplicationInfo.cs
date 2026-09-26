using DVLD.Licenses.Local_Driving_License;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.Controls
{
    public partial class ctrlLocalDrivingApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication
        {
            get { return _LocalDrivingLicenseApplication; }
        }


        public ctrlLocalDrivingApplicationInfo()
        {
            InitializeComponent();
        }

        public ctrlLocalDrivingApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;


        }

        public void LoadData(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);

            ctrlApplicationInfo1.LoadData(LocalDrivingLicenseApplicationID);

            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.ID.ToString();
            lblAppliedFor.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.Name;

            lblPassedTests.Text = clsTests.GetTestsPassed(_LocalDrivingLicenseApplicationID).ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationInfo1_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlLocalDrivingApplicationInfo_Load(object sender, EventArgs e)
        {

           
        }

        private void ctrlApplicationInfo2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowDriverCardInfo frm = new frmShowDriverCardInfo(ctrlApplicationInfo1.Application.PersonInfo.NationalNo);
            frm.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
