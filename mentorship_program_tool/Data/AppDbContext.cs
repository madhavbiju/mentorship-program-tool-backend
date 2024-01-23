using System.Collections.Generic;
using System.Reflection.Emit;
using mentorship_program_tool.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<RoleModel> role { get; set; }


        public DbSet<StatusModel> status { get; set; }
        public DbSet<ReportTypeModel> reporttype { get; set; }


        public DbSet<EmployeeModel> Employee { get; set; }
        public DbSet<RegisterModel> register { get; set; }
        public DbSet<LoginModel> login { get; set; }
        public DbSet<EmployeeRoleMapModel> employeerolemapping { get; set; }
    }
}
