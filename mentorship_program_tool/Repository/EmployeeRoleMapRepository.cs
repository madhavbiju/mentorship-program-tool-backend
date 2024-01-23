using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository
{
    public class EmployeeRoleMapRepository : Repository<EmployeeRoleMapModel>, IEmployeeRoleMapRepository
    {
        public EmployeeRoleMapRepository(AppDbContext context) : base(context)
        {
        }
    }
}
