using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Buisness
{
    public class clsLicenseClasses

    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Discription { set; get; }

        public byte MinimumAllowedAge { set; get; }

        public byte DefaultValidityLenght { set; get; }

        public float Fees { set; get; }

       

        private clsLicenseClasses(int ID, string Name, string Discription, byte MinimumAllowedAge,
            byte DefaultValidityLenght, float Fees)
        {
            this.ID = ID;
            this.Name = Name;
            this.Discription = Discription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLenght = DefaultValidityLenght;
            this.Fees = Fees;
        }

        public static  clsLicenseClasses Find(int ID)
        {
            string Name = "", Discription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLenght = 0;
            float Fees = 0;
            bool IsFound = clsLicenseClassesData.GetLicenseClasseInfoByID
                                (
                                    ID, ref Name, ref Discription,ref MinimumAllowedAge,
                                    ref DefaultValidityLenght,ref Fees
                                );
            if (IsFound)
            {
               
                return new clsLicenseClasses(ID,Name, Discription,MinimumAllowedAge,
                                    DefaultValidityLenght,Fees);
            }
            else
                return null;
        }

        public static clsLicenseClasses Find(string Name)
        {
            int ID = -1;
            string Discription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLenght = 0;
            float Fees = 0;
            bool IsFound = clsLicenseClassesData.GetLicenseClasseInfoByClassName
                                (
                                    ref ID,  Name, ref Discription, ref MinimumAllowedAge,
                                    ref DefaultValidityLenght, ref Fees
                                );
            if (IsFound)
            {

                return new clsLicenseClasses(ID, Name, Discription, MinimumAllowedAge,
                                    DefaultValidityLenght, Fees);
            }
            else
                return null;
        }

        public static DataTable GetAllLisencesClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

    }
}
