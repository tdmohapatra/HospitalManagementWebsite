using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementWebsite.Models.InternalUser
{
    public class Department
    {
        public int DepartmentID { get; set; }                  // ID of the department
        public string DepartmentName { get; set; }              // Name of the department
        public string DepartmentCode { get; set; }              // Code of the department
        public string ParentDepartmentCode { get; set; }        // Parent department code
        public bool ActiveFlag { get; set; }                     // Flag to indicate if the department is active
        public int HierarchyLevel { get; set; }                  // Hierarchy level in the department structure
        public string DepartmentHierarchyName { get; set; }     // Full department hierarchy name

      
        public string PositionName { get; set; }
        public string PositionCode { get; set; }
        public string JobDescription { get; set; }
        public bool PositionActiveFlag { get; set; }
    }
}