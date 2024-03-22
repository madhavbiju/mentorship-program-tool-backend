using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace mentorship_program_tool.Services.GetAllMenteesListService
{
    public class GetAllMenteesListService : IGetAllMenteesListService
    {
        private readonly AppDbContext _context;
        public GetAllMenteesListService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GetAllMenteesListAPIModel> GetAllMenteesList()
        {
            var query = _context.Programs;
            var menteesList = (from p in query
                               join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
                               select new GetAllMenteesListAPIModel
                               {
                                   EmployeeID = p.MenteeID,
                                   ProgramID = p.ProgramID,
                                   FirstName = mentee.FirstName,
                                   LastName = mentee.LastName,
                                 
                               });
            return menteesList;
        }

    }
}



 

       

       