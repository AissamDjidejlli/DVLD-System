using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTestTypes
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;

        public enum enTestType { VisionTest = 1, WrittenTest = 2,StreetTest = 3 }

        public clsTestTypes.enTestType ID { set; get; }
        public string Title { set; get; }

        public string Discription { set; get; }
        public float Fees { set; get; }
        public clsTestTypes()
        {
            this.ID = clsTestTypes.enTestType.VisionTest;
            this.Title = "";
            this.Discription = "";
            this.Fees = 0;
            Mode = enMode.Add;
        }
        private clsTestTypes(clsTestTypes.enTestType ID,string Discription, string Title, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Discription = Discription;
            this.Fees = Fees;
            Mode = enMode.Update;
        }

        public static clsTestTypes Find(clsTestTypes.enTestType ID)
        {
            string Title = ""; string Discription = ""; float Fees = 0;
            if (clsTestTypesData.GetTestTypeByID((int)ID, ref Title, ref Discription , ref Fees))
                return new clsTestTypes(ID, Discription, Title, Fees);
            else
                return null;
        }

        private bool _AddNewTestType()
        {
             this.ID = (clsTestTypes.enTestType) clsTestTypesData.AddNewTestType(this.Title, this.Discription, this.Fees);
            return (this.ID > 0);
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int) this.ID, this.Title, this.Discription, this.Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
