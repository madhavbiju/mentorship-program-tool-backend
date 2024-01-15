using System.Collections.Generic;
using System.Reflection.Emit;
using mentorship_program_tool.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmployeeModel> Employees { get; set; }


       
        
    }
}
