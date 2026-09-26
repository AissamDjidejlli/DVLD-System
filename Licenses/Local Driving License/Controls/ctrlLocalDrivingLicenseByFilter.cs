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

namespace DVLD.Licenses.Local_Driving_License.Controls
{
    public partial class ctrlLocalDrivingLicenseByFilter : UserControl
    {
        public clsLicenses License;
        public ctrlLocalDrivingLicenseByFilter()
        {
            InitializeComponent();
        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }


        private void ctrlLocalDrivingInfo1_Load(object sender, EventArgs e)
        {
           
        }


        
        private void ctrlLocalDrivingLicenseByFilter_Load(object sender, EventArgs e)
        {


        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            ctrlLocalDrivingInfo1.LoadData(Convert.ToInt16(txtLicenseID.Text));
            License = ctrlLocalDrivingInfo1.License;
        }
    }
}
