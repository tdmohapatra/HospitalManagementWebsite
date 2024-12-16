using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebsite.Models
{
    public class DoctorRegistration
    {

        public int Id { get; set; }
        public String Name { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be empty")]

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }


        //public String Email { get; set; }
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        //[Required]
        //[StringLength(100, MinimumLength = 8)]
        //public string Password { get; set; }
        public String Mob { get; set; }
        public String Department { get; set; }
        public String DOB { get; set; }
        public String Experience { get; set; }

    }
}