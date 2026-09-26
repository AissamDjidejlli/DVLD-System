using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicensesData
    {
        public static bool GetLicensesInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseCLassID,
          ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            // استخدام ConnectionString وإنشاء SqlConnection جديد داخل كل دالة
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        // فتح الاتصال الخاص بهذه الدالة فقط
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                ApplicationID = (int)reader["ApplicationID"];
                                DriverID = (int)reader["DriverID"];
                                LicenseCLassID = (int)reader["LicenseCLass"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                PaidFees = Convert.ToSingle(reader["PaidFees"]);
                                IsActive = (bool)reader["IsActive"];
                                IssueReason = (byte)reader["IssueReason"];

                                if (reader["Notes"] != DBNull.Value)
                                    Notes = (string)reader["Notes"];
                                else
                                    Notes = "";

                                CreatedByUserID = (int)reader["CreatedByUserID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsFound = false;
                    }
                    // عند الخروج من بلوك using يتم إغلاق الاتصال وتحريره تلقائياً حتى لو حدث Exception
                }
            }

            return IsFound;
        }

        public static bool GetLicensesInfoByDriverID(ref int LicenseID, ref int ApplicationID,  int DriverID, ref int LicenseCLassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Licenses where DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if ((reader.Read()))
                {
                    IsFound = true;


                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseID = (int)reader["LicenseID"];
                    LicenseCLassID = (int)reader["LicenseCLass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];


                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

                    CreatedByUserID = (int)reader["CreatedByUserID"];


                }
                else
                {
                    IsFound = false;
                }
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }



        public static int AddNewLicenses(int ApplicationID,  int DriverID,  int LicenseCLass,
            DateTime IssueDate,  DateTime ExpirationDate,  string Notes,  float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses (ApplicationID,DriverID,LicenseCLass,IssueDate,ExpirationDate,Notes
                                                          ,PaidFees, IsActive, IssueReason,CreatedByUserID)
                              VALUES (@ApplicationID,@DriverID,@LicenseCLass,@IssueDate,@ExpirationDate,@Notes
                                                          ,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicationID", @ApplicationID);
            command.Parameters.AddWithValue("DriverID", @DriverID);
            command.Parameters.AddWithValue("LicenseCLass", @LicenseCLass);
            command.Parameters.AddWithValue("IssueDate", @IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", @ExpirationDate);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("PaidFees", @PaidFees);
            command.Parameters.AddWithValue("IsActive", @IsActive);
            command.Parameters.AddWithValue("IssueReason", @IssueReason);
            command.Parameters.AddWithValue("CreatedByUserID", @CreatedByUserID);


            try
            {

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;
        }

        public static bool HasPersonLicenseFromSameClasse(int PersonID,int LicenseClass)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found=1 
                    from Licenses inner join  Applications on Licenses.ApplicationID = Applications.ApplicationID
                    where Applications.ApplicantPersonID = @PersonID and Licenses.LicenseClass = @LicenseClass";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;

        }



        public static bool DisactivateLicense(int LicenseID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Licenses  
                            set Licenses.IsActive = 0
                            where Licenses.LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }



        public static DataTable GetAllPersonLocalLicense(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID,Licenses.ApplicationID,LicenseClasses.ClassName,
                    Licenses.IssueDate,Licenses.ExpirationDate,Licenses.IsActive
                         FROM  Licenses INNER JOIN 
                               LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID  INNER JOIN 
                               Drivers on Licenses.DriverID = Drivers.DriverID
                               where Drivers.PersonID = @PersonID
                    ORDER BY Licenses.IssueDate";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                connection.Open();
               

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
