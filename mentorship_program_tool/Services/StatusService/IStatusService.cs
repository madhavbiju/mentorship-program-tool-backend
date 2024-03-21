using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.StatusService
{
    public interface IStatusService
    {
        IEnumerable<Status> GetStatus();
        Status GetStatusById(int id);
        void CreateStatus(Status status);
        void UpdateStatus(int id, Status status);
        void DeleteStatus(int id);
    }
}
