using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace mentorship_program_tool.Models.APIModel
{
    public class EmployeeRoleMappingAPI
    {
        [Key]
        public int employeerolemappingid { get; set; }
        public int employeeid { get; set; }
        public int roleid { get; set; }
        public int createdby { get; set; }


    }
}
