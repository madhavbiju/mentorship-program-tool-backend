using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorRequestResponseAPIModel
    {
        public IEnumerable<ProgramExtension> Requests { get; set; }
        public int TotalCount { get; set; }
    }
}
