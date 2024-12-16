using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Models
{
    public class PatientViewModel
    {
        //step 1

        //i have to create property as much the number of coloumn present in Patient table
        //we are using property to catch the data from user and stoe it later in patient or get data from ssms

        public string genderId { get; set; }

        public IEnumerable<SelectListItem> bloodGroup { get; set; }
        public IEnumerable<SelectListItem> getgenderlist { get; set; }




        public int Pid { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name must be at most 20 characters.")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name must be at most 20 characters.")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Patient age is required.")]
        [Range(1, 50, ErrorMessage = "Age must be between 1 and 50.")]
        public int Age { get; set; }


        [Required(AllowEmptyStrings = true, ErrorMessage = "BG cannot be empty")]
        public string Bg { get; set; }


        public String email { get; set; }
        public String phoneNo { get; set; }
        public String Country { get; set; }
        [Required(ErrorMessage = "Patient Country is required.")]

        public String State { get; set; }
        public String City { get; set; }
        public String Zipcode { get; set; }
        public String diseaseId { get; set; }
        public String gender { get; set; }
        public String Attachment { get; set; }

        //added by td mohapatra--2024-02-02
        //we have to create property for dropdown list which will give data to drop downlist


    }
}