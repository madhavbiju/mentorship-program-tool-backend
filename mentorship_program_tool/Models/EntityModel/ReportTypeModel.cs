using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class ReportTypeModel
    {
        [Key]
        public int reporttypeid { get; set; }
        public string reporttype { get; set; }
    }
}
