using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.UnitOfWork;
using System.Linq;

namespace mentorship_program_tool.Services.GetPairByProgramIdService
{
    public class GetPairByProgramIdService : IGetPairByProgramIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetPairByProgramIdService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public GetPairByProgramIdAPIModel GetPairDetailsById(int id)
        {
            var program = _context.Programs
                .Where(p => p.ProgramID == id)
                .Join(_context.Employees,
                    p => p.MentorID,
                    mentor => mentor.EmployeeID,
                    (p, mentor) => new { Program = p, Mentor = mentor })
                .Join(_context.Employees,
                    p => p.Program.MenteeID,
                    mentee => mentee.EmployeeID,
                    (p, mentee) => new { p.Program, p.Mentor, Mentee = mentee })
                .Join(_context.Statuses,
                    p => p.Program.ProgramStatus,
                    status => status.StatusID,
                    (p, status) => new GetPairByProgramIdAPIModel
                    {
                        ProgramName = p.Program.ProgramName,
                        MentorName =p.Mentor.FirstName,
                        MenteeName = p.Mentee.FirstName,
                        ProgramStatus = status.StatusValue,
                        StartDate = p.Program.StartDate,
                        EndDate = p.Program.EndDate
                    })
                .FirstOrDefault();

            return program;
        }
    }
}
