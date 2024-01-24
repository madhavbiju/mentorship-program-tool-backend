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
     .Join(_context.employeerolemapping,
         e => e.employeeid,
         erm => erm.EmployeeId,
         (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
     .Where(x => x.EmployeeRoleMapping.RoleId == 3 && x.Employee.accountstatus == "active")
     .GroupJoin(_context.Program,
         x => x.Employee.employeeid,
         p => p.MenteeId,
         (x, programs) => new { Employee = x.Employee, Programs = programs })
     .Where(x => !x.Programs.Any())
     .Select(x => new GetAllActiveUnpairedMenteesAPIModel
     {
         EmployeeId = x.Employee.employeeid,
         FirstName = x.Employee.firstname,
         LastName = x.Employee.lastname,

         // Add any additional properties specific to GetAllActiveMentorAPIModel
     })
     .ToList();

            return menteeList;
        }

    }
}
