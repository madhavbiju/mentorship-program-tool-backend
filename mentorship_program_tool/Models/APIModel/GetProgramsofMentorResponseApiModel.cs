using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Models.ApiModel
{
    public class GetProgramsofMentorResponseApiModel
    {
        public IEnumerable<GetProgramsofMentorApiModel> Programs { get; set; }
        public int TotalCount { get; set; }
    }
}
