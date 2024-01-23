using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class EmployeeRoleMapModel
    {
        [Key]
        public int employeerolemappingid { get; set; }
        public int employeeid { get; set; }
        public int roleid { get; set; }
        public int createdby { get; set; }
        public DateTime createdtime { get; set; }
        public int? modifiedby { get; set; }
        public DateTime? modifiedtime { get; set; }
    }
}
