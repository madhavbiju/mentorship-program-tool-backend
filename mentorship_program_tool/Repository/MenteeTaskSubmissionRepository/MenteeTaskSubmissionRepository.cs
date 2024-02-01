using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository.MentorTaskRepository;

namespace mentorship_program_tool.Repository.MenteeTaskSubmissionRepository
{
    public class MenteeTaskSubmissionRepository : Repository<Models.EntityModel.Task>, IMenteeTaskSubmissionRepository
    {
        public MenteeTaskSubmissionRepository(AppDbContext context) : base(context) { }
    }
}
