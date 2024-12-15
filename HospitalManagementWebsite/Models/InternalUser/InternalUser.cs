using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models.InternalUser
{
    public class InternalUser
    {
        public int INTUID { get; set; }
        public string ITNUSERNAME { get; set; }
        public string INTPASSWORD { get; set; }
        public int INTDEPARTMENTCODE { get; set; }
        public int INTDESIGNATIONCODE { get; set; }
        public string INTFLGSTATUS { get; set; }
        public DateTime CREATEDON { get; set; }
        public string EMAIL { get; set; }
        public string MOBILENO { get; set; }
        public DateTime? DOB { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string GENDER { get; set; }
        public DateTime? DOJ { get; set; }
        public string MODULEID { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
    }

}