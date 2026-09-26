using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Licenses.Local_Driving_License
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        private clsLicenses _Licenses;

        private clsDrivers _Driver;



    
        public frmIssueDriverLicenseFirstTime()
        {
            InitializeComponent();
        }

        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void ctrlLocalDrivingApplicationInfo1_Load(object sender, EventArgs e)
        {
            

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            _Driver.PersonID = _LocalDrivingLicenseApplication.ApplicationInfo.PersonID;
            _Driver.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            if (!clsDrivers.IsDriverExistForPersonID(_Driver.PersonID))
            {
                if (_Driver.Save())
                {
                    _Licenses.DriverID = _Driver.DriverID;
                }
            }
            else
            {
                _Licenses.DriverID = clsDrivers.FindByPersonID(_Driver.PersonID).DriverID;
            }

            _Licenses.ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;   
            _Licenses.LicenseClassID = _LocalDrivingLicenseApplication.LicenseClassID;
            _Licenses.Notes = txtNotes.Text;
            _Licenses.PaidFees = _LocalDrivingLicenseApplication.ApplicationInfo.Fees;
            _Licenses.IssueReason = clsLicenses.enIssueReason.FirstTime;
            _Licenses.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;


            if (_Licenses.Save())
            {
                MessageBox.Show("License Successfully with ID =" + _Licenses.LicenseID.ToString(), "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();

                if (_LocalDrivingLicenseApplication.ApplicationInfo.SetComplete())
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void ctrlLocalDrivingApplicationInfo1_Load_1(object sender, EventArgs e)
        {
            ctrlLocalDrivingApplicationInfo1.LoadData(_LocalDrivingLicenseApplicationID);

            _LocalDrivingLicenseApplication = ctrlLocalDrivingApplicationInfo1.LocalDrivingLicenseApplication;
            _Driver = new clsDrivers();
            _Licenses = new clsLicenses();
        }
    }
}
