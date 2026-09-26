using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private string _NationalNo = string.Empty;

        private clsPerson _Person;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(string NationalNo)
        {
            InitializeComponent();
            _NationalNo = NationalNo;
        }



        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {
           

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            _Person = clsPerson.Find(_NationalNo);
            ctrlPersonCardWithFilter1.LoadData(_Person.PersonID);

            ctrlDriverLicenses1.LoadDataForLocal(_Person.PersonID);

            if (!clsInternationalDrivingLicense.HasDriverInternationalLicense(clsDrivers.FindByPersonID(_Person.PersonID).DriverID))
            {
                return;
            }

            ctrlDriverLicenses1.LoadDataForInternational(_Person.PersonID);
        }



        private void ctrlDriverLicenses1_Load(object sender, EventArgs e)
        {
          
        }
    }
}
