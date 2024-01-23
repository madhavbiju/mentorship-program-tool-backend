using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Sprache;
using System;

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
            var menteeList = _context.Employee
    .Join(_context.employeerolemapping, e => e.employeeid, erm => erm.EmployeeId, (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
    .Join(_context.role, x => x.EmployeeRoleMapping.RoleId, r => r.roleid, (x, r) => new { x.Employee, x.EmployeeRoleMapping, Role = r })
    .GroupJoin(_context.programpair, x => x.Employee.employeeid, pp => pp.MenteeId, (x, programPairs) => new { x.Employee, x.EmployeeRoleMapping, x.Role, ProgramPairs = programPairs.DefaultIfEmpty() })
    .Where(x => x.Role.roles == "Mentee" && x.Employee.accountstatus == "active" && x.ProgramPairs.All(pp => pp == null))
    .Select(x => new GetAllActiveUnpairedMenteesAPIModel
    {
        EmployeeId =  x.Employee.employeeid,
        FirstName = x.Employee.firstname,
        LastName = x.Employee.lastname,
       
    })
    .ToList();

            return menteeList;
        }

    }
}
