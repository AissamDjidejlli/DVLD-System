using DVLD_DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_Buisness
{
    public class clsApplication

    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }
        public int ApplicationID { set; get; }
        public int PersonID { set; get; }

        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(PersonID).FullName();
            }
        }
        public clsPerson PersonInfo { set; get; }
        public DateTime ApplicationDate { set; get; }

        public int ApplicationTypeID { set; get; }
        public clsApplicationType ApplicationTypeInfo { set; get; }

        public clsApplication.enApplicationStatus ApplicationStatus { set; get; }

        public DateTime LastStatusDate { set; get; }

        public float Fees { set; get; }

        public int UserID { set; get; }
        public clsUser UserInfo { set; get; }

        public clsApplication()
        {
            ApplicationID = -1;
            PersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = clsApplication.enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            Fees = 0;
            UserID = -1;

            Mode = enMode.Add;
        }

        private clsApplication(int ApplicationID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID,
            clsApplication.enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float Fees, int UserID)
        {
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.Fees = Fees;
            this.UserID = UserID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.UserInfo = clsUser.Find(UserID);

            Mode = enMode.Update;
        }


        public static clsApplication FindByID(int ApplicationID)
        {
            int PersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            clsApplication.enApplicationStatus ApplicationStatus = enApplicationStatus.New;
            byte ApplicationStatusID = (byte)ApplicationStatus;
            DateTime LastStatusDate = DateTime.Now;
            float Fees = 0;
            int UserID = -1;
            bool IsFound = clsApplicationData.GetApplicationInfoByID
                                (
                                    ApplicationID, ref PersonID, ref ApplicationDate,
                                    ref ApplicationTypeID, ref ApplicationStatusID, ref LastStatusDate,
                                    ref Fees, ref UserID
                                );
            if (IsFound)
            {
                ApplicationStatus = (clsApplication.enApplicationStatus)ApplicationStatusID;
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                        ApplicationStatus, LastStatusDate, Fees, UserID);
            }
            else
                return null;
        }

        public static clsApplication FindByPersonID(int PersonID)
        {
            int ApplicationID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            clsApplication.enApplicationStatus ApplicationStatus = enApplicationStatus.New;
            int ApplicationStatusID = (int)ApplicationStatus;
            DateTime LastStatusDate = DateTime.Now;
            float Fees = 0;
            int UserID = -1;
            bool IsFound = clsApplicationData.GetApplicationInfoByPersonID
                                (
                                    ref ApplicationID, PersonID, ref ApplicationDate,
                                    ref ApplicationTypeID, ref ApplicationStatusID, ref LastStatusDate,
                                    ref Fees, ref UserID
                                );
            if (IsFound)
            {
                ApplicationStatus = (clsApplication.enApplicationStatus)ApplicationStatusID;
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                        ApplicationStatus, LastStatusDate, Fees, UserID);
            }
            else
                return null;
        }

        public static clsApplication FindByUserID(int UserID)
        {
            int ApplicationID = -1;
            int PersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            clsApplication.enApplicationStatus ApplicationStatus = enApplicationStatus.New;
            int ApplicationStatusID = (int)ApplicationStatus;
            DateTime LastStatusDate = DateTime.Now;
            float Fees = 0;
            bool IsFound = clsApplicationData.GetApplicationInfoByUserID
                                (
                                    ref ApplicationID, ref PersonID, ref ApplicationDate,
                                    ref ApplicationTypeID, ref ApplicationStatusID, ref LastStatusDate,
                                    ref Fees, UserID
                                );
            if (IsFound)
            {
                ApplicationStatus = (clsApplication.enApplicationStatus)ApplicationStatusID;
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                        ApplicationStatus, LastStatusDate, Fees, UserID);
            }

            else
                return null;
        }

        public bool _AddNewApplication()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationData.AddNewApplication(
                this.PersonID, this.ApplicationDate,
                this.ApplicationTypeID, (int)this.ApplicationStatus,
                this.LastStatusDate, this.Fees, this.UserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            //call DataAccess Layer 

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.PersonID, this.ApplicationDate,
                this.ApplicationTypeID, (int)this.ApplicationStatus,
                this.LastStatusDate, this.Fees, this.UserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplication();
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public static bool Cancel(int ApplicationID)
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 1);
        }

        public static bool SetComplete(int ApplicationID)
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);


        }

        public bool SetComplete()
        {
            return SetComplete(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static int GetActiveApplication(int ApplicantPersonID, clsApplication.enApplicationStatus ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplication(ApplicantPersonID, (int)ApplicationTypeID);
        }
        public int GetActiveApplication(clsApplication.enApplicationStatus ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplication(this.PersonID, (int)ApplicationTypeID);
        }


        public static int GetActiveApplicationForLicenseClasses(int ApplicantPersonID, clsApplication.enApplicationStatus ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationForLicenseClasses(ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }



        public static bool DoesPersonHaveActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(ApplicantPersonID, ApplicationTypeID);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(this.PersonID, ApplicationTypeID);
        }

    }
}
