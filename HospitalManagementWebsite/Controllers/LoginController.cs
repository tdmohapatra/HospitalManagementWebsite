using HospitalManagementWebsite.Models;
using System;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();  // Ensure this View exists under Views/Login/Login.cshtml
        }


        // POST: Login
        [HttpPost]
        public ActionResult Login(DoctorRegistration registration)
        {
            if (ModelState.IsValid)
            {
                DoctorRegistrationModelManager modelManager = new DoctorRegistrationModelManager();
                try
                {
                    // Check user credentials using a method that retrieves the stored password hash
                    var storedHash = modelManager.GetPasswordHashByEmail(registration.Email);

                    // If a valid user is found and the password hashes match
                    if (!string.IsNullOrEmpty(storedHash) && BCrypt.Net.BCrypt.Verify(registration.Password, storedHash))
                    {
                        // Store user data in session or set up authentication cookies as needed
                        Session["UserEmail"] = registration.Email; // Example of session storage, can be changed as per your requirements

                        // Redirect to a protected page (like patient details)
                        return RedirectToAction("GetAllPatient", "Patients");
                    }
                    else
                    {
                        // Show an error message if credentials are invalid
                        ModelState.AddModelError("", "Invalid email or password.");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Login", "Login");
                }

               
            }

            return View(registration); // Return the login view with validation errors
        }

        // GET: DrRegister (Doctor Registration View)
        [HttpGet]
        public ViewResult DrRegister()
        {
            DoctorRegistration doctor = new DoctorRegistration();
            return View(doctor); // Return the registration view
        }

        // POST: DrRegister (Doctor Registration Logic)
        [HttpPost]
        public ActionResult DrRegister(DoctorRegistration registration)
        {
            if (ModelState.IsValid)
            {
                DoctorRegistrationModelManager modelManager = new DoctorRegistrationModelManager();

                // Hash the password before saving it to the database
                registration.Password = BCrypt.Net.BCrypt.HashPassword(registration.Password);

                int insertedRow = modelManager.DrRegistration(registration); // Register the doctor

                if (insertedRow > 0)
                {
                    // Redirect to login page after successful registration
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                }
            }

            return View(); // Return the registration view if validation fails
        }
    }
}
