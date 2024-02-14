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
        [Range(18, 90, ErrorMessage = "Age Must in Between 18 and 90")]
       
        public int age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BG cannot be empty")]
        public string bg { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
         public string gender { get; set; }

        //added by td mohapatra---2024-02-02
        public string genderId { get; set; }


        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; }

        //[Required(ErrorMessage = "Phone number is required.")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string phoneNo { get; set; }
        //public String flg { get; set; }
        public String country { get; set; }
        public String state { get; set; }
        public String disease { get; set; }
        //public String gender { get; set; }

        //public String flg { get; set; }

        //public String disease { get; set; }


    }
}