using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;


namespace HospitalManagementWebsite.Models.InternalUser
{
    public class InternalUserModelManager
    {
        // Connection string to the database
        private string connectionString = @"data source=SHAHEB;initial catalog=TDM;integrated security=sspi";

        // Method to get the password hash and department code from the database by username
        public (string passwordHash, int departmentCode) GetUserCredentialsByUsername(string username)
        {
            string passwordHash = string.Empty;
            int departmentCode = -1;  // Default value for departmentCode if no result is found

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to get the password hash and department code for the provided username
                string query = "SELECT INTPASSWORD, INTDEPARTMENTCODE FROM INTERNALUSERDETAILS WHERE EMAIL = @Username";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username); // Use parameterized query to prevent SQL injection

                try
                {
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // If a matching record is found, retrieve the password hash and department code
                    if (dr.Read())
                    {
                        // Handle potential DBNull values explicitly
                        passwordHash = dr["INTPASSWORD"] != DBNull.Value ? dr["INTPASSWORD"].ToString() : string.Empty;
                        departmentCode = dr["INTDEPARTMENTCODE"] != DBNull.Value ? Convert.ToInt32(dr["INTDEPARTMENTCODE"]) : -1;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (optional: log the exception for debugging)
                    Console.WriteLine(ex.Message);
                }
            }

            return (passwordHash, departmentCode); // Return both password hash and department code
        }

        // Method for user login with department code verification
        public bool Login(string username, string password, int departmentCode)
        {
            // Get the stored password hash and department code from the database
            var userCredentials = GetUserCredentialsByUsername(username);

            if (string.IsNullOrEmpty(userCredentials.passwordHash))
            {
                return false; // No user found with this username
            }

            // Verify the password
            bool isPasswordValid = VerifyPassword(password, userCredentials.passwordHash, username);

            // Verify department code (Ensure comparison is between integers)
            bool isDepartmentValid = userCredentials.departmentCode == departmentCode;

            // Return true if both the password and department code are valid
            return isPasswordValid && isDepartmentValid;
        }

