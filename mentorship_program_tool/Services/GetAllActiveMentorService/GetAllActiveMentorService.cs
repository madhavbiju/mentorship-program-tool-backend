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
    public class GetAllActiveMentorService : IGetAllActiveMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetAllActiveMentorService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<GetAllActiveMentorAPIModel> GetAllActiveMentors()
        {
            var mentorList = _context.Employees
    .Join(_context.EmployeeRoleMappings, e => e.EmployeeID, erm => erm.EmployeeID, (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
    .Where(x => x.EmployeeRoleMapping.RoleID == 2)// && x.Employee.AccountStatus == "active"
    .Select(x => new GetAllActiveMentorAPIModel
    {
        EmployeeID = x.Employee.EmployeeID,
        FirstName = x.Employee.FirstName,
        LastName = x.Employee.LastName,

    })
                .ToList();

            return mentorList;
        }
    }
}
