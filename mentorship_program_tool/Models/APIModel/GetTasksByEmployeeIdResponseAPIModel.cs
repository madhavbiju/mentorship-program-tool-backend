using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Models.APIModel
{
    public class GetTasksByEmployeeIdResponseAPIModel
    {
        public IEnumerable<GetTasksByEmployeeIdAPIModel> Tasks { get; set; }
        public int TotalCount { get; set; }
    }
}
