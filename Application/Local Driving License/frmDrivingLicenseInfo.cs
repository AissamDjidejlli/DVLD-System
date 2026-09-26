using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.Local_Driving_License
{
    public partial class frmDrivingLicenseInfo : Form
    {
        public frmDrivingLicenseInfo()
        {
            InitializeComponent();
        }

        public frmDrivingLicenseInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            ctrlLocalDrivingApplicationInfo1.LoadData(LocalDrivingLicenseApplicationID);
        }

        private void ctrlLocalDrivingApplicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmDrivingLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
