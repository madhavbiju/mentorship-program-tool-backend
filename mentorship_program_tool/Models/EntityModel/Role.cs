using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleName { get; set; }
    }
}
