using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetActiveTasksService
{
    namespace mentorship_program_tool.Services
    {
        public interface IGetTasksByProgramIdService
        {
            GetTasksByProgramIdResponseAPIModel GetTasksByProgramId(int ID, int status, int page, string? sortBy);

        }
    }
}
