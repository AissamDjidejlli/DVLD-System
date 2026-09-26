using DVLD.Application.Local_Driving_License;
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
using static DVLD_Buisness.clsTestTypes;

namespace DVLD.Test
{
    public partial class frmListTestAppointment : Form
    {
        public delegate void DataBackEventHandler(object sender, int LocalDrivingLicenseApplicationID);
        public event DataBackEventHandler DataBack;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;


        private int _TestTypeID = -1;
        private clsTestTypes _TestType;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication
        {
            get { return _LocalDrivingLicenseApplication; }
        }


        private static DataTable _dtAllListTestAppointments;
        private void _RefreshListTestAppointments()
        {

            ctrlLocalDrivingApplicationInfo1.LoadData(_LocalDrivingLicenseApplicationID);
            _LocalDrivingLicenseApplication = ctrlLocalDrivingApplicationInfo1.LocalDrivingLicenseApplication;

            _dtAllListTestAppointments = clsTestAppointment.GetAllTestAppointmentForLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID, _TestTypeID);
            dgvLicenseTestAppointments.DataSource = _dtAllListTestAppointments;
            lblRecordsCount.Text = _dtAllListTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.RowCount > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 140;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 220;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 180;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 110;
            }
            
            

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();
        }

        public frmListTestAppointment()
        {
            InitializeComponent();

        }

        public frmListTestAppointment(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.HasActiveAppointment(ctrlLocalDrivingApplicationInfo1.LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("Error: Appointment is already Exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmSchudleTest frm = new frmSchudleTest(ctrlLocalDrivingApplicationInfo1.LocalDrivingLicenseApplicationID, _TestTypeID);
           frm.ShowDialog();
           _RefreshListTestAppointments();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if (dgvLicenseTestAppointments.CurrentRow != null && dgvLicenseTestAppointments.CurrentRow.Index >= 0)
            {
                int TestAppointmentID = Convert.ToInt32(dgvLicenseTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value);

                

                frmSchudleTest frm = new frmSchudleTest(TestAppointmentID);
                frm.ShowDialog();
                _RefreshListTestAppointments();
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLicenseTestAppointments.CurrentRow != null && dgvLicenseTestAppointments.CurrentRow.Index >= 0)
            {
                int TestAppointmentID = Convert.ToInt32(dgvLicenseTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value);

                frmTakeTest frm = new frmTakeTest(TestAppointmentID);
                frm.ShowDialog();
                _RefreshListTestAppointments();
            }

            if (clsTests.IsTestPassed(LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                btnAddNewAppointment.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                takeTestToolStripMenuItem.Enabled = false;
            }

            DataBack?.Invoke(this, _LocalDrivingLicenseApplicationID);
        }

        private void frmListTestAppointment_Load(object sender, EventArgs e)
        {
            _RefreshListTestAppointments();     
            _TestType = clsTestTypes.Find((clsTestTypes.enTestType)_TestTypeID);
            lblTitle.Text = _TestType.Title;

            if (_TestType.ID == (clsTestTypes.enTestType)1)
            {
                pbTestTypeImage.Image = Properties.Resources.Vision_512;
            }

            if (_TestType.ID == (clsTestTypes.enTestType)2)
            {
                pbTestTypeImage.Image = Properties.Resources.Written_Test_32;
            }

            if (_TestType.ID == (clsTestTypes.enTestType)3)
            {
                pbTestTypeImage.Image = Properties.Resources.Street_Test_32;
            }
        }

        private void dgvLicenseTestAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ctrlLocalDrivingApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
