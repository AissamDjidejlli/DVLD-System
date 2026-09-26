using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People.controles;
using DVLD_Buisness;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        
        public frmListPeople()
        {
            InitializeComponent();
        }

        private DataTable _dtPeaple = clsPerson.GetAllPeople();
        private void _FillFilterComboBox()
        {
            cbFilterBy.Items.Clear();
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("PersonID");
            cbFilterBy.Items.Add("NationalNo");
            cbFilterBy.Items.Add("FirstName");
            cbFilterBy.Items.Add("SecondName");
            cbFilterBy.Items.Add("ThirdName");
            cbFilterBy.Items.Add("LastName");
            cbFilterBy.Items.Add("Nationality");
            cbFilterBy.Items.Add("Phone");
            cbFilterBy.Items.Add("Email");


            cbFilterBy.SelectedIndex = 0;

        }
        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtPeaple.DefaultView.RowFilter = string.Empty;
                return;
            }
            else
            {
                if (FilterColumn == "PersonID")
                {
                    _dtPeaple.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);

                }
                else 
                {
                    _dtPeaple.DefaultView.RowFilter = string.Format("[{0}] like {1}", FilterColumn, txtFilterValue.Text);
                }
            }

            dgvPeople.DataSource = _dtPeaple.DefaultView;
            lblRecordNumber.Text = dgvPeople.Rows.Count.ToString();

        }
        private void _FillFilterValueComboBox()
        {
            switch (cbFilterBy.Text)
            {
                case "None":
                    txtFilterValue.Enabled = false;
                    dgvPeople.DataSource = clsPerson.GetAllPeople();
                    break;

                case "PersonID":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("PersonID");

                    break;

                case "NationalNo":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("NationalNo");
                    break;

                case "FirstName":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("FirstName");
                    break;

                case "SecondName":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("SecondName");
                    break;

                case "Third Name":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("ThirdName");
                    break;

                case "LastName":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("LastName");
                    break;

                case "Nationality":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("Nationality");
                    break;

                case "Phone":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("Phone");
                    break;


                case "Email":
                    txtFilterValue.Focus();
                    txtFilterValue.Enabled = true;
                    _FindByFilter("Email");
                    break;




            }
        }

        private void _RefreshPeopleList()
        {
            dgvPeople.DataSource = _dtPeaple;
            lblRecordNumber.Text = dgvPeople.Rows.Count.ToString();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPeople.CurrentRow != null && dgvPeople.CurrentRow.Index >= 0)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);
                frmAddUpdatePeople frm = new frmAddUpdatePeople(PersonID);
                frm.ShowDialog();
                _RefreshPeopleList();
            }

        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmListPeople_Load_1(object sender, EventArgs e)
        {
           
            _FillFilterComboBox();
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            
                frmAddUpdatePeople frm = new frmAddUpdatePeople();
                frm.DataBack += frmAddUpdatePeople_DataBack;
                frm.ShowDialog();
                dgvPeople.DataSource = clsPerson.GetAllPeople();


        }

        private void frmAddUpdatePeople_DataBack(object sender, int PersonID)
        {
            _RefreshPeopleList();
        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                frmAddUpdatePeople frm = new frmAddUpdatePeople();
                frm.ShowDialog();
                dgvPeople.DataSource = clsPerson.GetAllPeople();
        } 

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow != null && dgvPeople.CurrentRow.Index >= 0)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

                frmAddUpdatePeople frm = new frmAddUpdatePeople(PersonID);
                frm.ShowDialog();
                frm.DataBack += frmAddUpdatePeople_DataBack;
                dgvPeople.DataSource = clsPerson.GetAllPeople();
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]?",
                        "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

              
                    if (clsPerson.DeletePerson(PersonID))
                    {
                        MessageBox.Show("Deleted Successfully.");
                      dgvPeople.DataSource = clsPerson.GetAllPeople();
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed");
                    }
               
            }  
        }

        private void showCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow != null && dgvPeople.CurrentRow.Index >= 0)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

                frmShowPersonInfoCard frm = new frmShowPersonInfoCard(PersonID);
                frm.ShowDialog();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;

                }
            }
        }

        private void lblRecordNumber_Click(object sender, EventArgs e)
        {
            
        }

        private void lblRecordNumber_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
