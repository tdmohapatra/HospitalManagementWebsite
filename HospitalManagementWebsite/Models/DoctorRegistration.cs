using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models
{
    public class DoctorRegistration
    {

        public int Id { get; set; }
        public String Name { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be empty")]
        public String Email { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public String Password { get; set; }
        public String Mob { get; set; }
        public String Department { get; set; }
        public String DOB { get; set; }
        public String Experience { get; set; }

    }
}