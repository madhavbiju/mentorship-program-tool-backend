using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetActiveTasksService
{
    namespace mentorship_program_tool.Services
    {
        public interface IGetTasksByProgramIdService
        {
            IEnumerable<GetTasksByProgramIdAPIModel> GetTasksByProgramId(int id,int status, int page);
        }
    }
}
