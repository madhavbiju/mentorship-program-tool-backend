﻿using System.Collections.Generic;
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
        public DbSet<MentorRequestModel> programextension { get; set; }
        public DbSet<EmployeeRoleMappingModel> employeerolemapping { get; set; }
        public DbSet<ProgramModel> Program { get; set; }

        public DbSet<TaskModel> task { get; set; }
    }
}
