using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.EmployeeRepository
{
    public class EmployeeRepository : Repository<EmployeeModel>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }
    }
}
