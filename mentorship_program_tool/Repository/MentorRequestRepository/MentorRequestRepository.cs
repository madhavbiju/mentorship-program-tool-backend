using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.MentorRequestRepository
{
    public class MentorRequestRepository : Repository<ProgramExtension>, IMentorRequestRepository
    {
        public MentorRequestRepository(AppDbContext context) : base(context) { }
    }
}
