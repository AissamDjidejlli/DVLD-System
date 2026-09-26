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

namespace DVLD.Test.TestTypes
{
    public partial class frmEditTestTypes : Form
    {
        public delegate void DataBackEventHandler(object sender, clsTestTypes.enTestType TestTypeID);
        public event DataBackEventHandler DataBack;


        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsTestTypes _TestType;


        public frmEditTestTypes()
        {
            InitializeComponent();
        }

        public frmEditTestTypes(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
           
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            lblTestTypeID.Text = ((int)_TestTypeID).ToString();

            _TestType = clsTestTypes.Find(_TestTypeID);

            if (_TestType != null)
            {
                txtTitle.Text = _TestType.Title;
                txtDescription.Text = _TestType.Discription;
                txtFees.Text = _TestType.Fees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            _TestType.Title = txtTitle.Text;
            _TestType.Discription = txtDescription.Text;
            _TestType.Fees = Convert.ToSingle(txtFees.Text);


            if (_TestType.Save())
            {

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _TestTypeID);
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
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

        private void txtFees_Validating(object sender, CancelEventArgs e)
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
    }
}
