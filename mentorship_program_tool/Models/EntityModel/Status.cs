using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        public string StatusValue { get; set; }
    }
}
