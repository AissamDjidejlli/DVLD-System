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

namespace DVLD.Test.Controls
{
    public partial class ctrlShudledTest : UserControl
    {

        private int _TestAppointmentID;
        private clsTestAppointment _TestAppointment;


        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }
        public clsTestAppointment TestAppointment
        {
            get {  return _TestAppointment; }
        }


      



        public ctrlShudledTest()
        {
            InitializeComponent();
        }

        public void LoadData(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.FindByID(TestAppointmentID);

            lblTitle.Text = _TestAppointment.TestTypeInfo.Title;

            if (_TestAppointment.TestTypeID == 1)
            {
                pbTestTypeImage.Image = Properties.Resources.Vision_512;
            }
            if (_TestAppointment.TestTypeID == 2)
            {
                pbTestTypeImage.Image = Properties.Resources.Written_Test_32;
            }
            if (_TestAppointment.TestTypeID == 3)
            {
                pbTestTypeImage.Image = Properties.Resources.Street_Test_32;
            }

            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString();
            lblDrivingClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.Name;
            lblFullName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicationInfo.ApplicantFullName;
            lblFees.Text = _TestAppointment.TestTypeInfo.Fees.ToString();

            lblTrial.Text = (clsTestAppointment.GetTrialsNumbers(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeID) + 1).ToString();

            if (_TestAppointment.IsLocked == true)
            {
                lblTestID.Text = clsTests.FindByAppointmentID(_TestAppointmentID).TestID.ToString();
            }
            else
            {
                lblTestID.Text = "Not Created Yet";
            }
        }

        private void pbTestTypeImage_Click(object sender, EventArgs e)
        {

        }

        private void ctrlShudledTest_Load(object sender, EventArgs e)
        {

        }
    }
}
