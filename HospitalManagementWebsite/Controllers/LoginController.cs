using HospitalManagementWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login(DoctorRegistration registration)
        {
            //we have to get paticular patient dat as we dont have we have to create a modelmanager for that
            //create object of the model manager of which return the patient data
            if (ModelState.IsValid)
            {
                DoctorRegistrationModelManager modelmanager = new DoctorRegistrationModelManager();

                int matchedrecord = modelmanager.login(registration);
                if (matchedrecord > 0)
                {
                    return RedirectToAction("GetallPatient", "Patients");
                }
                else
                {
                    Response.Output.WriteLine("INVALID DETAILS");
                }
            }


            return View(registration);
        }
        [HttpGet]
        public ViewResult DrRegister()
        {
            DoctorRegistration Doctor = new DoctorRegistration();
            //return empty model which will sended to view 
            //if we dont pass this empty view to View error will occurs
            return View(Doctor);
        }
        [HttpPost]
        public ActionResult DrRegister(DoctorRegistration registration)
        {
            //we have to give if condition if validation is valid then only these code get executed
            if (ModelState.IsValid)
            {
                DoctorRegistrationModelManager pm = new DoctorRegistrationModelManager();
                int InsertedRow = pm.DrRegistration(registration);
                if (InsertedRow > 0)
                {
                    return RedirectToAction("Login");
                }
            }

            return View();
        }
    }
}
