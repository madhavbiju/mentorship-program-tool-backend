using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }
        IStatusRepository Status { get; }
        IReportTypeRepository ReportType { get; }

        IEmployeeRepository Employee { get; }
        IMentorRequestRepository mentorRequestRepository { get; }
        IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }

        IGetAllProgramsRepository getAllProgramsRepository { get; }


        int Complete();
    }

}
