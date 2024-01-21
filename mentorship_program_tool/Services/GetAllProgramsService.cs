using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Sprache;

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
        public IEnumerable<GetAllProgramsAPIModel> GetAllPrograms()
        {
            var programList= from program in _context.Program
                                           join pair in _context.programpair on program.programpairid equals pair.ProgramPairId
                                           join mentor in _context.Employee on pair.MentorId equals mentor.employeeid
                                            join mentee in _context.Employee on pair.MenteeId equals mentee.employeeid
                             select new GetAllProgramsAPIModel
                             {
                                              ProgramId=  program.programid,
                                 ProgramName =program.programname,
                                               MentorFirstName = mentor.firstname,
                                               MentorLastName = mentor.lastname,
                                               MenteeFirstName = mentee.firstname,
                                               MenteeLastName = mentee.lastname,
                                 EndDate= program.enddate,
                                               ProgramStatus = program.programstatus
                             };


            return programList;
        }

    }
}
