using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.GetActiveTasksService
{
    namespace mentorship_program_tool.Services
    {
        public interface IGetTasksByProgramIdService
        {
            GetTasksByProgramIdResponseAPIModel GetTasksByProgramId(int ID, int status, int page, string? sortBy);
            Task<Models.EntityModel.Task> GetTaskById(int id);
        }
    }
}
