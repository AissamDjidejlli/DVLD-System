using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsLicenses;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Buisness
{
    public class clsDetainReleaseLicense
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;

        public int DetainID { get; set; }


        public int LicenseID { get; set; }
        public clsLicenses Licenses { get; set; }



        public DateTime DetainDate { get; set; }

        public DateTime? ReleaseDate { get; set; }


        public float FineFees { get; set; }

        public bool IsReleased { get; set; }



        public int CreatedByUserID;
        public clsUser CreatedByUser { get; set; }


        public int ReleasedByUserID;
        public clsUser ReleasedByUser { get; set; }



        public int ReleaseApplicationID;
        public clsApplication ReleaseApplication { get; set; }


        public clsDetainReleaseLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.IsReleased = false;
            this.CreatedByUserID = -1;

            this.ReleaseDate = null;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;

            Mode = enMode.Add;
        }

        private clsDetainReleaseLicense(int DetainID,int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;


          
            this.Licenses = clsLicenses.FindByID(LicenseID);
            this.CreatedByUser = clsUser.Find(CreatedByUserID);

            if (ReleasedByUserID == -1)
                this.ReleasedByUser = null;
            else
                this.ReleasedByUser = clsUser.Find(ReleasedByUserID);

            if (ReleasedByUserID == -1)
                this.ReleasedByUser = null;
            else
                this.ReleaseApplication = clsApplication.FindByID(ReleaseApplicationID);

            Mode = enMode.Update;

        }


        public static clsDetainReleaseLicense FindByID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            DateTime? ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            int CreatedByUserID = -1;


            bool IsFound = clsDetainReleaseLicenseData.GetDetainLicenseInfoByID
                                (
                                     DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                                      ref IsReleased,ref  ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID

                                );
            if (IsFound)
            {
                return new clsDetainReleaseLicense(DetainID,  LicenseID,  DetainDate,  FineFees,  CreatedByUserID,
                                       IsReleased,  ReleaseDate,  ReleasedByUserID,  ReleaseApplicationID);
            }
            else
                return null;
        }

        public static clsDetainReleaseLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            DateTime? ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            int CreatedByUserID = -1;
   

            bool IsFound = clsDetainReleaseLicenseData.GetDetainLicenseInfoByLicenseID
                                (
                                     ref DetainID,  LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                                      ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID

                                );
            if (IsFound)
            {
                return new clsDetainReleaseLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                                       IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        private bool _AddNewDetainLicense()
        {

            this.DetainID = clsDetainReleaseLicenseData.AddNewDetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID,
                                       this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainReleaseLicenseData.UpdateDetainedLicense(this.DetainID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_AddNewDetainLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }

            return false;
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainReleaseLicenseData.IsLicenseDetained(LicenseID);
        }
        public  bool IsLicenseDetained()
        {
            return clsDetainReleaseLicense.IsLicenseDetained(this.LicenseID);
        }



        public static bool ReleaseLicense(int LicenseID)
        {
            return clsDetainReleaseLicenseData.ReleaseLicense(LicenseID);
        }

        public bool ReleaseLicense()
        {
            return clsDetainReleaseLicense.ReleaseLicense(this.LicenseID);
        }





        public static DataTable GetAllDetainLicenses()
        {
            return clsDetainReleaseLicenseData.GetAllDetainLicenses();
        }


    }
}
