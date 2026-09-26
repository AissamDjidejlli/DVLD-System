using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDrivers
    {
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;

        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUser { set; get; }
        
        public DateTime CreatedDate { set; get; }

        public clsDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;


            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUser = clsUser.Find(CreatedByUserID);
            
        }  


        public static clsDrivers Find(int DriverID)
        {

            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now ;

            bool IsFound = clsDriversData.GetDriverInfoByID
                                (
                                    DriverID, ref PersonID, ref CreatedByUserID,ref CreatedDate
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsDrivers(DriverID,PersonID,CreatedByUserID,CreatedDate);
            else
                return null;
        }

        public static clsDrivers FindByPersonID(int PersonID)
        {

            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = clsDriversData.GetDriverInfoByPersonID
                                (
                                    ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        private bool _AddNewDriving()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID > 0);
        }

        public bool Save()
        {
            if (_AddNewDriving())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }

        public static bool IsDriverExist(int DriverID)
        {
            return clsDriversData.IsDriverExist(DriverID);
        }
        public static bool IsDriverExistForPersonID(int PersonID)
        {
            return clsDriversData.IsDriverExistForPersonID(PersonID);
        }

    }
}
