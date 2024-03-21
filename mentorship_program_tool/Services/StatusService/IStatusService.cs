using mentorship_program_tool.Models.EntityModel;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.StatusService
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetStatus();
        Task<Status> GetStatusById(int id);
        Task CreateStatus(Status status);
        void UpdateStatus(int id, Status status);
        void DeleteStatus(int id);
    }
}
