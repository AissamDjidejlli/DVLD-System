using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Test.Controls
{
    public partial class ctrlShudleTest : UserControl
    {

        private enum enMode { Add = 0, Update = 1 }
        private enMode Mode;

        private enum enAppointmentMode { New = 0, Retake = 1 }
        private enAppointmentMode AppointmentMode = enAppointmentMode.New;


        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;



        private int _TestTypeID;
        private clsTestTypes _TestType;


        private int _TestAppointmentID;
        private clsTestAppointment _TestAppointment;



        private clsApplication _RetakeTestApplication;

        public ctrlShudleTest()
        {
            InitializeComponent();
            
        }

        public void LoadDataFromLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            Mode = enMode.Add;


            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);

            _TestTypeID = TestTypeID;  
            _TestType = clsTestTypes.Find((clsTestTypes.enTestType)_TestTypeID);



            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.ID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.Name;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicationInfo.ApplicantFullName;
            lblTrial.Text = (clsTestAppointment.GetTrialsNumbers(_LocalDrivingLicenseApplicationID,_TestTypeID) + 1).ToString();
            lblFees.Text = _TestType.Fees.ToString();



            _TestAppointment = new clsTestAppointment();




            lblTitle.Text = _TestType.Title;

            if (_TestType.ID == (clsTestTypes.enTestType)1)
            {
                pbTestTypeImage.Image = Properties.Resources.Vision_512;
            }

            if (_TestType.ID == (clsTestTypes.enTestType)2)
            {
                pbTestTypeImage.Image = Properties.Resources.Written_Test_32;
            }

            if (_TestType.ID == (clsTestTypes.enTestType)3)
            {
                pbTestTypeImage.Image = Properties.Resources.Street_Test_32;
            }


          

            if (clsTests.HasFailedTest(_LocalDrivingLicenseApplicationID))
            {
                AppointmentMode = enAppointmentMode.Retake;

                lblTitle.Text = $"{_TestType.Title} - Retake";


                gbRetakeTestInfo.Enabled = true;


                _RetakeTestApplication = new clsApplication();


                float Fees = clsApplicationType.Find(7).Fees;
                float TotalFees = Fees + _TestType.Fees;

                _RetakeTestApplication.Fees = TotalFees;

                lblRetakeAppFees.Text = Fees.ToString();
                lblTotalFees.Text = TotalFees.ToString();

                
            }
        }

        public void LoadDataFromAppointment(int TestAppointmentID)
        {
            Mode = enMode.Update;


            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.FindByID(TestAppointmentID); 


            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            dtpTestDate.Value = _TestAppointment.AppointmentDate;
            lblDrivingClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.Name;
            lblFullName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicationInfo.ApplicantFullName;
            lblTrial.Text = (clsTestAppointment.GetTrialsNumbers(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeID)+1).ToString();
            lblFees.Text = _TestAppointment.TestTypeInfo.Fees.ToString();


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


            if (_TestAppointment.IsLocked)
            {
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
            }




            if (_TestAppointment.RetakeTestApplicationID != -1)
            {
                AppointmentMode = enAppointmentMode.Retake;

                lblTitle.Text = $"{_TestAppointment.TestTypeInfo.Title} - Retake";

                gbRetakeTestInfo.Enabled = true;


                float Fees = _TestAppointment.RetakeTestApplicationInfo.ApplicationTypeInfo.Fees;
                float TotalFees = Fees + _TestAppointment.TestTypeInfo.Fees;


                lblRetakeAppFees.Text = Fees.ToString();
                lblTotalFees.Text = TotalFees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
        }




        private void lblRetakeAppFees_Click(object sender, EventArgs e)
        {

        }

        private void gbRetakeTestInfo_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlShudleTest_Load(object sender, EventArgs e)
        {
            dtpTestDate.Value = DateTime.Now;
            dtpTestDate.MinDate = dtpTestDate.Value;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {        

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = _TestType.Fees;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            if (AppointmentMode == enAppointmentMode.Retake)
            {

                _RetakeTestApplication.PersonID = _LocalDrivingLicenseApplication.ApplicationInfo.PersonID;
                _RetakeTestApplication.ApplicationTypeID = 7;
                _RetakeTestApplication.ApplicationStatus = (clsApplication.enApplicationStatus)1;
                _RetakeTestApplication.UserID = clsGlobalSetting.CurrentUser.UserID;

                _RetakeTestApplication.Save();

                lblRetakeTestAppID.Text = _RetakeTestApplication.ApplicationID.ToString();

                _TestAppointment.RetakeTestApplicationID = _RetakeTestApplication.ApplicationID;
            }

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblUserMessage_Click(object sender, EventArgs e)
        {

        }

        private void dtpTestDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
