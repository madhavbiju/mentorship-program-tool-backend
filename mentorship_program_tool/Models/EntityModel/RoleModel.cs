using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class RoleModel
    {
        [Key]
        public int roleid { get; set; }
        public string roles { get; set; }
    }
}
