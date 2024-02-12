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

        public GetAllMenteesOfMentorResponseAPIModel GetAllMenteesById(int ID, int pageNumber, int pageSize, string sortBy)
        {
            //get all programs of that mentor
            var query = _context.Programs
        .Where(p => p.MentorID == ID);

            var totalCount = query.Count();


            //for sorting program name and endDate
            switch (sortBy)
            {
                case "ProgramName":
                    query = query.OrderBy(p => p.ProgramName); // Ascending order by default
                    break;
                case "ProgramName_desc":
                    query = query.OrderByDescending(p => p.ProgramName); // Descending order for ProgramName
                    break;
                case "endDate":
                    query = query.OrderBy(p => p.EndDate); // Ascending order by default
                    break;
                case "endDate_desc":
                    query = query.OrderByDescending(p => p.EndDate); // Descending order for EndDate
                    break;
            }


            // Calculate the number of records to skip for pagination
            int skip = (pageNumber - 1) * pageSize;

            var menteesList = (from p in query
                               join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
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
