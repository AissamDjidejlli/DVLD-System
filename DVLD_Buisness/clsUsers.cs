using DVLD_DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_Buisness
{
    public class clsUser
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;

        public int UserID { set; get; }
        public int PersonID { set; get; }

        public clsPerson PersonInfo ;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool  IsActive { set; get; }
        
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;

            Mode = enMode.Add;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }

        public static clsUser Find(int UserID)
        {

            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = true;

            bool IsFound = clsUsersData.GetUserInfoByID
                                (
                                    UserID, ref PersonID, ref UserName,
                                    ref Password, ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsUser(UserID,PersonID,UserName,Password,IsActive);
            else
                return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {

            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = true;

            bool IsFound = clsUsersData.GetUserInfoByPersonID
                                (
                                    ref UserID,PersonID,ref UserName,
                                    ref Password, ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {

            int UserID = -1;
            int PersonID = -1;
            bool IsActive = true;

            bool IsFound = clsUsersData.GetUserInfoByUserNameAndPassword
                                (
                                    ref UserID,ref PersonID, UserName,
                                     Password, ref IsActive
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }


        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(this.PersonID,this.UserName,this.Password,this.IsActive);

            return (this.UserID > 0);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUsersData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUsersData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistForPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID,string NewPassword)
        {
            return clsUsersData.ChangePassword(UserID,NewPassword);
        }
    }
}