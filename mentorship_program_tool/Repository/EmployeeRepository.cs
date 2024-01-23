using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Repository
{
    public class EmployeeRepository : Repository<EmployeeModel>, IEmployeeRepository
    {
        
        public EmployeeRepository(AppDbContext context) : base(context)
        {
           
        }
        

    }
}
