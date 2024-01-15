using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }
        IStatusRepository Status { get; }
        IReportTypeRepository ReportType { get; }

        IEmployeeRepository Employee { get; }

        int Complete();
    }

}
