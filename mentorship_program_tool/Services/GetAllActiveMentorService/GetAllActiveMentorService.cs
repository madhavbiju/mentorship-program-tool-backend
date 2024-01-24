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
                var mentorList = _context.Employee
        .Join(_context.employeerolemapping, e => e.employeeid, erm => erm.EmployeeId, (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
        .Where(x => x.EmployeeRoleMapping.RoleId == 2 && x.Employee.accountstatus == "active")
        .Select(x => new GetAllActiveMentorAPIModel
                    {
                        EmployeeId = x.Employee.employeeid,
                        FirstName = x.Employee.firstname,
                        LastName = x.Employee.lastname,

                        // Add any additional properties specific to GetAllActiveMentorAPIModel
                    })
                    .ToList();

                return mentorList;
        }
    }
}
