using HospitalManagementWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class PatientsController : Controller
    {
        // GET: Patients
        public ViewResult GetallPatient()
        {
            //step1
            PatientModelManager modelManager = new PatientModelManager();
            List<Patient> patients = modelManager.GetPatients();
            //getpatient data is returning list of patient Data

            return View(patients);
        }
        //Insert Operations
        [HttpGet]
        public ViewResult CreatePatient()
        {
            PatientViewModel patient = new PatientViewModel();
            //return empty model which will sended to view 
            //if we dont pass this empty view to View error will occurs
            return View(patient);
        }
        [HttpPost]//attributes
        public ActionResult CreatePatient(Patient Patient)
        {
            //we have to give if condition if validation is valid then only these code get executed
            if (ModelState.IsValid)
            {
                PatientModelManager modelManager = new PatientModelManager();

                int InsertedRow = modelManager.CreatePatient(Patient);

                if (InsertedRow > 0)
                {
                    return RedirectToAction("GetallPatient");
                }
            }

            return View();
        }
        //This action method will dispaly A Patient details By id inside the Text box in the view page
        [HttpGet]
        public ActionResult Updatepatient(int id)
        {
            //we have to get paticular patient dat as we dont have we have to create a modelmanager for that
            //create object of the model manager of which return the patient data
            PatientModelManager modelmanager = new PatientModelManager();
            Patient patient = modelmanager.GetPatientById(id);

            return View(patient);
        }
        [HttpPost]//during Postback request
        public ActionResult Updatepatient(Patient patient)
        {
            //we have to get paticular patient dat as we dont have we have to create a modelmanager for that
            //create object of the model manager of which return the patient data
            if (ModelState.IsValid)
            {
                PatientModelManager modelmanager = new PatientModelManager();
                int UpdatedRows = modelmanager.UpdatePatient(patient);
                if (UpdatedRows > 0)
                {
                    return RedirectToAction("GetallPatient");
                }
            }
            return View(patient);
        }
        public ActionResult DeletePatient(int id)
        {
            PatientModelManager modelManager = new PatientModelManager();
            int DeletedRows = modelManager.DeletePatient(id);
            if (DeletedRows > 0)
            {
                return RedirectToAction("GetallPatient");
            }
            return View();
        }
    }
}