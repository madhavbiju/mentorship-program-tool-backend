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

        public GetAllProgramsResponseAPIModel GetAllPrograms(int page, int? programStatus, string sortOrder, string search)
        {
            int pageSize = 5;
            int offset = (page - 1) * pageSize;

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
                                  StartDate = program.StartDate,
                                  ProgramStatus = program.ProgramStatus
                              };

            // Apply filtering by ProgramStatus if provided
            if (programStatus.HasValue)
            {
                programList = programList.Where(p => p.ProgramStatus == programStatus.Value);
            }

            // Apply search functionality if search text is provided
            if (!string.IsNullOrEmpty(search))
            {
                programList = programList.Where(p =>
                    p.ProgramName.Contains(search) ||
                    p.MentorFirstName.Contains(search) ||
                    p.MentorLastName.Contains(search) ||
                    p.MenteeFirstName.Contains(search) ||
                    p.MenteeLastName.Contains(search)
                );
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "programName_desc":
                    programList = programList.OrderByDescending(p => p.ProgramName);
                    break;
                case "endDate":
                    programList = programList.OrderBy(p => p.EndDate);
                    break;
                case "endDate_desc":
                    programList = programList.OrderByDescending(p => p.EndDate);
                    break;
                default:
                    programList = programList.OrderBy(p => p.ProgramName);
                    break;
            }

            int totalCount = programList.Count();

            // Apply pagination
            if (page != 0)
            {
                programList = programList.Skip(offset).Take(pageSize);
            }
            return new GetAllProgramsResponseAPIModel
            {
                Programs = programList.ToList(),
                TotalCount = totalCount
            };
        }


        public GetAllProgramsResponseAPIModel GetAllProgramsEndingSoon(int pageNumber, int pageSize)
        {
            DateTime today = DateTime.Today;
            int offset = (pageNumber - 1) * pageSize;

            var programList = (from program in _context.Programs
                               join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                               join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                               where program.EndDate >= today && program.ProgramStatus == 1
                               orderby program.EndDate
                               select new GetAllProgramsAPIModel
                               {
                                   ProgramID = program.ProgramID,
                                   ProgramName = program.ProgramName,
                                   MentorFirstName = mentor.FirstName,
                                   MentorLastName = mentor.LastName,
                                   MenteeFirstName = mentee.FirstName,
                                   MenteeLastName = mentee.LastName,
                                   EndDate = program.EndDate,
                                   StartDate = program.StartDate,
                                   ProgramStatus = program.ProgramStatus
                               }).Skip(offset).Take(pageSize).ToList();

            int totalCount = _context.Programs.Count(p => p.EndDate >= today && p.ProgramStatus == 1);

            return new GetAllProgramsResponseAPIModel { Programs = programList, TotalCount = totalCount };
        }



    }
}
