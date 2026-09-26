
using DVLD_DataAccess;
using System.Data;



namespace DVLD_Buisness
{
    public class clsCountry
    {

        public int CountryID { set; get; }
        public string CountryName { set; get; }


        public clsCountry()

        {
            this.CountryID = -1;
            this.CountryName = "";

        }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
           
        }


        public static clsCountry Find(int CountryID)
        {

            string CountryName = "";

            if (clsCountryData.GetCountryByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {

            int CountryID = -1;

            if (clsCountryData.GetCountryByName(CountryName, ref CountryID))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
