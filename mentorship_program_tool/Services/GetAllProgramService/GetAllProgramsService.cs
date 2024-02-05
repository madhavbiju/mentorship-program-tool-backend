using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services.GetAllProgramService
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
            var programList = from program in _context.Programs
                              join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                              join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                              select new GetAllProgramsAPIModel
                              {
                                  ProgramID = program.ProgramID,
                                  ProgramName = program.ProgramName,
                                  MentorFirstName = mentor.FirstName,
                                  MentorLastName = mentor.LastName,
                                  MenteeFirstName = mentee.FirstName,
                                  MenteeLastName = mentee.LastName,
                                  EndDate = program.EndDate,
                                  ProgramStatus = program.ProgramStatus
                              };

            return programList.ToList();
        }
    }
}
