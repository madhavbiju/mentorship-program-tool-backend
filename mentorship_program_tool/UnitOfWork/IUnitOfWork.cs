using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }
        IStatusRepository Status { get; }
        IReportTypeRepository ReportType { get; }

        IEmployeeRepository Employee { get; }
        IRegisterRepository Register { get; }
        ILoginRepository Login { get; }
        IEmployeeRoleMapRepository EmployeeRoleMap { get; }

        int Complete();
        Task<int> SaveChangesAsync();
    }

}
