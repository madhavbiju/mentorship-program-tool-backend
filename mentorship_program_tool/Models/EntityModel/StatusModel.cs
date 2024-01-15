using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace mentorship_program_tool.Models.EntityModel
{
    public class StatusModel
    {
        [Key]
        public int statusid { get; set; }
        public string currentstatus { get; set; }
    }
}
