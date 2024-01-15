using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class EmployeeModel
    {
        [Key]
        public int employeeid { get; set; }
        public string outlookemployeeid { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string emailid { get; set; }

        public DateTime createddate { get; set; }
        
    }
}
