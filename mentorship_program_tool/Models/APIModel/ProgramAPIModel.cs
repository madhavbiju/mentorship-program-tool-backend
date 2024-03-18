using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class ProgramAPIModel
    {
        [Key]
        public int ProgramID { get; set; }

        public int? MentorID { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedTime { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? ProgramName { get; set; }
    }
}
