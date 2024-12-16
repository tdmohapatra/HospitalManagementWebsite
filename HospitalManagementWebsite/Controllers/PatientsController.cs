using HospitalManagementWebsite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientModelManager modelManager = new PatientModelManager();

        // GET: Patients
        public ViewResult GetallPatient()
        {
            // Fetch the list of patients
            List<Patient> patients = modelManager.GETPATIENT();
            return View(patients);
        }

        // Insert Operations
        [HttpGet]
        public ViewResult CreatePatient()
        {
            var patientViewModel = new PatientViewModel
            {
                bloodGroup = modelManager.GetBLoodGroups()?.Select(bg => new SelectListItem
                {
                    Text = bg.Bg,
                    Value = bg.Bg
                }).ToList(),

                getgenderlist = modelManager.GetGenders()?.Select(g => new SelectListItem
                {
                    Text = g.genderId,
                    Value = g.gId.ToString()
                }).ToList()
            };

            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult CreatePatient(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient
                {
                    fname = patientViewModel.Fname,
                    lname = patientViewModel.Lname,
                    age = patientViewModel.Age,
                    bg = patientViewModel.Bg,
                    gender = patientViewModel.genderId,
                    email = patientViewModel.email,
                    phoneNo = patientViewModel.phoneNo,
                    Country = patientViewModel.Country,
                    State = patientViewModel.State,
                    City = patientViewModel.City,
                    Zipcode = patientViewModel.Zipcode

                };

                int insertedRows = modelManager.CreatePatient(patient);
                if (insertedRows > 0)
                {
                    return RedirectToAction("GetallPatient");
                }
                ModelState.AddModelError("", "An error occurred while saving the patient.");
            }

            // Reload dropdown data in case of validation failure
            patientViewModel.bloodGroup = modelManager.GetBLoodGroups()?.Select(bg => new SelectListItem
            {
                Text = bg.Bg,
                Value = bg.Bg
            }).ToList();

            patientViewModel.getgenderlist = modelManager.GetGenders()?.Select(g => new SelectListItem
            {
                Text = g.genderId,
                Value = g.gId.ToString()
            }).ToList();

            return View(patientViewModel);
        }

        [HttpGet]
        public ActionResult UpdatePatient(int id)
        {
            var patient = modelManager.GetPatientById(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            var patientViewModel = new PatientViewModel
            {
                Pid = patient.pid,
                Fname = patient.fname,
                Lname = patient.lname,
                Age = patient.age,
                Bg = patient.bg,
                genderId = patient.gender,
                email = patient.email,
                phoneNo = patient.phoneNo,
                Country = patient.Country,
                State = patient.State,
                City = patient.City,

                bloodGroup = modelManager.GetBLoodGroups()?.Select(bg => new SelectListItem
                {
                    Text = bg.Bg,
                    Value = bg.Bg
                }).ToList(),
                getgenderlist = modelManager.GetGenders()?.Select(g => new SelectListItem
                {
                    Text = g.genderId,
                    Value = g.gId.ToString()
                }).ToList()
            };

            return View(patientViewModel);
        }


        [HttpPost]
        public ActionResult UpdatePatient(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient
                {
                    pid = patientViewModel.Pid,
                    fname = patientViewModel.Fname,
                    lname = patientViewModel.Lname,
                    age = patientViewModel.Age,
                    bg = patientViewModel.Bg,
                    gender = patientViewModel.genderId,
                    email = patientViewModel.email,
                    phoneNo = patientViewModel.phoneNo
                };

                int updatedRows = modelManager.UpdatePatient(patient);
                if (updatedRows > 0)
                {
                    return RedirectToAction("GetallPatient");
                }

                ModelState.AddModelError("", "An error occurred while updating the patient.");
            }

            // Reload dropdown data in case of validation failure
            patientViewModel.bloodGroup = modelManager.GetBLoodGroups()?.Select(bg => new SelectListItem
            {
                Text = bg.Bg,
                Value = bg.Bg
            }).ToList();

            patientViewModel.getgenderlist = modelManager.GetGenders()?.Select(g => new SelectListItem
            {
                Text = g.genderId,
                Value = g.gId.ToString()
            }).ToList();

            return View(patientViewModel);
        }

        public ActionResult DeletePatient(int id)
        {
            int deletedRows = modelManager.DeletePatient(id);
            if (deletedRows > 0)
            {
                return RedirectToAction("GetallPatient");
            }

            ModelState.AddModelError("", "An error occurred while deleting the patient.");
            return RedirectToAction("GetallPatient");
        }

        public ActionResult OpenCamera()
        {
            return View();
        }
    }
}
