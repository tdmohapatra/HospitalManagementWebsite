using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models
{
    public class DoctorRegistrationModelManager
    {
        public int DrRegistration(DoctorRegistration registration)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            string query = string.Format("insert into DoctorRegistration(Name,Email,PassWord,Mob) values('{0}','{1}','{2}','{3}')", registration.Name, registration.Email, registration.Password, registration.Mob);

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int InsertedRow = cmd.ExecuteNonQuery();
            //closing the connection
            connection.Close();

            return InsertedRow;
        }
        public int login(DoctorRegistration registration)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");
            string qry = "select * from DoctorRegistration";
            SqlCommand cmd = new SqlCommand(qry, connection);
            connection.Close();
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            bool x = false;
            while (dr.Read())
            {
                if (registration.Email == dr["Email"].ToString() && registration.Password == dr["PassWord"].ToString())
                {
                    x = true;
                }

            }
            int matched = 0;
            if (x == true)
            {
                matched = 1;
            }
            return matched;

        }
    }
}
