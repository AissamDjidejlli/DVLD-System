using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Driving_License
{
    public partial class frmShowDriverCardInfo : Form
    {
        private string _NationalNo = string.Empty;

        private int _LicenseID = -1;

        public frmShowDriverCardInfo()
        {
            InitializeComponent();
        }

        public frmShowDriverCardInfo(string NationalNo)
        {
            InitializeComponent();
            _NationalNo = NationalNo;
        }

        public frmShowDriverCardInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void ctrlLocalDrivingInfo1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlLocalDrivingInfo1_Load_1(object sender, EventArgs e)
        {
            if (_LicenseID != -1)
                    ctrlLocalDrivingInfo1.LoadData(_LicenseID);

            else
                ctrlLocalDrivingInfo1.LoadData(_NationalNo);


        }
    }
}
