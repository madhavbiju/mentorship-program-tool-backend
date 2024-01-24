using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.MenteeTaskSubmissionRepository;
using mentorship_program_tool.Repository.MentorTaskRepository;

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
        IMentorTaskRepository mentorTaskRepository { get; }
        IMenteeTaskSubmissionRepository menteeTaskSubmissionRepository { get; }
        int Complete();
    }

}
