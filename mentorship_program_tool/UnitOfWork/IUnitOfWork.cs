using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.AdminApprovalRequestRepository;
using mentorship_program_tool.Repository.EmployeeRepository;
using mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository;
using mentorship_program_tool.Repository.GetAllProgramsRepository;
using mentorship_program_tool.Repository.MentorRequestRepository;
using mentorship_program_tool.Repository.ReportTypeRepository;
using mentorship_program_tool.Repository.RoleRepository;
using mentorship_program_tool.Repository.StatusRepository;
using mentorship_program_tool.Repository.MenteeTaskSubmissionRepository;

using mentorship_program_tool.Repository.GetTasksByEmployeeIdRepository;
using mentorship_program_tool.Repository.MentorTaskRepository;
using mentorship_program_tool.Repository.ProgramRepository;
using mentorship_program_tool.Repository.GetActiveTasksRepository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }
        IStatusRepository Status { get; }
        IReportTypeRepository ReportType { get; }
        IEmployeeRepository Employee { get; }
        IProgramRepository Program { get; }
        IMentorRequestRepository mentorRequestRepository { get; }
        IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }
        IGetAllProgramsRepository getAllProgramsRepository { get; }
        IGetMenteeDetailsByIdRepository getMenteeDetailsByIdRepository { get; }
        IGetAllActiveUnpairedMenteesRepository getAllActiveUnpairedMenteesRepository { get; }
        IGetAllActiveMentorRepository getAllActiveMentorRepository { get; }
        IGetTasksByProgramIdRepository getTasksByProgramIdRepository { get; }
        IGetTasksbyEmployeeIdRepository getTasksByEmployeeIdRepository { get; }


        IMentorTaskRepository mentorTaskRepository { get; }
        IMenteeTaskSubmissionRepository menteeTaskSubmissionRepository { get; }
        int Complete();
    }

}
