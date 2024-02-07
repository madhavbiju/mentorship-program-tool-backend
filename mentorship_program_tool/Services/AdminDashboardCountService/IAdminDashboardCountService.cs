using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.AdminDashboardCountService
{
    public interface IAdminDashboardCountService
    {
        int GetAdminDashboardMenteeCount();
        int GetAdminDashboardMentorCount();
        int GetAdminDashboardProgramCount();
        int GetAdminDashboardTotalCount();

    }
}
