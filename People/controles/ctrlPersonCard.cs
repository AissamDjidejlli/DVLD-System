using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.controles
{
    public partial class ctrlPersonCard : UserControl
    {
        
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private int _PersonID;
        private clsPerson Person;

        public ctrlPersonCard(int PersonID)
        {
            InitializeComponent();
            LoadPersonInfo(PersonID);

        }

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return Person; }
        }

        
        public void LoadPersonInfo(int PersonID)
        {

            _PersonID = PersonID;
            Person = clsPerson.Find(PersonID);

            if (Person == null)
            {
                MessageBox.Show("Person not Found");
                return;
            }

            else
            {
                _FillPersonInfo();
            }

        }
        public void LoadPersonInfo(string NationalNo)
        {

           
            Person = clsPerson.Find(NationalNo);


            if (Person == null)
            {
                MessageBox.Show("Person not Found");
                return;
            }

            else
            {
                _FillPersonInfo();
                _PersonID = Person.PersonID;
            }

        }

        private void _LoadImage()
        {
            if (Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Image.FromFile("C:\\Users\\HP 840 G6\\Desktop\\Road Map\\Course 19\\Doctor Solution\\Icons\\Female 512.png"); ;

            string ImagePath = Person.ImagPath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _FillPersonInfo()
        {
            lblPersonID.Text = _PersonID.ToString();
            lblFullName.Text = Person.FullName();
            lblDateOfBirth.Text = Person.DateOfBirth.ToString();
            lblPhone.Text = Person.Phone;
            lblEmail.Text = Person.Email;
            lblNationalNo.Text = Person.NationalNo;
            lblGendor.Text = Person.Gendor == 0 ? "Male" : "Female";
            lblCountry.Text = clsCountry.Find(Person.NationalityCountryID).CountryName;
            lblAddress.Text = Person.Address;
            _LoadImage();

        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePeople frm = new frmAddUpdatePeople(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }
    }
}
