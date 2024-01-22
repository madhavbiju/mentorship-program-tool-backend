using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllProgramsAPIModel
    {
        public int ProgramId { get; set; }
        public string MentorFirstName { get; set; }

        public string MentorLastName { get; set; }
        public string MenteeFirstName { get; set; }
        public string MenteeLastName { get; set; }
        public string ProgramName { get; set; }
        public DateTime EndDate { get; set; }
        public string ProgramStatus {  get; set; }
    }
}
