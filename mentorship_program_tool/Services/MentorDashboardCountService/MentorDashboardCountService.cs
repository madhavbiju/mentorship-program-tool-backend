using mentorship_program_tool.Data;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MentorDashboardCountService
{
    public class MentorDashboardCountService : IMentorDashboardCountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public MentorDashboardCountService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // Get mentees Count
        public int GetMentorDashboardMenteeCount(int ID)
        {
            int menteesCount = (from p in _context.Programs
                                where p.MentorID == ID && p.ProgramStatus == 1
                                select p.MenteeID).Distinct().Count();

            return menteesCount;
        }
    }
}
