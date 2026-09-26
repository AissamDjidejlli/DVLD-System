using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode { Add = 0, Update = 1 }
        enMode Mode;


        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsApplication ApplicationInfo;
        public clsLicenseClasses LicenseClassInfo;


        public clsLocalDrivingLicenseApplication()
        {
            ID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;

            Mode = enMode.Add;
        }

        private clsLocalDrivingLicenseApplication(int ID, int ApplicationID, int LicenseClassID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationInfo = clsApplication.FindByID(ApplicationID);
            this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);

            Mode = enMode.Update;
        }


        public static clsLocalDrivingLicenseApplication FindByID(int ID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                                (
                                    ID, ref ApplicationID, ref LicenseClassID
                                );
            if (IsFound)
                return new clsLocalDrivingLicenseApplication(ID, ApplicationID, LicenseClassID);
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int ID = -1, LicenseClassID = -1;
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                                (
                                    ref ID, ApplicationID, ref LicenseClassID
                                );
            if (IsFound)
                return new clsLocalDrivingLicenseApplication(ID, ApplicationID, LicenseClassID);
            else
                return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.ID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.ApplicationID > 0);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.ID, this.LicenseClassID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }


        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static bool DeleteLocalDrivingLicenseApplications(int ID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(ID);
        }

       
    }
}
