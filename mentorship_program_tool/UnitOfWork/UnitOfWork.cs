using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;

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
            Register = new RegisterRepository(_context);
            Login = new LoginRepository(_context);
            EmployeeRoleMap = new EmployeeRoleMapRepository(_context);

            mentorRequestRepository = new MentorRequestRepository(_context);

            adminApprovalRequestRepository = new AdminApprovalRequestRepository(_context);

            getAllProgramsRepository = new GetAllProgramsRepository(_context);

            // Initialize other repositories.
        }

        public IRoleRepository Role { get; }

        public IStatusRepository Status { get; }
        public IReportTypeRepository ReportType { get; }

        public IEmployeeRepository Employee { get; }
        public IMentorRequestRepository mentorRequestRepository { get; }
        public IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }
        public IGetAllProgramsRepository getAllProgramsRepository { get; }

        public IRegisterRepository Register { get; }
        public ILoginRepository Login { get; }
        public IEmployeeRoleMapRepository EmployeeRoleMap { get; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
