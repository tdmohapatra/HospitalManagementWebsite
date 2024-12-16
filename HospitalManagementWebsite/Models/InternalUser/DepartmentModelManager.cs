using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models.InternalUser
{
    public class DepartmentModelManager
    {
        string strcon = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                // SQL query to get DepartmentID, DepartmentName, and DepartmentCode
                string query = "SELECT DepartmentID, DepartmentName, DepartmentCode FROM Department WHERE ActiveFlag = 1";

                SqlCommand cmd = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentID = (int)reader["DepartmentID"],
                            DepartmentName = reader["DepartmentName"].ToString(),
                            DepartmentCode = reader["DepartmentCode"].ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return departments;
        }
    }
}