using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {

        private int _UserID;

        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }

        public clsUser User
        {
            get { return _User; }
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public ctrlUserCard(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            
        }

        public void LoadData(int UserID)
        {

            _UserID = UserID;
            _User = clsUser.Find(UserID);
            

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            if (_User.IsActive == true)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }

        }
        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUserCard_Load(object sender, EventArgs e)
        {
            
        }
    }
}
