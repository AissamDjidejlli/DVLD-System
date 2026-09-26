using DVLD.People;
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

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {

        private static DataTable _dtAllUsers = clsUser.GetAllUsers();


        private void _RefreshUserList()
        {
            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void frmAddUpdateUsers_DataBack(object sender, int PersonID)
        {
            _RefreshUserList();
        }




        private void _GetUserActiveOrNot()
        {
            switch (cbIsActive.Text)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = string.Empty;
                    break;

                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[IsActive] = 1");
                    break;

                case "No":
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[IsActive] = 0");
                    break;
            }
        }

        private void _FindByFilter(string FilterColumn)
        {
            if (txtFilterValue.Text == "")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Empty;
                return;
            }
            else
            {
                if (FilterColumn == "UserID" || FilterColumn == "PersonID")
                {
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);

                }
                else
                {
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterColumn, txtFilterValue.Text);                
                }
            }

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }
        private void _FillFilterValueComboBox()
        {
           if (cbFilterBy.Text == "IsActive")
           {
               txtFilterValue.Visible = false;
               cbIsActive.Visible = true;
               cbIsActive.SelectedIndex = 0;
               _GetUserActiveOrNot();
           }
           else
           {
               if (cbFilterBy.Text == "None")
               {
                   txtFilterValue.Enabled = false;
                   txtFilterValue.Visible = false;
                   _RefreshUserList();
               }

               else
               {
                   txtFilterValue.Visible = true;
                   txtFilterValue.Enabled = true;
                   cbIsActive.Visible = false;
                   _FindByFilter(cbFilterBy.Text);
               }
           }
            
        }


        public frmListUsers()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.DataBack += frmAddUpdateUsers_DataBack;
            frm.ShowDialog();
            _RefreshUserList();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {        
            _RefreshUserList();
            cbFilterBy.SelectedIndex = 0;
            _FillFilterValueComboBox();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.DataBack += frmAddUpdateUsers_DataBack;
            frm.ShowDialog();
            _RefreshUserList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Index >= 0)
            {
                int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);

                frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
                frm.ShowDialog();
                frm.DataBack += frmAddUpdateUsers_DataBack;
                _RefreshUserList();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete User [" + dgvUsers.CurrentRow.Cells[0].Value + "]?",
                        "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
                clsUser userToDelete = clsUser.Find(UserID);

                if (userToDelete != null && userToDelete.UserID != clsGlobalSetting.CurrentUser.UserID)
                {
                    if (clsUser.DeleteUser(UserID))
                    {
                        MessageBox.Show("Deleted Successfully.");
                        dgvUsers.DataSource = clsUser.GetAllUsers();
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed");
                    }
                   
                }
                else
                {
                    MessageBox.Show("User cannot be deleted.");
                    return;
                }

                

            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Index >= 0)
            {
                int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);

                frmChangePassword frm = new frmChangePassword(UserID);
                frm.ShowDialog();
                frm.DataBack += frmAddUpdateUsers_DataBack;
                dgvUsers.DataSource = clsUser.GetAllUsers();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "UserID" || cbFilterBy.Text == "PersonID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;

                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
            _GetUserActiveOrNot();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _FillFilterValueComboBox();
            
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _GetUserActiveOrNot();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Index >= 0)
            {
                int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);

                frmUserInfo frm = new frmUserInfo(UserID);
                frm.ShowDialog();
                frm.DataBack += frmAddUpdateUsers_DataBack;
                dgvUsers.DataSource = clsUser.GetAllUsers();
            }
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