        // Method for internal user registration
        public int RegisterInternalUser(InternalUser user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Insert the new internal user's registration data into the database
                string query = "INSERT INTO INTERNALUSERDETAILS (" +
                    "ITNUSERNAME," +
                    " INTPASSWORD," +
                    " INTDEPARTMENTCODE," +
                    " EMAIL," +
                    " MOBILENO," +
                    " DOB," +
                    " GENDER," +
                    " DOJ ," +
                    " INTDESIGNATIONCODE ," +
                    " EMPLOYEECODE) " +
                    "VALUES (" +
                    "@ItnUsername," +
                    "@IntPassword," +
                    "@IntDepartmentCode," +
                    "@Email," +
                    "@MobileNo," +
                    "@Dob," +
                    "@Gender," +
                    "@Doj," +
                    "@INTDESIGNATIONCODE," +
                    "@EMPLOYEECODE" +
                    ")";

                SqlCommand cmd = new SqlCommand(query, connection);

                // Add parameters explicitly with data types
                cmd.Parameters.Add("@ItnUsername", SqlDbType.NVarChar).Value = user.ITNUSERNAME;
                cmd.Parameters.Add("@IntDepartmentCode", SqlDbType.Int).Value = user.INTDEPARTMENTCODE;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.EMAIL ?? (object)DBNull.Value;
                cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = user.MOBILENO ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Dob", SqlDbType.DateTime).Value = user.DOB ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = user.GENDER;
                cmd.Parameters.Add("@Doj", SqlDbType.DateTime).Value = user.DOJ ?? (object)DBNull.Value;

                // Manually assign values for the fields that do not have data currently
                cmd.Parameters.Add("@INTDESIGNATIONCODE", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@EMPLOYEECODE", SqlDbType.NVarChar).Value = "UNKNOWN";  // Example: default employee code (e.g., 'UNKNOWN')

                // Check if the user is an admin (special case, no password hashing)
                if (user.ITNUSERNAME.ToLower() == "admin@mail.com")
                {
                    // No hashing for admin, just pass the password directly
                    cmd.Parameters.Add("@IntPassword", SqlDbType.NVarChar).Value = user.INTPASSWORD;
                }
                else
                {
                    // Hash the password using SHA256 before storing it
                    string hashedPassword = HashPassword(user.INTPASSWORD);
                    cmd.Parameters.Add("@IntPassword", SqlDbType.NVarChar).Value = hashedPassword;
                }

                try
                {
                    connection.Open();
                    int insertedRow = cmd.ExecuteNonQuery(); // Execute the query and return the number of rows inserted
                    return insertedRow;
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine("Error: " + ex.Message);
                    return 0; // Return 0 if there was an error
                }
            }
        }



        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash of the password
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Format as hexadecimal string
                }

                return builder.ToString(); // Return the hashed password
            }
        }

        // Method to verify the password against the stored hash
        public bool VerifyPassword(string inputPassword, string storedHash, string username)
        {
            if (username.ToLower() == "admin@mail.com")
            {
                // For admin, directly compare the passwords (no hashing)
                return inputPassword == storedHash;
            }
            else
            {
                // For other users, compare the hashes
                string hashedInputPassword = HashPassword(inputPassword);
                return hashedInputPassword == storedHash;
            }
        }



        // Method to fetch departments from the database
        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            // Ensure your connection string is correct and securely stored


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to get DepartmentID, DepartmentName, and DepartmentCode
                string query = "SELECT DepartmentID, DepartmentName, DepartmentCode FROM Departments WHERE ActiveFlag = 1";

                SqlCommand cmd = new SqlCommand(query, connection);

                try
                {
                    connection.Open();  // Open the SQL connection
                    SqlDataReader reader = cmd.ExecuteReader();  // Execute the query

                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID")),
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                            DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode"))
                        });
                    }

                    reader.Close();  // Close the reader when done
                }
                catch (Exception ex)
                {
                    // Handle exception (log or rethrow)
                    Console.WriteLine(ex.Message);
                    // Optionally log the exception to a file or logging system
                }
            }

            return departments;
        }


        public List<Department> GetDesignationsByDepartment(string departmentID)
        {
            List<Department> departmentDesignations = new List<Department>();
            int departmentId = Convert.ToInt32(departmentID);  // Ensure proper conversion

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to get department and designation information
                string query = @"
            SELECT 
                D.DepartmentName,
                D.DepartmentCode,
                P.PositionCode,
                P.PositionName AS Designations,
                P.JobDescription,
                P.ActiveFlag AS PositionActiveFlag
            FROM 
                Departments D
            LEFT JOIN 
                Positions P ON D.DepartmentCode = P.DepartmentCode
            WHERE 
                D.ActiveFlag = 1 AND D.DepartmentID = @DepartmentID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                try
                {
                    connection.Open();  // Open the SQL connection
                    SqlDataReader reader = cmd.ExecuteReader();  // Execute the query

                    while (reader.Read())
                    {
                        departmentDesignations.Add(new Department
                        {
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                            DepartmentCode = reader.GetString(reader.GetOrdinal("DepartmentCode")),
                            PositionName = reader.GetString(reader.GetOrdinal("Designations")),
                            PositionCode = reader.GetString(reader.GetOrdinal("PositionCode")),
                            JobDescription = reader.GetString(reader.GetOrdinal("JobDescription")),
                            PositionActiveFlag = reader.GetBoolean(reader.GetOrdinal("PositionActiveFlag"))
                        });
                    }

                    reader.Close();  // Close the reader when done
                }
                catch (Exception ex)
                {
                    // Handle exception (log or rethrow)
                    Console.WriteLine(ex.Message);
                    // Optionally log the exception to a file or logging system
                }
            }

            return departmentDesignations;
        }

    }

}



