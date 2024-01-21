using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository
{
    public class MentorRequestRepository : Repository<MentorRequestModel>, IMentorRequestRepository
    {
        public MentorRequestRepository(AppDbContext context) : base(context) { }
    }
}
