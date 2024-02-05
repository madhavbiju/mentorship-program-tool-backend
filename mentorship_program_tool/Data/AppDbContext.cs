using mentorship_program_tool.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProgramExtension> ProgramExtensions { get; set; }
        public DbSet<EmployeeRoleMapping> EmployeeRoleMappings { get; set; }
        public DbSet<Models.EntityModel.Program> Programs { get; set; }
        public DbSet<Models.EntityModel.Task> Tasks { get; set; }
    }
}
