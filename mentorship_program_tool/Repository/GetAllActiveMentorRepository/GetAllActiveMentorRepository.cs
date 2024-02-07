using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Repository
{
    public class GetAllActiveMentorRepository : Repository<GetAllActiveMentorAPIModel>, IGetAllActiveMentorRepository
    {
        public GetAllActiveMentorRepository(AppDbContext context) : base(context) { }
    }
}
