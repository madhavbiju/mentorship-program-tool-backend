using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository;

namespace mentorship_program_tool.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Status = new StatusRepository(_context);
            Role = new RoleRepository(_context);
            ReportType = new ReportTypeRepository(_context);

            Employee = new EmployeeRepository(_context);

            mentorRequestRepository = new MentorRequestRepository(_context);

            adminApprovalRequestRepository = new AdminApprovalRequestRepository(_context);

            getAllProgramsRepository = new GetAllProgramsRepository(_context);

            getMenteeDetailsByIdRepository = new GetMenteeDetailsByIdRepository(_context);

            getAllActiveUnpairedMenteesRepository = new GetAllActiveUnpairedMenteesRepository(_context);

            // Initialize other repositories.
        }

        public IRoleRepository Role { get; }

        public IStatusRepository Status { get; }
        public IReportTypeRepository ReportType { get; }

        public IEmployeeRepository Employee { get; }
        public IMentorRequestRepository mentorRequestRepository { get; }
        public IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }
        public IGetAllProgramsRepository getAllProgramsRepository { get; }

        public IGetMenteeDetailsByIdRepository getMenteeDetailsByIdRepository { get; }
        public IGetAllActiveUnpairedMenteesRepository getAllActiveUnpairedMenteesRepository { get; }

        public IGetAllActiveMentorRepository getAllActiveMentorRepository { get; }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
