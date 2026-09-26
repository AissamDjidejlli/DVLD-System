using DVLD.People.controles;
using DVLD.Validate;
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
    public partial class frmEditApplicationType : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;


        private int _ApplicationTypeID;
        private clsApplicationType _ApplicationType;

        
        public frmEditApplicationType()
        {
            InitializeComponent();
        }

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationType = clsApplicationType.Find(ApplicationTypeID);
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();

            if (_ApplicationType != null)
            {
                txtTitle.Text = _ApplicationType.Title;
                txtFees.Text = _ApplicationType.Fees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                _ApplicationType.Title = txtTitle.Text;
                _ApplicationType.Fees = Convert.ToSingle(txtFees.Text);


                 if (_ApplicationType.Save())
                 {
                         
                      MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      DataBack?.Invoke(this, _ApplicationTypeID);
                      this.Close();
                 }
                else
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            
            if (!clsValidate.IsValidFill(txtTitle, errorProvider1))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Box fill");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }


        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void txtFees_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidate.IsValidFill(txtFees, errorProvider1))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
        }
    }
}

