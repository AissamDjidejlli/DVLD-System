using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainReleaseLicenseData
    {
        public static bool GetDetainLicenseInfoByID( int DetainID, ref int LicenseID,
         ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased,
         ref DateTime? ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1 * from DetainedLicenses where DetainID = @DetainID
                            Order by DetainedLicenses.DetainDate desc";
                                    
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if ((reader.Read()))
                {
                    IsFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    IsReleased = (bool)reader["IsReleased"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                 
                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }

                   
                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {

                        ReleasedByUserID = -1;
                    }

                
                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }
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



        public static bool GetDetainLicenseInfoByLicenseID(ref int DetainID,int LicenseID,
         ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased,
         ref DateTime? ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select top 1 * from DetainedLicenses where LicenseID = @LicenseID
                            Order by DetainedLicenses.DetainDate desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if ((reader.Read()))
                {
                    IsFound = true;
   
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    IsReleased = (bool)reader["IsReleased"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];


                    FineFees = Convert.ToSingle(reader["FineFees"]);


  
                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }


                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID = -1;
                    }

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }
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



        public static int AddNewDetainLicense(int LicenseID,DateTime DetainDate,  float FineFees,  int CreatedByUserID,  
            bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {

            //this function will return the new person id if succeeded and -1 if not.
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[DetainedLicenses]
                            ([LicenseID]
                            ,[DetainDate]
                            ,[FineFees]
                            ,[CreatedByUserID]
                            ,[IsReleased]
                            ,[ReleaseDate]
                            ,[ReleasedByUserID]
                            ,[ReleaseApplicationID])
                      VALUES
                            (@LicenseID
                            ,@DetainDate
                            ,@FineFees
                            ,@CreatedByUserID
                            ,@IsReleased
                            ,@ReleaseDate
                            ,@ReleasedByUserID
                            ,@ReleaseApplicationID);
                    
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            if (ReleaseDate != null)
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);


            if (ReleasedByUserID != -1)
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            if (ReleaseApplicationID != -1)
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);




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
                    DetainID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return DetainID;
        }



        public static bool UpdateDetainedLicense(int DetainID, bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  DetainedLicenses  
                            set 
                                IsReleased = @IsReleased,
                                ReleaseDate=@ReleaseDate,
                                ReleasedByUserID = @ReleasedByUserID,  
                                ReleaseApplicationID = @ReleaseApplicationID
                                where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@DetainID", DetainID);

            command.Parameters.AddWithValue("@IsReleased", IsReleased);


            if (ReleaseDate != null)
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);


            if (ReleasedByUserID != -1)
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            if (ReleaseApplicationID != -1)
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);



            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static DataTable GetAllDetainLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from DetainedLicenses_View order by DetainDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static bool IsLicenseDetained(int LicenseID)
        { 
      

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from DetainedLicenses  
                            where DetainedLicenses.LicenseID = @LicenseID and DetainedLicenses.IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    if(insertedID > 0)
                    return true;


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return false;
        }


        public static bool ReleaseLicense(int LicenseID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  DetainedLicenses  
                            set DetainedLicenses.IsReleased = 1
                            where DetainedLicenses.LicenseID = @LicenseID";

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

    }
}
