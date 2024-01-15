﻿using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Status = new StatusRepository(_context);

            Employee = new EmployeeRepository(_context);


            // Initialize other repositories.
        }


        public IStatusRepository Status { get; }
        public IEmployeeRepository Employee { get; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
