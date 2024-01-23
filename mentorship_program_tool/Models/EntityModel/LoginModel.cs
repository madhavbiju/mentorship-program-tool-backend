using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class LoginModel
    {
        [Key]
        public int loginid { get; set; }
        public int employeeid { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
