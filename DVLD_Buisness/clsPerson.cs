using DVLD_DataAccess;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode;
        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName()
        {
            return this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
        }
        public DateTime DateOfBirth { set; get; }
        public short Gendor { set; get; }
        public string Address { set; get; }
        public string NationalNo { set; get; }
        public int NationalityCountryID { set; get; }

        public clsCountry CountryInfo { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        private string _ImagePath;

        public string ImagPath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }


        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.NationalNo = "";
            this.NationalityCountryID = -1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.ImagPath = "";

            Mode = enMode.Add;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor, string Address,
            string Phone, string Email, int NationalityCountryID, string Imag)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.NationalNo = NationalNo;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagPath = Imag;

            Mode = enMode.Update;
        }

        public static clsPerson Find(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "",NationalNo="", Email = "", Phone = "", Address = "", ImagePathPath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID 
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePathPath
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, FirstName,SecondName ,ThirdName, LastName,
                          NationalNo, DateOfBirth,Gendor, Address, Phone, Email,NationalityCountryID, ImagePathPath);
            else
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePathPath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo
                                (
                                    NationalNo,ref PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePathPath
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePathPath);
            else
                return null;
        }
        private  bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName
            , this.NationalNo, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
            this.Email, this.NationalityCountryID, this.ImagPath);

            return (this.PersonID > 0);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID,this.FirstName, this.SecondName, this.ThirdName, this.LastName
            , this.NationalNo, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
            this.Email, this.NationalityCountryID, this.ImagPath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    
                        if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
             return clsPersonData.DeletePerson(PersonID);
        }
        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
    }
}
