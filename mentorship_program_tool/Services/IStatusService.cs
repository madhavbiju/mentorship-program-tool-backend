using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services
{
    public interface IStatusService
    {
        IEnumerable<StatusModel> GetStatus();
        StatusModel GetStatusById(int id);
        void CreateStatus(StatusModel status);
        void UpdateStatus(int id, StatusModel status);
        void DeleteStatus(int id);
    }
}
