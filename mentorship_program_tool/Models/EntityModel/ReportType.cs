using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class ReportType
    {
        [Key]
        public int ReportTypeID { get; set; }

        public string ReportName { get; set; }
    }
}
