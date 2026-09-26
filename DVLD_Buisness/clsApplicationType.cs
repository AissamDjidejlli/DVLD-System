using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;
        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }
      
        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;

            Mode = enMode.Add;
        }

        private clsApplicationType(int ID, string Title, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;

            Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ID)
        {

            string Title = ""; float Fees = 0;

            if (clsApplicationTypeData.GetAppilicationTypeByID(ID, ref Title, ref Fees))

                return new clsApplicationType(ID, Title, Fees);
            else
                return null;
        }

        private bool _AddNewApplicationType()
        {
            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);

            return (this.ID > 0);
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplication(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
    }
}
