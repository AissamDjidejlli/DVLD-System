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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenses_Load(object sender, EventArgs e)
        {

        }

        private void tpLocalLicenses_Click(object sender, EventArgs e)
        {

        }

        public void  LoadDataForLocal(int PersonID)
        {
            dgvLocalLicensesHistory.DataSource = clsLicenses.GetAllLocalPeopleLicenses(PersonID);
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            dgvLocalLicensesHistory.Columns[0].HeaderText = "License ID";
            dgvLocalLicensesHistory.Columns[0].Width = 100;

            dgvLocalLicensesHistory.Columns[1].HeaderText = "Application ID";
            dgvLocalLicensesHistory.Columns[1].Width = 100;

            dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
            dgvLocalLicensesHistory.Columns[2].Width = 250;

            dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvLocalLicensesHistory.Columns[3].Width = 150;

            dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvLocalLicensesHistory.Columns[4].Width = 150;

            dgvLocalLicensesHistory.Columns[5].HeaderText = "IsActive";
            dgvLocalLicensesHistory.Columns[5].Width = 100;

        }


        public void LoadDataForInternational(int PersonID)
        {
           

            dgvInternationalLicensesHistory.DataSource = clsInternationalDrivingLicense.GetAllPersonInternationalLicense(PersonID);
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int License ID";
            dgvInternationalLicensesHistory.Columns[0].Width = 100;
           
            dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicensesHistory.Columns[1].Width = 100;

            dgvInternationalLicensesHistory.Columns[2].HeaderText = "L License ID";
            dgvInternationalLicensesHistory.Columns[2].Width = 100;

            dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvInternationalLicensesHistory.Columns[3].Width = 150;

            dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvInternationalLicensesHistory.Columns[4].Width = 150;

            dgvInternationalLicensesHistory.Columns[5].HeaderText = "IsActive";
            dgvInternationalLicensesHistory.Columns[5].Width = 100;

        }



        private void dgvLocalLicensesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void tbInternationalLicenses_Click(object sender, EventArgs e)
        {

        }
    }
}
