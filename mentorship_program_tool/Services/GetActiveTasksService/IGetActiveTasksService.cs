using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetActiveTasksService
{
    namespace mentorship_program_tool.Services
    {
        public interface IGetActiveTasksService
        {
            IEnumerable<GetActiveTasksAPIModel> GetAllActiveTasks();
        }
    }
}
