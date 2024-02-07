using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class EmployeeRoleMapping
    {
        [Key]
        public int EmployeeRoleMappingID { get; set; }

        public int EmployeeID { get; set; }

        public int RoleID { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

    }
}
