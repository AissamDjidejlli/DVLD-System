using DVLD.Application.ApplicationType;
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

namespace DVLD.Test.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            dgvTestTypes.DataSource = clsTestTypes.GetAllTestTypes();
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void frmEditTest_DataBack(object sender, clsTestTypes.enTestType TestTypeID)
        {
            _RefreshTestTypesList();

        }


        private void dgvTestTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text = _dtAllTestTypes.Rows.Count.ToString();

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 110;
      
            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 100;
      
            dgvTestTypes.Columns[2].HeaderText = "Discription";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.CurrentRow != null && dgvTestTypes.CurrentRow.Index >= 0)
            {
                int TestTypeID = Convert.ToInt32(dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value);

                frmEditTestTypes frm = new frmEditTestTypes((clsTestTypes.enTestType)TestTypeID);
                frm.ShowDialog();
                frm.DataBack += frmEditTest_DataBack;
                _RefreshTestTypesList();
            }
        }
    }
}
