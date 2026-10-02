using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_License;
using DVLD.Test;
using DVLD.Users;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Buisness.clsApplication;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Application.Local_Driving_License
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private static DataTable _dtAllListLocalDrivingLicenseApplications;


        private void _RefreshListLocalDrivingLicenseApplicationsList()
        {
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void frmAddUpdateLocalDrivingLicenseApplications_DataBack(object sender, int LocalDrivingAppID)
        {
            _RefreshListLocalDrivingLicenseApplicationsList();
        }

        private void _GetLocalDrivingLicenseApplicationsStatus()
        {
            switch (cbStatus.Text)
            {
                case "New":
                    _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] = 'New'");
                    break;

                case "Cancelled":
                    _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] = 'Cancelled' ");
                    break;

                case "Completed":
                    _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] = 'Completed' ");
                    break;
            }

            dgvLocalDrivingLicenseApplications.DataSource = _dtAllListLocalDrivingLicenseApplications.DefaultView;
        }
        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Empty;
                dgvLocalDrivingLicenseApplications.DataSource = _dtAllListLocalDrivingLicenseApplications.DefaultView;
            }
            else
            {
                if (FilterColumn == "LocalDrivingLicenseApplicationID")
                {
                    _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
                    dgvLocalDrivingLicenseApplications.DataSource = _dtAllListLocalDrivingLicenseApplications.DefaultView;
                }
                else
                {
                    _dtAllListLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterColumn, txtFilterValue.Text);
                    dgvLocalDrivingLicenseApplications.DataSource = _dtAllListLocalDrivingLicenseApplications.DefaultView;
                }

            }

            lblRecordsCount.Text = _dtAllListLocalDrivingLicenseApplications.DefaultView.Count.ToString();

        }
        private void _FillFilterValueComboBox()
        {
            string filterValue = string.Empty;
            switch (cbFilterBy.Text)
            {
                case "L.D.L.A ID":
                    filterValue = "LocalDrivingLicenseApplicationID";
                    break;
                case "Class Name":
                    filterValue = "ClassName";
                    break;
                case "National No":
                    filterValue = "NationalNo";
                    break;
                case "Full Name":
                    filterValue = "FullName";
                    break;
                case "Status":
                    filterValue = "Status";
                    break;
                default:
                    filterValue = "None";
                    break;
            }


            if (filterValue == "Status")
            {
                txtFilterValue.Visible = false;
                cbStatus.Visible = true;
                _GetLocalDrivingLicenseApplicationsStatus();
            }
            else
            {
                if (filterValue == "None")
                {
                    cbStatus.Visible = false;
                    txtFilterValue.Enabled = false;
                    txtFilterValue.Visible = false;
                    _RefreshListLocalDrivingLicenseApplicationsList();
                }

                else
                {
                    txtFilterValue.Visible = true;
                    txtFilterValue.Enabled = true;
                    cbStatus.Visible = false;
                    _FindByFilter(filterValue);
                    
                }
            }
        }
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }


        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllListLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllListLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.A ID";
            dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;

            dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Class Name";
            dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

            dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No";
            dgvLocalDrivingLicenseApplications.Columns[2].Width = 110;

            dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
            dgvLocalDrivingLicenseApplications.Columns[3].Width = 300;

            dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
            dgvLocalDrivingLicenseApplications.Columns[4].Width = 140;

            dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Test";
            dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;

            dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
            dgvLocalDrivingLicenseApplications.Columns[6].Width = 110;


            cbFilterBy.SelectedIndex = 0;
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();

            txtFilterValue.Text = "";
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
         
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.A ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;

                }
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.DataBack += frmAddUpdateLocalDrivingLicenseApplications_DataBack;
            frm.ShowDialog();
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
                frm.ShowDialog();
                frm.DataBack += frmAddUpdateLocalDrivingLicenseApplications_DataBack;
                dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Application [" + dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value + "]?",
                        "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
                clsLocalDrivingLicenseApplication ApplicationToDelete = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);

                if (ApplicationToDelete != null)
                {
                    if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID))
                    {
                        MessageBox.Show("Deleted Successfully.");
                        dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                        lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed");
                    }

                }
                else
                {
                    MessageBox.Show("Application cannot be deleted.");
                    return;
                }
            }
            
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            clsLocalDrivingLicenseApplication ApplicationToCancel = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);

            if (ApplicationToCancel != null)
            {
                if (clsApplication.Cancel(ApplicationToCancel.ApplicationID))
                {
                    MessageBox.Show("Application Cancelled Successfully.");
                    dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                    lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                }
                else
                {
                    MessageBox.Show("Cancel application Failed");
                }

            }
            else
            {
                MessageBox.Show("Application cannot be Cancelle.");
                return;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmDrivingLicenseInfo frm = new frmDrivingLicenseInfo(LocalDrivingLicenseApplicationID);
                frm.ShowDialog();
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmListTestAppointment frm = new frmListTestAppointment(LocalDrivingLicenseApplicationID, 1);
                frm.DataBack += frmAddUpdateLocalDrivingLicenseApplications_DataBack;
                frm.ShowDialog();

                dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
            
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmListTestAppointment frm = new frmListTestAppointment(LocalDrivingLicenseApplicationID, 2);
                frm.DataBack += frmAddUpdateLocalDrivingLicenseApplications_DataBack;
                frm.ShowDialog();

                dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmListTestAppointment frm = new frmListTestAppointment(LocalDrivingLicenseApplicationID, 3);
                frm.DataBack += frmAddUpdateLocalDrivingLicenseApplications_DataBack;
                frm.ShowDialog();

                dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {

        }

        private void TestEnable(int LocalDrivingLicenseApplicationID)
        {
            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;

            if (clsTests.IsTestPassed(LocalDrivingLicenseApplicationID, 1))
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }

            if (clsTests.IsTestPassed(LocalDrivingLicenseApplicationID, 2))
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;
            }

            if (clsTests.IsTestPassed(LocalDrivingLicenseApplicationID, 3))
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                ScheduleTestsMenue.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true ;
            }

        }
        private void dgvLocalDrivingLicenseApplications_SelectionChanged(object sender, EventArgs e)
        {
            editToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            CancelApplicaitonToolStripMenuItem.Enabled = true;
            ScheduleTestsMenue.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;



            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                string Status = (dgvLocalDrivingLicenseApplications.CurrentRow.Cells["Status"].Value).ToString();

                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);


                if (Status == "New")
                {
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = false;

                    TestEnable(LocalDrivingLicenseApplicationID);
                }

                else if (Status == "Cancelled")
                {
                    editToolStripMenuItem.Enabled = false;
                    DeleteApplicationToolStripMenuItem.Enabled = false;
                    CancelApplicaitonToolStripMenuItem.Enabled = false;
                    ScheduleTestsMenue.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = false;
                }

                //(Status == "Completed")
                else 
                {
                    editToolStripMenuItem.Enabled = false;
                    DeleteApplicationToolStripMenuItem.Enabled = false;
                    CancelApplicaitonToolStripMenuItem.Enabled = false;
                    ScheduleTestsMenue.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                }
            }


            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
                frm.ShowDialog();

                _RefreshListLocalDrivingLicenseApplicationsList();
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null && dgvLocalDrivingLicenseApplications.CurrentRow.Index >= 0)
            {
                string NationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["NationalNo"].Value.ToString();

                frmShowDriverCardInfo frm = new frmShowDriverCardInfo(NationalNo);
                frm.ShowDialog();

                _RefreshListLocalDrivingLicenseApplicationsList();
            }
        }

        private void dgvLocalDrivingLicenseApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["NationalNo"].Value.ToString();
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(NationalNo);
            frm.ShowDialog();

            
        }
    }
}

