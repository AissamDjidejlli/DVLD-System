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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        private static DataTable _dtAllListDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }


        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtAllListDrivers.DefaultView.RowFilter = string.Empty;

            }
            else
            {
                if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                {
                    _dtAllListDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
                }
                else
                {
                    _dtAllListDrivers.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterColumn, txtFilterValue.Text);
                }
            }

            lblRecordsCount.Text = _dtAllListDrivers.Rows.Count.ToString();

        }


        private void _FillFilterValueComboBox()
        {
            
            string filterValue = string.Empty;
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    filterValue = "DriverID";
                    break;
                case "Person ID":
                    filterValue = "PersonID";
                    break;
                case "National No":
                    filterValue = "NationalNo";
                    break;
                case "Full Name":
                    filterValue = "FullName";
                    break;
                default:
                    filterValue = "None";
                    break;
            }


            
                if (filterValue == "None")
                {
                    txtFilterValue.Enabled = false;
                    txtFilterValue.Visible = false;
                    dgvDrivers.DataSource = _dtAllListDrivers;
                }

                else
                {
                    txtFilterValue.Visible = true;
                    txtFilterValue.Enabled = true;
                    _FindByFilter(filterValue);
                }
        }

        private void dgvDrivers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtAllListDrivers = clsDrivers.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllListDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            dgvDrivers.Columns[0].HeaderText = "Driver ID";
            dgvDrivers.Columns[0].Width = 100;

            dgvDrivers.Columns[1].HeaderText = "Person ID";
            dgvDrivers.Columns[1].Width = 100;

            dgvDrivers.Columns[2].HeaderText = "National No";
            dgvDrivers.Columns[2].Width = 110;

            dgvDrivers.Columns[3].HeaderText = "Full Name";
            dgvDrivers.Columns[3].Width = 300;

            dgvDrivers.Columns[4].HeaderText = "Date";
            dgvDrivers.Columns[4].Width = 140;

            dgvDrivers.Columns[5].HeaderText = "Active License";
            dgvDrivers.Columns[5].Width = 100;


            cbFilterBy.SelectedIndex = 0;

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
            
        }

        private void cbFilterBy_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "DriverID" && cbFilterBy.Text == "PersonID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;

                }
            }
        }
    }
}
