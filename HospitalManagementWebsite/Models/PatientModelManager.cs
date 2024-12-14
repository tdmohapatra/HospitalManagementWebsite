using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models
{
    public class PatientModelManager
    {
        //ADDED BY TD MOHAPATRA 05-01-24
        string strcon =   ConfigurationManager.ConnectionStrings["CON"].ConnectionString;


        //we need 4 methods :GetSllPatient(),CreatePatient(),UpdatePatient(),DeletePatient()
        public List<Patient> GetPatients()
        {
            //return type is list as we will get all the patient details in list format
            //we are going to return all patient data from DB
            //Ado.net code 1-SqlConnection Object

            //ADDED BY TD MOHAPATRA--05-01-24
            SqlConnection connection = new SqlConnection(strcon);

          //  SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");

            //step-2 create Sql coāæmmand object
            SqlCommand cmd = new SqlCommand("select * from Patient", connection);
            //step-3 open connection
            connection.Open();
            //step-4 read the Connection and catch the result in  data reader
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            //to read each record we will use while loop
            //before that we will create a list to store all the data which comes from DB
            List<Patient> patients = new List<Patient>();

            while (sqlDataReader.Read())
            {
                //now the list is empty to fill data we need to create object of patient
                Patient patient = new Patient();
                //read data from each column of patient table in DB
                //Do convertion accroding to data type
                patient.pid = Convert.ToInt32(sqlDataReader["pid"]);
                patient.fname = sqlDataReader["fname"].ToString();
                patient.lname = sqlDataReader["lname"].ToString();
                patient.age = Convert.ToInt32(sqlDataReader["age"]);
                patient.bg = Convert.ToString(sqlDataReader["bg"]);

                //add data into list
                patients.Add(patient);


            }
            connection.Close();
            //retrun data types
            return patients;
        }
        //insert Method
        //public int CreatePatient(Patient patient)
        //{
        //    int tdm_genderId = 0;
        //    //ADDED BY TD MOHAPATRA--05-01-24
        //    SqlConnection connection = new SqlConnection(strcon);

        //    //coomented by td mohapatra-2024-02-02
        //   // string query = string.Format("insert into patient(fname,lname,age,bg) values('{0}','{1}','{2}','{3}')", patient.fname, patient.lname, patient.age, patient.bg);
        //   if(Convert.ToInt16(patient.genderId) >=0)
        //    {
        //         tdm_genderId = Convert.ToInt16(patient.gender);
        //    }
        //    string query = string.Format("insert into patient(fname,lname,age,bg,genderId,email,phoneNo,Country,State,City,Zipcode) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},{10})", patient.fname, patient.lname, patient.age, patient.bg, tdm_genderId, patient.email,patient.phoneNo,patient.Country, patient.State, patient.City,patient.Zipcode);


        //    SqlCommand cmdd = new SqlCommand(query, connection);

        //    connection.Open();
        //    //catching no of rows affected
        //    int InsertedRow = cmdd.ExecuteNonQuery();
        //    //closing the connection
        //    connection.Close();

        //    return InsertedRow;
        //}



        public int CreatePatient(Patient patient)
        {
            int tdm_genderId = 0;
            SqlConnection connection = new SqlConnection(strcon);

            if (Convert.ToInt16(patient.genderId) >= 0)
            {
                tdm_genderId = Convert.ToInt16(patient.gender);
            }

            string query = "INSERT INTO patient (fname, lname, age, bg, genderId, email, phoneNo, Country, State, City, Zipcode) " +
                           "VALUES (@fname, @lname, @age, @bg, @genderId, @email, @phoneNo, @Country, @State, @City, @Zipcode)";

            SqlCommand cmdd = new SqlCommand(query, connection);

            // Add parameters to the command
            cmdd.Parameters.AddWithValue("@fname", patient.fname);
            cmdd.Parameters.AddWithValue("@lname", patient.lname);
            cmdd.Parameters.AddWithValue("@age", patient.age);
            cmdd.Parameters.AddWithValue("@bg", patient.bg);
            cmdd.Parameters.AddWithValue("@genderId", tdm_genderId);
            cmdd.Parameters.AddWithValue("@email", patient.email);
            cmdd.Parameters.AddWithValue("@phoneNo", patient.phoneNo);
            cmdd.Parameters.AddWithValue("@Country", patient.Country);
            cmdd.Parameters.AddWithValue("@State", patient.State);
            cmdd.Parameters.AddWithValue("@City", patient.City);
            cmdd.Parameters.AddWithValue("@Zipcode", patient.Zipcode);

            connection.Open();
            int InsertedRow = cmdd.ExecuteNonQuery();
            connection.Close();

            return InsertedRow;
        }

        public Patient GetPatientById(int id)
        {
            //return type is list as we will get all the patient details in list format
            //we are going to return all patient data from DB
            //Ado.net code 1-SqlConnection Object
            //  SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi;");

            //step-2 create Sql coāæmmand object
            SqlCommand cmd = new SqlCommand($"select * from Patient where pid={id}", connection);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "GETPATIENTDATABYID";
            //step-3 open connection
            connection.Open();
            //step-4 read the Connection and catch the result in  data reader
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            //now the list is empty to fill data we need to create object of patient
            Patient patient = new Patient();

            if (sqlDataReader.Read())
            {

                //read data from each column of patient table in DB
                //Do convertion accroding to data type
                patient.pid = Convert.ToInt32(sqlDataReader["pid"]);
                patient.fname = sqlDataReader["fname"].ToString();
                patient.lname = sqlDataReader["lname"].ToString();
                patient.age = Convert.ToInt32(sqlDataReader["age"]);
                patient.bg = Convert.ToString(sqlDataReader["bg"]);
                //added on 2024-02-02
                patient.genderId = Convert.ToString(sqlDataReader["genderId"]);
                patient.email = Convert.ToString(sqlDataReader["email"]);
                patient.Country = Convert.ToString(sqlDataReader["Country"]);
                patient.State = Convert.ToString(sqlDataReader["State"]);
                patient.City = Convert.ToString(sqlDataReader["City"]);
                patient.Zipcode = Convert.ToString(sqlDataReader["Zipcode"]);



            }
            connection.Close();
            //retrun data types
            return patient;
            
        }
        //Update patient Details By ID
        public int UpdatePatient(Patient patient)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            //string query = string.Format("update Patient set fname='{0}',lname='{1}',age='{2}',bg='{3}', where pid={4}", patient.fname, patient.lname, patient.age, patient.bg, patient.pid);

            string query = string.Format("update Patient set fname='{0}',lname='{1}',age='{2}',bg='{3}',genderId='{4}',email='{5}',phoneNo='{6}' where pid={7}", patient.fname, patient.lname, patient.age, patient.bg, patient.genderId, patient.email, patient.phoneNo, patient.pid);


            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int UpdatedRow = cmd.ExecuteNonQuery();


            return UpdatedRow;

        }
        //Delete Operation O
        public int DeletePatient(int pid)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            string query = string.Format("update Patient set flg='N' where pid={0}", pid);

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int DeletedRows = cmd.ExecuteNonQuery();
            connection.Close();


            return DeletedRows;

        }
        public List<BLoodGroup> GetBLoodGroups()
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB;initial catalog=TDM;integrated security=sspi");
            String Qry = "Select * from Bloodgroup";
            SqlCommand cmd = new SqlCommand(Qry,connection);
            connection.Open();
            //list is required to get all data from bloodgroup table
          

           SqlDataReader sqlDataReader= cmd.ExecuteReader();
              List<BLoodGroup> bLoodGroups= new List<BLoodGroup>();


            // Add default option
            //bLoodGroups.Add(new BLoodGroup
            //{
            //    Id = 0, // Assuming 0 as the default ID or set it to an appropriate default value
            //    Bg = "---SELECT BLOOD GROUP--"
            //});
            while (sqlDataReader.Read())
            {
                BLoodGroup bLoodGroup= new BLoodGroup();
                bLoodGroup.Id = Convert.ToInt32(sqlDataReader["ID"]);
                bLoodGroup.Bg = sqlDataReader["Bg"].ToString();
                bLoodGroups.Add(bLoodGroup);
            }
            return bLoodGroups;
        }
        //added by td mohapatra --2024-02-02
        public List<Gender>GetGenders()
        {
            SqlConnection connection = new SqlConnection(strcon);
            string qry = "select *  from gender ";
            SqlCommand cmd = new SqlCommand(qry, connection);
            connection.Open();
            SqlDataReader sqlDataReader= cmd.ExecuteReader();
            List<Gender> Lgenders = new List<Gender>();
            while (sqlDataReader.Read())
            {
                Gender GENDER = new Gender();
                GENDER.gId = Convert.ToInt32(sqlDataReader["gId"]);
                GENDER.genderId = sqlDataReader["gender"].ToString();
                GENDER.gDesc = sqlDataReader["gDesc"].ToString();

                Lgenders.Add(GENDER);
            }
            return Lgenders;


        }

        //create method using procedure call
        public List<Patient> GETPATIENT()
        
        {
            SqlConnection con = new SqlConnection(strcon);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GETALLPATIENTDATA";
            //add any parameters the stored procedure might require
            con.Open();
            //object getPatient = cmd.ExecuteScalar();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            //create alist object tto store all data comming from table
            List<Patient> patients = new List<Patient>();
            //Patient patient = new Patient();



            while (sqlDataReader.Read())
            {
                Patient patient = new Patient();
                //read data from each column of patient table in DB
                //Do convertion accroding to data type
                patient.pid = Convert.ToInt32(sqlDataReader["pid"]);
                patient.fname = sqlDataReader["fname"].ToString();
                patient.lname = sqlDataReader["lname"].ToString();
                patient.age = Convert.ToInt32(sqlDataReader["age"]);
                patient.bg = Convert.ToString(sqlDataReader["bg"]);
                patient.gender = Convert.ToString(sqlDataReader["gender"]);
                patient.phoneNo = Convert.ToString(sqlDataReader["phoneNo"]);
                patient.email = Convert.ToString(sqlDataReader["email"]);

                //add data into list
                patients.Add(patient);


            }
            con.Close();
            //retrun data types
            return patients;

        }
    }
}