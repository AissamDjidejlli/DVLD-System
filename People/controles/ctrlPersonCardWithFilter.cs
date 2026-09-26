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

namespace DVLD.People.controles
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {



        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


 

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson PersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
            
        }

        public void LoadData(int PersonID)
        {
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(Convert.ToInt32(txtFilterValue.Text));
            gbFilters.Enabled = false;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {


        }

        
     
        private void btnFind_Click(object sender, EventArgs e)
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(Convert.ToInt32(txtFilterValue.Text));
                    break;

                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
            }


            if (OnPersonSelected != null)
                // Raise the event with a parameter
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 1;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ctrlPersonCardWithFilter_DataBack(object sender, int PersonID)
        {
            txtFilterValue.Text = PersonID.ToString();
            cbFilterBy.Text = "Person ID";
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        } 
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePeople frm = new frmAddUpdatePeople();
            frm.DataBack += ctrlPersonCardWithFilter_DataBack;
            frm.ShowDialog();

            
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
