using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository
{
    public class AdminApprovalRequestRepository : Repository<MentorRequestModel>, IAdminApprovalRequestRepository
    {
        public AdminApprovalRequestRepository(AppDbContext context) : base(context) { }
    }
}
