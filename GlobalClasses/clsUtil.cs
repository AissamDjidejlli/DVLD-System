using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GlobalClasses
{
    internal class clsUtil
    {

        public static string GenerateGuid()
        {
            Guid NewGuid = Guid.NewGuid();
            return NewGuid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameByGuid(string SourceFile)
        {
            string fileName = SourceFile;
            FileInfo fi = new FileInfo(fileName);
            string ext = fi.Extension;
            return GenerateGuid() + ext;
        }

        public static bool CopyImageToProjectImageFolder(ref string SourceFile)
        {
            string DestinationFile = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFile))
            {
                return false;
            }

            string destinationFile = DestinationFile + ReplaceFileNameByGuid(SourceFile);
            try
            {
                File.Copy(SourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = destinationFile;
            return true;
        }
    }
}
