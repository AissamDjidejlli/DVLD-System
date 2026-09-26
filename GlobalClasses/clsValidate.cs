using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Validate
{
    public class clsValidate
    {
        public static bool IsValidFill(Control control ,ErrorProvider errorprovider1,string ErrorText = "This Fiels is Empty")
        {
            

            if (string.IsNullOrWhiteSpace(control.Text))
            {
                errorprovider1.SetError(control, ErrorText);
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool IsValidEmail(Control control, ErrorProvider errorprovider1,string EmailPattern = "@gmail.com", string ErrorText = "Wrong Email Format")
        {
            if(control.Text.Contains(EmailPattern))
            {
                return true;
            }
            else
            {
                errorprovider1.SetError(control, ErrorText);
                return false;
            }
                
        }

        public static void IsValidAge(DateTimePicker dateTimePicker)
        {
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker.Value = dateTimePicker.MaxDate;
            dateTimePicker.MinDate = DateTime.Now.AddYears(-100);
        }



    }
}
