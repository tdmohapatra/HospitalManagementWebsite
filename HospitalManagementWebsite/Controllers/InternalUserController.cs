using HospitalManagementWebsite.Models;
using HospitalManagementWebsite.Models.InternalUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class InternalUserController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult InternalLogin()
        {
            // Create an instance of InternalUserModelManager
            InternalUserModelManager modelManager = new InternalUserModelManager();

            // Fetch department data from the database
            List<Department> departments = modelManager.GetDepartments(); // Call the method on the instance

            if (departments != null && departments.Any())
            {
                // Populate ViewBag with departments if available
                // Set "DepartmentID" as Value and "DepartmentName" as Text for the SelectList
                ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
            }
            else
            {
                // Handle case where no departments are found
                ViewBag.Departments = new SelectList(new List<Department>(), "DepartmentID", "DepartmentName");
            }

            return View();
        }





        // POST: Login
        [HttpPost]
        public ActionResult InternalLogin(InternalUser user)
        {
            if (ModelState.IsValid)
            {
                // Create an instance of InternalUserModelManager to interact with the database
                InternalUserModelManager modelManager = new InternalUserModelManager();

                // Retrieve user credentials based on the username
                var userCredentials = modelManager.GetUserCredentialsByUsername(user.ITNUSERNAME);
                if (!string.IsNullOrEmpty(userCredentials.passwordHash) && modelManager.VerifyPassword(user.INTPASSWORD, userCredentials.passwordHash, user.ITNUSERNAME))

                {
                    // Ensure department codes are compared as strings for consistency
                    string departmentCodeFromDb = userCredentials.departmentCode.ToString();  // Convert to string
                    string departmentCodeFromInput = user.INTDEPARTMENTCODE.ToString();      // Ensure input is also string

                    // Check if department codes match
                    if (departmentCodeFromDb == departmentCodeFromInput)
                    {
                        // If the returned password hash is valid, verify the password
                        if (!string.IsNullOrEmpty(userCredentials.passwordHash) &&
                            modelManager.VerifyPassword(user.INTPASSWORD, userCredentials.passwordHash, user.ITNUSERNAME))
                        {
                            // Store user data in session (email or username) or set up authentication cookies as needed
                            Session["UserEmail"] = user.ITNUSERNAME;  // Example of session storage

                            // Optionally, you can store additional session data, such as user roles or department

                            // Redirect to a protected page (like Dashboard or home page)
                            return RedirectToAction("GetAllPatient", "Patients");

                        }
                        else
                        {
                            // Show an error message if the password is invalid
                            ModelState.AddModelError("", "Invalid username or password.");
                        }
                    }
                    else
                    {
                        // Show an error message if department codes don't match
                        ModelState.AddModelError("", "Invalid department code.");
                    }
                }
                else
                {
                    // Show an error message if the user was not found
                    ModelState.AddModelError("", "User not found.");
                }
            }

            // Return the login view with validation errors
            return View(user);
            
        }



        [HttpGet]
        public ViewResult Register()
        {
            // Create an instance of InternalUserModelManager
            InternalUserModelManager modelManager = new InternalUserModelManager();

            // Fetch department data from the database (replace with actual method to get departments)
            var departments = modelManager.GetDepartments();

            // Check if departments are not null and have items
            if (departments != null && departments.Any())
            {
                // Populate ViewBag with department data
                // SelectList expects value field ("DepartmentID") and text field ("DepartmentName")
                ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
            }
            else
            {
                // Handle the case where no departments are available
                ViewBag.Departments = new SelectList(Enumerable.Empty<Department>(), "DepartmentID", "DepartmentName");
            }

            // Return the view
            return View();
        }



        [HttpPost]
        public ActionResult Register(InternalUser user)
        {
            if (ModelState.IsValid)
            {
                // Create an instance of InternalUserModelManager
                InternalUserModelManager modelManager = new InternalUserModelManager();

                // Register the user
                int result = modelManager.RegisterInternalUser(user);  // Call the method on the instance

                if (result > 0)
                {
                    // Successful registration, redirect to login
                    return RedirectToAction("InternalLogin");
                }
                else
                {
                    // Error during registration, add a model error
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                }
            }

            // Return the registration view with validation errors if any
            return View();
        }


    }
}
