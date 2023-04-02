using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Models
{
    public class PatientViewModel
    {
        //step 1

        //i have to create property as much the number of coloumn present in Patient table
        //we are using property to catch the data from user and stoe it later in patient or get data from ssms
        public int Pid { get; set; }
        //In mvc we have to write the validator in  model brfore the controlors using attributes
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fname cannot be empty")]
        public string Fname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lname cannot be empty")]

        public string Lname { get; set; }
        //RangeValidator
        [Range(18, 50, ErrorMessage = "Age Must in Between 18 and 50")]
        public int Age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BG cannot be empty")]
        public string Bg { get; set; }
        //we have to create property for dropdown list which will give data to drop downlist
        public IEnumerable<SelectListItem>bbb
        {
            get
            {
                PatientModelManager modelManager = new PatientModelManager();
                List<BLoodGroup> bgs = modelManager.GetBLoodGroups();

                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (var Item in bgs)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = Item.Bg;
                    selectListItem.Text = Item.Bg;
                    selectListItems.Add(selectListItem);
                }
                return selectListItems;
            }


        }
    }
}