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

namespace DVLD.Test
{
    public partial class frmTakeTest : Form
    {
        private clsTests _Test;

        private int _TestAppointmentID;
        
        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlShudledTest1.LoadData(_TestAppointmentID);

            _Test = new clsTests();

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestAppointment = clsTestAppointment.FindByID(_TestAppointmentID);

            if (_Test.TestAppointment.IsLocked)
            {
                if (_Test.TestResult == 1) { rbPass.Checked = true; }
                else { rbFail.Checked = true; }

                rbPass.Enabled = false;
                rbFail.Enabled = false;
                lblUserMessage.Visible = true;
            }
            else
            {
                rbPass.Enabled = true;
                rbFail.Enabled = true;
                lblUserMessage.Visible = false;
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           

            

            if (rbPass.Checked == true){_Test.TestResult = 1;}
            else { _Test.TestResult = 0;}

            _Test.CreatedByUserID = clsGlobalSetting.CurrentUser.UserID;

            _Test.Notes = txtNotes.Text;

            if (_Test.Save())
            {
               
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if(_Test.TestAppointment.LockAppointment())
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
