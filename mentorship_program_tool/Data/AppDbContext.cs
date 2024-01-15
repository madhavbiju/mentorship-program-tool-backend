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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Your model configurations here...

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Cochin" },
                new City { CityId = 2, CityName = "Trivandrum" }
                // Add more cities as needed...
            );

            modelBuilder.Entity<Designation>().HasData(
                new Designation { DesignationId = 1, DesignationName = "Manager" },
                new Designation { DesignationId = 2, DesignationName = "Developer" }
                // Add more designations as needed...
            );

            modelBuilder.Entity<Education>().HasData(
                new Education { EducationId = 1, EducationLevel = "Bachelor's Degree" },
                new Education { EducationId = 2, EducationLevel = "Master's Degree" }
                // Add more education levels as needed...
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" }
                // Add more departments as needed...
            );
        }
    }
}
