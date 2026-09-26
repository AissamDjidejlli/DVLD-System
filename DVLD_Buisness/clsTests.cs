using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsTestAppointment;

namespace DVLD_Buisness
{
    public class clsTests
    {
        public int TestID;

        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointment { get; set; }

        public int TestResult { get; set; }
        public string Notes { get; set; }

        public int CreatedByUserID;


        public clsTests()
        {
            this.TestID = 1;
            this.TestAppointmentID = -1;
            this.TestResult = -1;
            this.Notes = "";

        }

        private clsTests(int TestID, int TestAppointmentID, int TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            this.TestAppointment = clsTestAppointment.FindByID(TestAppointmentID);
        }


        public static clsTests FindByID(int TestID)
        {
            int TestAppointmentID = -1;
            int TestResult = -1;
            string Notes = "";
            int CreatedByUserID = -1;

            bool IsFound = clsTestsData.GetTestInfoByID
                                (
                                    TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID
                                );
            if (IsFound)
            {
                return new clsTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsTests FindByAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            int TestResult = -1;
            string Notes = "";
            int CreatedByUserID = -1;

            bool IsFound = clsTestsData.GetTestInfoByAppointmentID
                                (
                                    ref TestID, TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID
                                );
            if (IsFound)
            {
                return new clsTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;
        }

        private bool _AddNewTest()
        {

            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }

        public DataTable GetAllTest()
        {
            return clsTestsData.GetAllTest();
        }

        public bool Save()
        {
            if (_AddNewTest())
                return true;
            else
                return false;
        }

        public static bool HasFailedTest(int LocalDrivingLicenseAppID)
        {
            return clsTestsData.HasFailedTest(LocalDrivingLicenseAppID);
        }

        public static bool IsTestPassed(int LocalDrivingLicenseAppID,int TestPassed)
        {
            return clsTestsData.IsTestPassed(LocalDrivingLicenseAppID, TestPassed);
        }

        public static int GetTestsPassed(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsData.GetTestsPassed(LocalDrivingLicenseApplicationID);
        }


    }
}
