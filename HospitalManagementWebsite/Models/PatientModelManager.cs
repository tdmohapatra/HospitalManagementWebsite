using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models
{
    public class PatientModelManager
    {
        //we need 4 methods :GetSllPatient(),CreatePatient(),UpdatePatient(),DeletePatient()
        public List<Patient> GetPatients()
        {
            //return type is list as we will get all the patient details in list format
            //we are going to return all patient data from DB
            //Ado.net code 1-SqlConnection Object
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");

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
        public int CreatePatient(Patient patient)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            string query = string.Format("insert into patient(pid,fname,lname,age,bg) values('{0}','{1}','{2}','{3}','{4}')", patient.pid, patient.fname, patient.lname, patient.age, patient.bg);

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int InsertedRow = cmd.ExecuteNonQuery();
            //closing the connection
            connection.Close();

            return InsertedRow;
        }
        public Patient GetPatientById(int id)
        {
            //return type is list as we will get all the patient details in list format
            //we are going to return all patient data from DB
            //Ado.net code 1-SqlConnection Object
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");

            //step-2 create Sql coāæmmand object
            SqlCommand cmd = new SqlCommand($"select * from Patient where pid={id}", connection);
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
            }
            connection.Close();
            //retrun data types
            return patient;

        }
        //Update patient Details By ID
        public int UpdatePatient(Patient patient)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            string query = string.Format("update Patient set fname='{0}',lname='{1}',age='{2}',bg='{3}' where pid={4}", patient.fname, patient.lname, patient.age, patient.bg, patient.pid);

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int UpdatedRow = cmd.ExecuteNonQuery();


            return UpdatedRow;

        }
        //Delete Operation O
        public int DeletePatient(int pid)
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");
            //we have to write sql quiery and save to a string 

            string query = string.Format("Delete Patient where pid={0}", pid);

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            //catching no of rows affected
            int DeletedRows = cmd.ExecuteNonQuery();
            connection.Close();


            return DeletedRows;

        }
        public List<BLoodGroup> GetBLoodGroups()
        {
            SqlConnection connection = new SqlConnection(@"data source=SHAHEB\MSSQLSERVER01;initial catalog=Palle;integrated security=sspi");
            String Qry = "Select * from Bloodgroup";
            SqlCommand cmd = new SqlCommand(Qry,connection);
            connection.Open();
            //list is required to get all data from bloodgroup table
          

           SqlDataReader sqlDataReader= cmd.ExecuteReader();
              List<BLoodGroup> bLoodGroups= new List<BLoodGroup>();
            while (sqlDataReader.Read())
            {
                BLoodGroup bLoodGroup= new BLoodGroup();
                bLoodGroup.Id = Convert.ToInt32(sqlDataReader["ID"]);
                bLoodGroup.Bg = sqlDataReader["Bg"].ToString();
                bLoodGroups.Add(bLoodGroup);
            }
            return bLoodGroups;
        }
    }
}