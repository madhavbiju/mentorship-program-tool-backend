using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetTasksbyEmployeeIdService
{
    public interface IGetTasksbyEmployeeIdService
    {
        GetTasksByEmployeeIdResponseAPIModel GetTasksByEmployeeId(int ID, int status, int page);
            }
}
