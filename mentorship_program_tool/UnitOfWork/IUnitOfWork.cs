using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStatusRepository Status { get; }
        IReportTypeRepository ReportType { get; }


        int Complete();
    }

}
