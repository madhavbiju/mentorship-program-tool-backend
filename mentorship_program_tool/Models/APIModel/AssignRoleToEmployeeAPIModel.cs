using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class AssignRolesToEmployeeModel
    {
        public int EmployeeID { get; set; }
        public bool IsMentor { get; set; }
        public bool IsMentee { get; set; }
    }
}
