using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services.GetAllMenteesOfMentorService
{
    public class GetAllMenteesOfMentorService : IGetAllMenteesOfMentorService
    {
        private readonly AppDbContext _context;

        public GetAllMenteesOfMentorService(AppDbContext context)
        {
            _context = context;
        }

        public GetAllMenteesOfMentorResponseAPIModel GetAllMenteesById(int ID, int pageNumber, int pageSize)
        {
            var totalCount = _context.Programs
                .Count(p => p.MentorID == ID);

            // Calculate the number of records to skip for pagination
            int skip = (pageNumber - 1) * pageSize;

            var menteesList = (from p in _context.Programs
                               join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
                               where p.MentorID == ID
                               select new GetAllMenteesOfMentorAPIModel
                               {
                                   EmployeeID = p.MenteeID,
                                   FirstName = mentee.FirstName,
                                   LastName = mentee.LastName,
                                   ProgramName = p.ProgramName,
                                   StartDate = p.StartDate,
                                   EndDate = p.EndDate
                               })
                              .Skip(skip)
                              .Take(pageSize)
                              .ToList();

            return new GetAllMenteesOfMentorResponseAPIModel { Mentees = menteesList, TotalCount = totalCount };
        }

    }
}
