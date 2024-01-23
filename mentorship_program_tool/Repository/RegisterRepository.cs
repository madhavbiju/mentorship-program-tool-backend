using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository
{
    public class RegisterRepository : Repository<RegisterModel>, IRegisterRepository
    {
        public RegisterRepository(AppDbContext context) : base(context) { }
    }
}
