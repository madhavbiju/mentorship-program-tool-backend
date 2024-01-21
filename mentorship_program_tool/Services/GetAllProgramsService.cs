using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Services
{
    public class GetAllProgramsService : IGetAllProgramsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetAllProgramsService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public  IEnumerable<GetAllProgramsAPIModel> GetAllPrograms()
        {
          var  programlist = from p in _context.Program
                         join pp in _context.ProgramPair on p.ProgramPairId equals pp.ProgramPairId
                         join eMentor in _context.Employee on pp.MentorId equals eMentor.EmployeeId
                         join eMentee in _context.Employee on pp.MenteeId equals eMentee.EmployeeId
                         select new
                         {
                             ProgramId = p.ProgramId,
                             ProgramName = p.ProgramName,
                             MentorFirstName = eMentor.FirstName,
                             MentorLastName = eMentor.LastName,
                             MenteeFirstName = eMentee.FirstName,
                             MenteeLastName = eMentee.LastName,
                             EndDate = p.EndDate,
                             ProgramStatus = p.ProgramStatus
                         };

            // Now 'result' contains the data you need



            return (programlist);
        }
    }
}
