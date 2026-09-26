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

namespace DVLD.Licenses.Interantional_Driving_License
{
    public partial class frmShowInternationalDrivingLicenseInfocs : Form
    {

        private int _InternationalDrivingLicenseID = -1;
        //public clsInternationalDrivingLicense InternationalDrivingLicense;
        public frmShowInternationalDrivingLicenseInfocs()
        {
            InitializeComponent();
        }


        public frmShowInternationalDrivingLicenseInfocs(int InternationalDrivingLicenseID)
        {
            InitializeComponent();
            _InternationalDrivingLicenseID = InternationalDrivingLicenseID;
        }



        private void ctrlInternationalDrivingLicenseCardInfo1_Load(object sender, EventArgs e)
        {
            ctrlInternationalDrivingLicenseCardInfo1.LoadData(_InternationalDrivingLicenseID);

        }
    }
}
