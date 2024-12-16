using System;
using System.Data.SqlClient;

namespace HospitalManagementWebsite.Models
{
    public class DoctorRegistrationModelManager
    {
        // Connection string to the database
        private readonly string connectionString = @"data source=SHAHEB;initial catalog=TDM;integrated security=sspi";

        // Method to get the password hash from the database by email
        public string GetPasswordHashByEmail(string email)
        {
            string passwordHash = string.Empty; // Default value if no match is found.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to get the password hash for the provided email
                string query = "SELECT PassWord FROM DoctorRegistration WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email); // Use parameterized query to prevent SQL injection

                try
                {
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // If a matching record is found, retrieve the password hash
                    if (dr.Read())
                    {
                        passwordHash = dr["PassWord"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (optional: log the exception for debugging)
                    Console.WriteLine(ex.Message);
                }
            }

            return passwordHash; // Return the password hash, or an empty string if no record was found
        }

        // Method for doctor registration
        public int DrRegistration(DoctorRegistration registration)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Insert the new doctor's registration data into the database
                string query = "INSERT INTO DoctorRegistration (Name, Email, PassWord, Mob) " +
                               "VALUES (@Name, @Email, @PassWord, @Mob)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", registration.Name);
                cmd.Parameters.AddWithValue("@Email", registration.Email);
                cmd.Parameters.AddWithValue("@PassWord", registration.Password); // The password should be hashed before insertion
                cmd.Parameters.AddWithValue("@Mob", registration.Mob);

                try
                {
                    connection.Open();
                    int insertedRow = cmd.ExecuteNonQuery(); // Execute the query and return the number of rows inserted
                    return insertedRow;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (optional: log the exception for debugging)
                    Console.WriteLine(ex.Message);
                    return 0; // Return 0 if there was an error
                }
            }
        }
    }
}
