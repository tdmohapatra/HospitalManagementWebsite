using System;

namespace HospitalManagementWebsite.Models.MenuModel
{
    public class MenuControl
    {
        public int MenuID { get; set; }
        public string DepartmentCode { get; set; }
        public string DesignationCode { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string PageLink { get; set; }
        public int ImportanceLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Icon { get; set; }
        public string ExtraColumn1 { get; set; }
        public string ExtraColumn2 { get; set; }
    }

}