using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Repository.GetUserDetailsRepository
{
    public class GetUserDetailsRepository : Repository<GetUserDetailsAPIModel>, IGetUserDetailsRepository
    {
        public GetUserDetailsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
