using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace mentorship_program_tool.Services.GetMenteeDetailsById
{
    public class GetMenteeDetailsByIdService: IGetMenteeDetailsByIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetMenteeDetailsByIdService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public GetMenteeDetailsByIdAPIModel GetDetailsById(int id)
        {
            var result = from p in _context.Program
                         join m in _context.Employee on p.mentorid equals m.employeeid
                         join e in _context.Employee on p.menteeid equals e.employeeid
                         where e.employeeid == id
                         select new GetMenteeDetailsByIdAPIModel
                         {
                             programid = p.programid,
                             ProgramName = p.programname,
                             MentorFirstName = m.firstname,
                             MenteeFirstName = e.firstname,
                             MenteeLastName = e.lastname,
                             startdate = p.startdate,
                             EndDate = p.enddate
                         };

            return result.SingleOrDefault();
        }
    }
}
