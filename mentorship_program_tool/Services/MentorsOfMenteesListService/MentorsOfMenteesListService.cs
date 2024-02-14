using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace mentorship_program_tool.Services.MentorsOfMenteesListService
    {
        public class MentorsOfMenteesListService : IMentorsOfMenteesListService
    {
            private readonly AppDbContext _context;

            public MentorsOfMenteesListService(AppDbContext context)
            {
                _context = context;
            }

        public List<MentorsOfMenteesListAPImodel> GetAllMentorsById(int ID)
        {
            var mentorsList = from p in _context.Programs
                              join mentor in _context.Employees on p.MentorID equals mentor.EmployeeID
                              where p.MenteeID == ID
                              select new MentorsOfMenteesListAPImodel
                              {
                                  EmployeeID = p.MenteeID,
                                  FirstName = mentor.FirstName,
                                  LastName = mentor.LastName,
                                  ProgramID = p.ProgramID,
                                  EmailID = mentor.EmailId,
                              };

            return mentorsList.ToList();
        }
    }
    }
