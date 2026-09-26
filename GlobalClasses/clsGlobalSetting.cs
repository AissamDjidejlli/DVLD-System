using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    
    public class clsGlobalSetting
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {

            
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                
                string FilePath = CurrentDirectory + "\\data.txt";
                
                if (Username == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return false;
                }
                
                string DataToSave = Username + "#//#" + Password;
                
                using(StreamWriter Writer = new StreamWriter(FilePath))
                {
                    Writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\data.txt";


                if (File.Exists(FilePath))
                {

                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while((line = reader.ReadLine()) != null)
                        {
                            string[] result = line.Split(new string[] { "#//#" },StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}

