using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.MenteeTaskSubmissionRepository;
using mentorship_program_tool.Repository.MentorTaskRepository;

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

            mentorTaskRepository = new MentorTaskRepository(_context);

            menteeTaskSubmissionRepository = new MenteeTaskSubmissionRepository(_context);

            // Initialize other repositories.
        }

        public IRoleRepository Role { get; }

        public IStatusRepository Status { get; }
        public IReportTypeRepository ReportType { get; }

        public IEmployeeRepository Employee { get; }
        public IMentorRequestRepository mentorRequestRepository { get; }
        public IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }
        public IMentorTaskRepository mentorTaskRepository { get; }
        public IMenteeTaskSubmissionRepository menteeTaskSubmissionRepository { get; }

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
