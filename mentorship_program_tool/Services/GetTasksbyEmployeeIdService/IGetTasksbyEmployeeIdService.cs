using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Services.GetTasksbyEmployeeIdService
{
    public interface IGetTasksbyEmployeeIdService
    {
        IEnumerable<GetTasksByEmployeeIdAPIModel> GetTasksByEmployeeId(int id, int status);
    }
}
