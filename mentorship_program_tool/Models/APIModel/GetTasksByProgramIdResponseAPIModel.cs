using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Models.APIModel
{
    public class GetTasksByProgramIdResponseAPIModel
    {
        public IEnumerable<GetTasksByProgramIdAPIModel> Tasks { get; set; }
        public int TotalCount { get; set; }
    }
}
