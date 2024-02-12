using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services.MenteesOfMentorListService
{
    public class MenteesOfMentorListService : IMenteesOfMentorListService
    {
        private readonly AppDbContext _context;

        public MenteesOfMentorListService(AppDbContext context)
        {
            _context = context;
        }

        public List<MenteesOfMentorListAPIModel> GetAllMenteesById(int ID)
        {
            var menteesList = from p in _context.Programs
                              join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
                              where p.MentorID == ID
                              select new MenteesOfMentorListAPIModel
                              {
                                  EmployeeID = p.MenteeID,
                                  FirstName = mentee.FirstName,
                                  LastName = mentee.LastName,
                                  ProgramID = p.ProgramID,
                              };

            return menteesList.ToList();
        }
    }
}