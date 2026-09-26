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

namespace DVLD.Test
{
    public partial class frmSchudleTest : Form
    {


        private int _TestAppointmentID;

        private int _LocalDrivingLicenseApplicationID;
        private int _TestTypeID;

        private enum enMode { Add = 0, Update = 1 }
        private enMode Mode;


       
        public frmSchudleTest(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            Mode = enMode.Add;
        }

        public frmSchudleTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            Mode = enMode.Update;
        }


        private void ctrlShudleTest1_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Add)
            {
                ctrlShudleTest1.LoadDataFromLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID, _TestTypeID);
                return;
            }

            if(Mode == enMode.Update)
            {
                ctrlShudleTest1.LoadDataFromAppointment(_TestAppointmentID);
                return;
            }

        }
    }
}
