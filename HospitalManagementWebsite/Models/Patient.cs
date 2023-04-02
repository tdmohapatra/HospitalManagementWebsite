using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models
{
    public class Patient
    {
        //i have to create property as much the number of coloumn present in Patient table
        //we are using property to catch the data from user and stoe it later in patient or get data from ssms
        public int pid { get; set; }
        //In mvc we have to write the validator in  model brfore the controlors using attributes
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fname cannot be empty")]
        public string fname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lname cannot be empty")]

        public string lname { get; set; }
        //RangeValidator
        [Range(18, 50, ErrorMessage = "Age Must in Between 18 and 50")]
       
        public int age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BG cannot be empty")]
        public string bg { get; set; }


    }
}