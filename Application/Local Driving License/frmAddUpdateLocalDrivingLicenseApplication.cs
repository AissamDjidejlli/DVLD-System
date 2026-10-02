using DVLD.GlobalClasses;
using DVLD.People.controles;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public delegate void DataBackEventHandler(object sender, int LocalDrivingLicenseApplicationID);
        public event DataBackEventHandler DataBack;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        
       
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication
        {
            get { return _LocalDrivingLicenseApplication; }
        }

        private int _ApplicationID = -1;
        private clsApplication _ApplicationInfo;
        private clsLicenseClasses _LicenseClassInfo;
        private string UserName;
        private clsUser _User;
        private enum enMode { Add = 0, Edit = 1 };
        enMode Mode;


       
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            Mode = enMode.Edit;
        }


        private void _DefaultValues()
        {

            if (Mode == enMode.Add)
            {
                lblTitle.Text = "Add New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                _ApplicationInfo = new clsApplication();
            }


            lblTitle.Text = "Update Local Driving License Application";
            
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            lblCreatedByUser.Text = clsGlobalSetting.CurrentUser.UserName;
            UserName = lblCreatedByUser.Text;
            _User = clsGlobalSetting.CurrentUser;
        }


        private void _LoadApplicationInfo()
        {
            _LicenseClassInfo = clsLicenseClasses.Find(cbLicenseClass.Text.ToString());


            _ApplicationInfo.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _ApplicationInfo.ApplicationTypeID = 1;
            _ApplicationInfo.LastStatusDate = DateTime.Now;
            _ApplicationInfo.Fees = Convert.ToSingle(lblFees.Text);
            _ApplicationInfo.UserID = _User.UserID;
            _ApplicationInfo.PersonID = ctrlPersonCardWithFilter1.PersonID;

            _LocalDrivingLicenseApplication.ApplicationInfo = _ApplicationInfo;
            
            _LocalDrivingLicenseApplication.LicenseClassInfo = _LicenseClassInfo;
            _LocalDrivingLicenseApplication.LicenseClassID = _LicenseClassInfo.ID;

        }

        private void _LoadUserInfoTo()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            _ApplicationInfo = _LocalDrivingLicenseApplication.ApplicationInfo;

            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationDate.ToString("yyyy-MM-dd hh:mm:ss tt");
            cbLicenseClass.SelectedItem = _LocalDrivingLicenseApplication.LicenseClassInfo.Name;
            lblFees.Text = _LocalDrivingLicenseApplication.ApplicationInfo.Fees.ToString();
            lblCreatedByUser.Text = UserName;
            ctrlPersonCardWithFilter1.LoadData(_LocalDrivingLicenseApplication.ApplicationInfo.PersonID);

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {

            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Mode == enMode.Edit)
            {
                 int ApplicationToEditID = clsApplication.GetActiveApplicationForLicenseClasses(_ApplicationInfo.PersonID,
                  (clsApplication.enApplicationStatus)_ApplicationInfo.ApplicationTypeID, clsLicenseClasses.Find(cbLicenseClass.Text.Trim()).ID);

                if (clsApplication.IsApplicationExist(ApplicationToEditID))
                {
                    MessageBox.Show("Error: Application is already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _LoadApplicationInfo();


            int ApplicationID = clsApplication.GetActiveApplicationForLicenseClasses(
                ctrlPersonCardWithFilter1.PersonID,(clsApplication.enApplicationStatus)_ApplicationInfo.ApplicationTypeID, _LicenseClassInfo.ID);

            if (ApplicationID == -1 && !clsLicenses.HasPersonLicenseFromSameClasse(_ApplicationInfo.PersonID, _LicenseClassInfo.ID))
            {
                if (_ApplicationInfo._AddNewApplication())
                {
                    _ApplicationID = _ApplicationInfo.ApplicationID;
                }
                
                _LocalDrivingLicenseApplication.ApplicationID = _ApplicationInfo.ApplicationID;

                if (_LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this, _LocalDrivingLicenseApplication.ID);
                    lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.ID.ToString();
                }
                else
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: Application is already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

             
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {    
              tcApplicationInfo.SelectedIndex = 1;
        }

        private void lblFees_Click(object sender, EventArgs e)
        {

        }

        private void lblFees_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _DefaultValues();

            if (Mode == enMode.Edit)
            {
                _LoadUserInfoTo();
            }
        }

        private void cbLicenseClass_TextChanged(object sender, EventArgs e)
        {
            string Fees = clsLicenseClasses.Find(cbLicenseClass.Text.ToString()).Fees.ToString();
            lblFees.Text = Fees;
        }

        private void lblCreatedByUser_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}
