using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestAppointment
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;

       
        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }
        public clsTestTypes TestTypeInfo { get; set; }


        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }

        public DateTime AppointmentDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }

        public bool IsLocked { get; set; }

        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }




        public clsTestAppointment()
        {
           this.TestAppointmentID = -1;
           this.TestTypeID = -1;
           this.LocalDrivingLicenseApplicationID = -1;
           this.AppointmentDate = DateTime.Now;
           this.PaidFees = 0;
           this.CreatedByUserID = -1;
           this.IsLocked = false;
           this.RetakeTestApplicationID = -1;
            

            Mode = enMode.Add;

        }

        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,float PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            this.TestTypeInfo = clsTestTypes.Find((clsTestTypes.enTestType)TestTypeID);
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);
            this.UserInfo = clsUser.Find(CreatedByUserID);
            this.RetakeTestApplicationInfo = clsApplication.FindByID(RetakeTestApplicationID);

            Mode = enMode.Update;
        }


        public static clsTestAppointment FindByID(int TestAppointmentID)
        {
            int TestTypeID = -1,LocalDrivingLicenseApplicationID = -1 , CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            bool IsLocked = false;

            bool IsFound = clsTestAppointmentData.GetTestAppointmentInfoByID
                                (
                                    TestAppointmentID,ref TestTypeID,ref LocalDrivingLicenseApplicationID,
                                    ref AppointmentDate,ref PaidFees,ref CreatedByUserID,ref IsLocked,ref RetakeTestApplicationID
                                );
            if (IsFound)
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                                     AppointmentDate, PaidFees, CreatedByUserID, IsLocked,RetakeTestApplicationID);
            }
            else
                return null;
        }

        

        public bool _AddNewTestAppointment()
        {
            //call DataAccess Layer 

            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(
                this.TestTypeID,this.LocalDrivingLicenseApplicationID,this.AppointmentDate,
                this.PaidFees,this.CreatedByUserID,this.IsLocked,this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID,this.AppointmentDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

        public static DataTable GetAllTestAppointmentForLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentForLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, TestTypeID);
        }


        public static bool LockAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.LockAppointment(TestAppointmentID);
        }
        public bool LockAppointment()
        {
            return LockAppointment(this.TestAppointmentID);
        }


        public static bool HasActiveAppointment(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.HasActiveAppointment(LocalDrivingLicenseApplicationID);
        }


       public static int GetTrialsNumbers(int LocalDrivingLicenseApplication,int TestTypeID)
       {
            return clsTestAppointmentData.GetTrialsNumbers(LocalDrivingLicenseApplication, TestTypeID);
       }

    }
}
