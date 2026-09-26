using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        public delegate void DataBackEventHandler(object sender, int UserID);
        public event DataBackEventHandler DataBack;
        public frmUserInfo()
        {
            InitializeComponent();

        }

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            ctrlUserCard1.LoadData(UserID);
        }
        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
