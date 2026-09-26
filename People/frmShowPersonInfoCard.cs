using DVLD.People.controles;
using DVLD_Buisness;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowPersonInfoCard : Form
    {

        public frmShowPersonInfoCard(int PersonID)
        {
             InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        public frmShowPersonInfoCard(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NationalNo);
        }
        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
