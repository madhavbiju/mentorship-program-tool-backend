using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllProgramsAPIModel
    {
        public string MentorName { get; set; }
        public string MenteeName { get; set; }
        public string ProgramName { get; set; }
        public DateTime EndDate { get; set; }
        public string ProgramStatus {  get; set; }
    }
}
