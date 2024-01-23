using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.RoleRepository
{
    public class RoleRepository : Repository<RoleModel>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context) { }
    }
}
