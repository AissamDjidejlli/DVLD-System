using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_License;
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

namespace DVLD.Application.Detain_and_Release_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        private static DataTable _dtAllDetainLicense;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }


        private void _RefreshDetainLicenseList()
        {
            dgvDetainedLicenses.DataSource = clsDetainReleaseLicense.GetAllDetainLicenses();
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }


        private void frmDetainReleaseLicense_DataBack(object sender, int DetainID)
        {
            _RefreshDetainLicenseList();
        }

        private void _GetReleasedLicense()
        {
            switch (cbIsReleased.Text)
            {
                case "All":
                    _dtAllDetainLicense.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllDetainLicense.DefaultView.RowFilter = "[IsReleased] = true";

                    break;

                case "No":
                    _dtAllDetainLicense.DefaultView.RowFilter = "[IsReleased] = false";

                    break;
            }

            dgvDetainedLicenses.DataSource = _dtAllDetainLicense.DefaultView;
        }
        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtAllDetainLicense.DefaultView.RowFilter = string.Empty;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                _dtAllDetainLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
                dgvDetainedLicenses.DataSource = _dtAllDetainLicense.DefaultView;
            }
            else
            {
                _dtAllDetainLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);

            }

            dgvDetainedLicenses.DataSource = _dtAllDetainLicense.DefaultView;
            lblTotalRecords.Text = _dtAllDetainLicense.Rows.Count.ToString();

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
                _GetReleasedLicense();
            }
            else
            {
                if (filterValue == "None")
                {
                    txtFilterValue.Visible = false;
                    txtFilterValue.Enabled = false;
                    cbIsReleased.Visible = false;
                    _RefreshDetainLicenseList();
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

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtAllDetainLicense = clsDetainReleaseLicense.GetAllDetainLicenses();
            dgvDetainedLicenses.DataSource = _dtAllDetainLicense;
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.CurrentRow != null && dgvDetainedLicenses.CurrentRow.Index >= 0)
            {

                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 70;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 70;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 200;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 90;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 200;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No";
                dgvDetainedLicenses.Columns[6].Width = 100;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 320;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App ID";
                dgvDetainedLicenses.Columns[8].Width = 100;
            }

            cbFilterBy.SelectedIndex = 0;
            cbIsReleased.SelectedIndex = 0;

        

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            _FillFilterValueComboBox();
            txtFilterValue.Text = "";
           
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
            frm.DataBack += frmDetainReleaseLicense_DataBack;
            _RefreshDetainLicenseList();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            frm.DataBack += frmDetainReleaseLicense_DataBack;
            _RefreshDetainLicenseList();
        }

        private void dgvDetainedLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.CurrentRow != null && dgvDetainedLicenses.CurrentRow.Index >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);

                frmShowPersonInfoCard frm = new frmShowPersonInfoCard(clsLicenses.FindByID(LicenseID).Application.PersonID);
                frm.ShowDialog();

                _RefreshDetainLicenseList();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.CurrentRow != null && dgvDetainedLicenses.CurrentRow.Index >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);

                frmShowDriverCardInfo frm = new frmShowDriverCardInfo(LicenseID);
                frm.ShowDialog();

                _RefreshDetainLicenseList();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.CurrentRow != null && dgvDetainedLicenses.CurrentRow.Index >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);

                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsLicenses.FindByID(LicenseID).Application.PersonInfo.NationalNo);
                frm.ShowDialog();

                _RefreshDetainLicenseList();
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.CurrentRow != null && dgvDetainedLicenses.CurrentRow.Index >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);

                frmReleaseLicense frm = new frmReleaseLicense(LicenseID);
                frm.ShowDialog();
                frm.DataBack += frmDetainReleaseLicense_DataBack;
                _RefreshDetainLicenseList();
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void dgvDetainedLicenses_SelectionChanged(object sender, EventArgs e)
        {
            if ((bool)dgvDetainedLicenses.CurrentRow.Cells["IsReleased"].Value)
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
        }
    }
}
