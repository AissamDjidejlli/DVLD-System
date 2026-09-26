using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public  class clsInternationalDrivingLicense
    {
        public int InternationalLicenseID { get; set; }


        public int ApplicationID { get; set; }
        public clsApplication Application { get; set; }


        public int DriverID { get; set; }
        public clsDrivers Driver { get; set; }



        public int IssuedUsingLocalLicenseID { get; set; }
        public clsLicenses IssuedUsingLocalLicense { get; set; }



        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }


        public bool IsActive { get; set; }


        public int CreatedByUserID;
        public clsUser CreatedByUser { get; set; }


        public clsInternationalDrivingLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now; 
            this.ExpirationDate = this.IssueDate.AddYears(1);
            this.IsActive = true;
            this.CreatedByUserID = -1;

        }

        private clsInternationalDrivingLicense(int InternationalLicenseID,  int ApplicationID,  int DriverID,  int IssuedUsingLocalLicenseID,
          DateTime IssueDate,  DateTime ExpirationDate,  bool IsActive,  int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;


            this.Application = clsApplication.FindByID(ApplicationID);
            this.IssuedUsingLocalLicense = clsLicenses.FindByID(IssuedUsingLocalLicenseID);
            this.Driver = clsDrivers.Find(DriverID);
            this.CreatedByUser = clsUser.Find(CreatedByUserID);
        }


        public static clsInternationalDrivingLicense FindByID(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;


            bool IsFound = clsInternationalDrivingLicenseData.GetInternationalLicenseInfoByID
                                (
                                  InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive,ref CreatedByUserID
                                );
            if (IsFound)
            {
                return new clsInternationalDrivingLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsInternationalDrivingLicense FindByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            int ApplicationID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;


            bool IsFound = clsInternationalDrivingLicenseData.GetInternationalLicenseInfoByDriverID
                                (
                                  ref InternationalLicenseID, ref ApplicationID,  DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID
                                );
            if (IsFound)
            {
                return new clsInternationalDrivingLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsInternationalDrivingLicense FindByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
             IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;


            bool IsFound = clsInternationalDrivingLicenseData.GetInternationalLicenseInfoByLocalLicenseID
                                (
                                  ref InternationalLicenseID, ref ApplicationID, ref DriverID,  IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID
                                );
            if (IsFound)
            {
                return new clsInternationalDrivingLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        private bool _AddNewInternationalLicense()
        {

            this.InternationalLicenseID = clsInternationalDrivingLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
             this.IssueDate, this.ExpirationDate,this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        public bool Save()
        {
            if (_AddNewInternationalLicense())
                return true;
            else
                return false;
        }

        public static bool HasDriverInternationalLicense(int DrivreID)
        {
            return clsInternationalDrivingLicenseData.HasDriverInternationalLicense(DrivreID);
        }

        public static DataTable GetAllPersonInternationalLicense(int PersonID)
        {
            return clsInternationalDrivingLicenseData.GetAllPersonInternationalLicense(PersonID);
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalDrivingLicenseData.GetAllInternationalLicense();
        }

    }
}
