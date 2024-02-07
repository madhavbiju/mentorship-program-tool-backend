using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services
{
    public class GetAllActiveUnpairedMenteesService : IGetAllActiveUnpairedMenteesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetAllActiveUnpairedMenteesService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<GetAllActiveUnpairedMenteesAPIModel> GetAllActiveUnpairedMentees()
        {
            var menteeList = _context.Employees
                .Join(_context.EmployeeRoleMappings,
                    e => e.EmployeeID,
                    erm => erm.EmployeeID,
                    (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
                .Where(x => x.EmployeeRoleMapping.RoleID == 3 && x.Employee.AccountStatus == "active")
                .GroupJoin(_context.Programs,
                    x => x.Employee.EmployeeID,
                    p => p.MenteeID ,
                    (x, programs) => new { Employee = x.Employee, Programs = programs })
                .Where(x => !x.Programs.Any())
                .Select(x => new GetAllActiveUnpairedMenteesAPIModel
                {
                    EmployeeID = x.Employee.EmployeeID,
                    FirstName = x.Employee.FirstName,
                    LastName = x.Employee.LastName,
                })
                .ToList();

            return menteeList;
        }
    }
}
