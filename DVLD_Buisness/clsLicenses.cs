using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicenses

    {
        public enum enIssueReason { FirstTime = 1,Renew = 2, Lost  = 3, Damage = 4}
        public int LicenseID { get; set; }

        public int ApplicationID { get; set; }
        public clsApplication Application{ get; set; }

        public int DriverID { get; set; }
        public clsDrivers Driver { get; set; }


        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClass { get; set; }



        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }



        public string Notes { get; set; }


        public float PaidFees { get; set; }

        public bool IsActive { get; set; }


        public clsLicenses.enIssueReason IssueReason { get; set; }



        public int CreatedByUserID;
        public clsUser CreatedByUser { get; set; }


        public clsLicenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = this.IssueDate.Add(TimeSpan.FromDays(3560));
            this.Notes = "";
            this.IsActive = true ;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

        }

        private clsLicenses(int LicenseID,  int ApplicationID,  int DriverID,  int LicenseCLassID,
             DateTime IssueDate,  DateTime ExpirationDate,  string Notes,  float PaidFees, bool IsActive, byte IssueReason,  int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseCLassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = (clsLicenses.enIssueReason)IssueReason;
            this.CreatedByUserID = CreatedByUserID;


            this.Application = clsApplication.FindByID(ApplicationID);
            this.LicenseClass = clsLicenseClasses.Find(LicenseCLassID);
            this.Driver = clsDrivers.Find(DriverID);
            this.CreatedByUser = clsUser.Find(CreatedByUserID);
        }


        public static clsLicenses FindByID(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = IssueDate.Add(TimeSpan.FromDays(3560));
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true;
            byte IssueReason = 1;
            int CreatedByUserID = -1;


            bool IsFound = clsLicensesData.GetLicensesInfoByID
                                (
                                  LicenseID,ref ApplicationID,ref DriverID,ref LicenseClassID,ref IssueDate,ref  ExpirationDate,
                                  ref Notes, ref PaidFees, ref IsActive,ref IssueReason,ref CreatedByUserID

                                );
            if (IsFound)
            {
                return new clsLicenses(LicenseID,  ApplicationID,  DriverID,  LicenseClassID,  IssueDate,  ExpirationDate,
                                            Notes, PaidFees,  IsActive,  IssueReason, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsLicenses FindByDriverID(int DriverID)
        {
            int LicenseID = -1;
            int ApplicationID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = IssueDate.Add(TimeSpan.FromDays(3560));
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true ;
            byte IssueReason = 1;
            int CreatedByUserID = -1;


            bool IsFound = clsLicensesData.GetLicensesInfoByDriverID
                                (
                                  ref LicenseID, ref ApplicationID,  DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate,
                                  ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID

                                );
            if (IsFound)
            {
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                                            Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
                return null;
        }

        private bool _AddNewLicense()
        {

            this.LicenseID = clsLicensesData.AddNewLicenses(this.ApplicationID, this.DriverID,this.LicenseClassID,
             this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        public bool Save()
        {
            if (_AddNewLicense())
                return true;
            else
                return false;
        }

        public static bool HasPersonLicenseFromSameClasse(int PersonID, int LicenseClass)
        {
            return clsLicensesData.HasPersonLicenseFromSameClasse(PersonID, LicenseClass);
        }


        public static bool DisactivateLicense(int LicenseID)
        {
            return clsLicensesData.DisactivateLicense(LicenseID);
        }

        public  bool DisactivateLicense()
        {
            return clsLicensesData.DisactivateLicense(this.LicenseID);
        }



        public static DataTable GetAllLocalPeopleLicenses(int PersonID)
        {
            return clsLicensesData.GetAllPersonLocalLicense(PersonID);
        }




    }
}
