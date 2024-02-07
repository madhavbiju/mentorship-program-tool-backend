using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace mentorship_program_tool.Services.GetMenteeDetailsById
{
    public class GetMenteeDetailsByIdService : IGetMenteeDetailsByIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetMenteeDetailsByIdService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public GetMenteeDetailsByIdAPIModel GetDetailsById(int ID)
        {
            var result = from p in _context.Programs
                         join m in _context.Employees on p.MentorID equals m.EmployeeID
                         join e in _context.Employees on p.MenteeID equals e.EmployeeID
                         where e.EmployeeID == ID
                         select new GetMenteeDetailsByIdAPIModel
                         {
                             ProgramID = p.ProgramID,
                             ProgramName = p.ProgramName,
                             MentorFirstName = m.FirstName,
                             MenteeFirstName = e.FirstName,
                             MenteeLastName = e.LastName,
                             StartDate = p.StartDate,
                             EndDate = p.EndDate
                         };

            return result.SingleOrDefault();
        }
    }
}
