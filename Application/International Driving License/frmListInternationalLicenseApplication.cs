using DVLD.Application.International_Driving_License.Controls;
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

namespace DVLD.Licenses.Interantional_Driving_License
{
    public partial class frmListInternationalLicenseApplication : Form
    {
        private static DataTable _dtAllInternationalDrivingLicenseApplications;


        private void _RefreshListLocalDrivingLicenseApplicationsList()
        {
            dgvInternationalLicenses.DataSource = clsInternationalDrivingLicense.GetAllInternationalLicense();
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        public frmListInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void _GetInternationalDrivingLicenseApplicationsStatus()
        {
            switch (cbIsReleased.Text)
            {
                case "All":
                    _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "[IsActive] = true";
                  
                    break;

                case "No":
                    _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "[IsActive] = false";
                   
                    break;
            }

            dgvInternationalLicenses.DataSource = _dtAllInternationalDrivingLicenseApplications.DefaultView;
        }
        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
               
            }

            dgvInternationalLicenses.DataSource = _dtAllInternationalDrivingLicenseApplications.DefaultView;
            lblInternationalLicensesRecords.Text = _dtAllInternationalDrivingLicenseApplications.Rows.Count.ToString();

        }

       

        private void _FillFilterValueComboBox()
        {
            string filterValue = string.Empty;
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    filterValue = "InternationalLicenseID";
                    break;
                case "Application ID":
                    filterValue = "ApplicationID";
                    break;
                case "Driver ID":
                    filterValue = "DriverID";
                    break;
                case "Local License ID":
                    filterValue = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    filterValue = "IsActive";
                    break;
                default:
                    filterValue = "None";
                    break;
            }


            if (filterValue == "IsActive")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                _GetInternationalDrivingLicenseApplicationsStatus();
            }
            else
            {
                if (filterValue == "None")
                {
                    txtFilterValue.Visible = false;
                    txtFilterValue.Enabled = false;
                    cbIsReleased.Visible = false;
                    _RefreshListLocalDrivingLicenseApplicationsList();
                }

                else
                {
                    
                    txtFilterValue.Visible = true;
                    txtFilterValue.Enabled = true;
                    cbIsReleased.Visible = false;
                    _FindByFilter(filterValue);
                }
            }
        }

        private void frmListInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _dtAllInternationalDrivingLicenseApplications = clsInternationalDrivingLicense.GetAllInternationalLicense();
            dgvInternationalLicenses.DataSource = _dtAllInternationalDrivingLicenseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            dgvInternationalLicenses.Columns[0].HeaderText = "International License ID";
            dgvInternationalLicenses.Columns[0].Width = 100;

            dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicenses.Columns[1].Width = 100;

            dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
            dgvInternationalLicenses.Columns[2].Width = 100;

            dgvInternationalLicenses.Columns[3].HeaderText = "Issued Using Local License ID";
            dgvInternationalLicenses.Columns[3].Width = 120;
           
            dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
            dgvInternationalLicenses.Columns[4].Width = 200;
     
            dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
            dgvInternationalLicenses.Columns[5].Width = 200;

            dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
            dgvInternationalLicenses.Columns[6].Width = 100;


            cbFilterBy.SelectedIndex = 0;
            cbIsReleased.SelectedIndex = 0;
        }

        private void dgvInternationalLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            _FillFilterValueComboBox();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            _FillFilterValueComboBox();
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {   
           if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {e.Handled = true;}    
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmIssueInternationalDrivingLicense frm = new frmIssueInternationalDrivingLicense();
            frm.ShowDialog();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInternationalLicenses.CurrentRow != null && dgvInternationalLicenses.CurrentRow.Index >= 0)
            {
                int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value);

                frmShowPersonInfoCard frm = new frmShowPersonInfoCard(clsDrivers.Find(DriverID).PersonID);
                frm.ShowDialog();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInternationalLicenses.CurrentRow != null && dgvInternationalLicenses.CurrentRow.Index >= 0)
            {
                int InternationalDrivingLicenseID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells["InternationalLicenseID"].Value);

                frmShowInternationalDrivingLicenseInfocs frm = new frmShowInternationalDrivingLicenseInfocs(InternationalDrivingLicenseID);
                frm.ShowDialog();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInternationalLicenses.CurrentRow != null && dgvInternationalLicenses.CurrentRow.Index >= 0)
            {
                int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value);

                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDrivers.Find(DriverID).PersonInfo.NationalNo);
                frm.ShowDialog();
            }
        }
    }
}
