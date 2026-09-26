using DVLD.Users;
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

namespace DVLD.Application.ApplicationType
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypesList()
        {
            dgvApplicationTypes.DataSource = clsApplicationType.GetAllApplicationTypes();
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void frmEditApplication_DataBack(object sender, int ApplicationTypeID)
        {
            _RefreshApplicationTypesList();

        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 110;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;

            
        }

        private void dgvApplicationTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.CurrentRow != null && dgvApplicationTypes.CurrentRow.Index >= 0)
            {
                int ApplicationTypeID = Convert.ToInt32(dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value);

                frmEditApplicationType frm = new frmEditApplicationType(ApplicationTypeID);
                frm.ShowDialog();
                frm.DataBack += frmEditApplication_DataBack;
                _RefreshApplicationTypesList();
            }
        }
    }
}
