using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository;

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
        IGetMenteeDetailsByIdRepository getMenteeDetailsByIdRepository { get; }
        IGetAllActiveUnpairedMenteesRepository getAllActiveUnpairedMenteesRepository { get; }

        IGetAllActiveMentorRepository getAllActiveMentorRepository { get; }

        int Complete();
    }

}
