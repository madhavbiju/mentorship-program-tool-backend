using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.GetAllMenteesOfMentorService
{
    public class GetAllMenteesOfMentorService : IGetAllMenteesOfMentorService
    {
        private readonly AppDbContext _context;

        public GetAllMenteesOfMentorService(AppDbContext context)
        {
            _context = context;
        }


        //(check mentrid in pgm table == coming id)
        //then retrieve the corresponding mentee id of that mentor, and look at emp table using that mentee id.
        public List<GetAllMenteesOfMentorAPIModel> GetAllMenteesById(int id)
        {
            var menteesList = from p in _context.Program
                              join mentee in _context.Employee on p.MenteeId equals mentee.employeeid
                              where p.MentorId == id
                              select new GetAllMenteesOfMentorAPIModel
                              {
                                  EmployeeId = p.MenteeId,
                                  FirstName = mentee.firstname,
                                  LastName = mentee.lastname,
                                  ProgramName = p.programname,
                                  Startdate = p.startdate,
                                  Enddate = p.enddate
                              };



            return menteesList.ToList();
        }
    }
}
