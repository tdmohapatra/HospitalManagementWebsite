using System;
using System.Collections.Generic;
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
            int departmentCode = -1;  // Default value for departmentCode in case no result is found

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to get the password hash and department code for the provided username
                string query = "SELECT INTPASSWORD, INTDEPARTMENTCODE FROM INTERNALUSERDETAILS WHERE ITNUSERNAME = @Username";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username); // Use parameterized query to prevent SQL injection

                try
                {
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // If a matching record is found, retrieve the password hash and department code
                    if (dr.Read())
                    {
                        passwordHash = dr["INTPASSWORD"].ToString();
                        departmentCode = Convert.ToInt32(dr["INTDEPARTMENTCODE"]);  // Ensure departmentCode is an integer
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
                //string query = "INSERT INTO INTERNALUSERDETAILS (ITNUSERNAME, INTPASSWORD, INTDEPARTMENTCODE, INTDESIGNATIONCODE, " +
                //               "INTFLGSTATUS, EMAIL, MOBILENO, DOB, EMPLOYEECODE, GENDER, DOJ,DOB ) " +
                //               "VALUES (@ItnUsername, @IntPassword, @IntDepartmentCode, @IntDesignationCode, @IntFlgStatus, @Email, @MobileNo, " +
                //               "@Dob, @EmployeeCode, @Gender, @Doj,@DoB)";
                string query = "INSERT INTO INTERNALUSERDETAILS (" +
                    "ITNUSERNAME," +
                    " INTPASSWORD," +
                    " INTDEPARTMENTCODE," +
                         "EMAIL, " +
                         "MOBILENO," +
                         " DOB," +
                         " GENDER," +
                         " DOJ ) " +
                         "VALUES (" +
                         "@ItnUsername," +
                         " @IntPassword," +
                         " @IntDepartmentCode, " +
                         "@Email," +
                         " @MobileNo, " +
                         "@Dob," +
                         " @Gender," +
                         " @Doj" +
                         ")";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ItnUsername", user.ITNUSERNAME);
                cmd.Parameters.AddWithValue("@IntPassword", user.INTPASSWORD);
                cmd.Parameters.AddWithValue("@IntDepartmentCode", user.INTDEPARTMENTCODE);
                //cmd.Parameters.AddWithValue("@IntDesignationCode", user.INTDESIGNATIONCODE);
                //cmd.Parameters.AddWithValue("@IntFlgStatus", user.INTFLGSTATUS);
                cmd.Parameters.AddWithValue("@Email", user.EMAIL ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MobileNo", user.MOBILENO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Dob", user.DOB ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@EmployeeCode", user.EMPLOYEECODE);
                cmd.Parameters.AddWithValue("@Gender", user.GENDER);
                cmd.Parameters.AddWithValue("@Doj", user.DOJ ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@ModuleId", user.MODULEID ?? (object)DBNull.Value);

                // Check if the user is an admin (special case, no password hashing)
                if (user.ITNUSERNAME.ToLower() == "admin@mail.com")
                {
                    cmd.Parameters.AddWithValue("@IntPassword", user.INTPASSWORD); // No hashing for admin
                }
                else
                {
                    // Hash the password using SHA256 before storing it
                    string hashedPassword = HashPassword(user.INTPASSWORD);
                    cmd.Parameters.AddWithValue("@IntPassword", hashedPassword);
                }

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



