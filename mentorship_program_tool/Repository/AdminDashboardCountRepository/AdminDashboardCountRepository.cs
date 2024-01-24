using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.AdminDashboardCountRepository
{
    public class AdminDashboardCountRepository : Repository<AdminDashboardCountAPIModel>, IAdminDashboardCountRepository
    {
        public AdminDashboardCountRepository(AppDbContext context) : base(context) { }
    }
}


