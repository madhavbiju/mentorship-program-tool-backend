using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.AdminApprovalRequestRepository
{
    public class AdminApprovalRequestRepository : Repository<ProgramExtension>, IAdminApprovalRequestRepository
    {
        public AdminApprovalRequestRepository(AppDbContext context) : base(context) { }
    }
}
