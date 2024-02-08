using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.EmployeeRoleRepository
{
    public class EmployeeRoleRepository : Repository<EmployeeRoleMapping>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
