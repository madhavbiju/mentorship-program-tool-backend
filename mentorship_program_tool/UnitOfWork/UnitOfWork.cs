﻿using mentorship_program_tool.Data;
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
using mentorship_program_tool.Repository.MentorTaskRepository;
using mentorship_program_tool.Repository.ProgramRepository;
using mentorship_program_tool.Repository.GetActiveTasksRepository;
using mentorship_program_tool.Repository.GetTasksByEmployeeIdRepository;
using mentorship_program_tool.Repository.GetUserDetailsRepository;
using mentorship_program_tool.Repository.EmployeeRoleRepository;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository.MeetingScheduleReposixtory;
using mentorship_program_tool.Repository.MeetingScheduleRepository;

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
            Program = new ProgramRepository(_context);
            MeetingSchedule = new MeetingScheduleRepository(_context);
            mentorRequestRepository = new MentorRequestRepository(_context);
            adminApprovalRequestRepository = new AdminApprovalRequestRepository(_context);
            getAllProgramsRepository = new GetAllProgramsRepository(_context);
            getMenteeDetailsByIdRepository = new GetMenteeDetailsByIdRepository(_context);
            getAllActiveUnpairedMenteesRepository = new GetAllActiveUnpairedMenteesRepository(_context);
            getTasksByProgramIdRepository = new GetTasksByProgramIdRepository(_context);
            getTasksByEmployeeIdRepository = new GetTasksbyEmployeeIdRepository(_context);
            mentorTaskRepository = new MentorTaskRepository(_context);
            menteeTaskSubmissionRepository = new MenteeTaskSubmissionRepository(_context);
            getUserDetailsRepository = new GetUserDetailsRepository(_context);
            employeeRoleRepository = new EmployeeRoleRepository(_context);
            // Initialize other repositories.
        }

        public IRoleRepository Role { get; }
        public IStatusRepository Status { get; }
        public IReportTypeRepository ReportType { get; }
        public IEmployeeRepository Employee { get; }
        public IMeetingScheduleRepository MeetingSchedule { get; }
        public IProgramRepository Program { get; }
        public IMentorRequestRepository mentorRequestRepository { get; }
        public IAdminApprovalRequestRepository adminApprovalRequestRepository { get; }
        public IGetAllProgramsRepository getAllProgramsRepository { get; }
        public IGetMenteeDetailsByIdRepository getMenteeDetailsByIdRepository { get; }
        public IGetAllActiveUnpairedMenteesRepository getAllActiveUnpairedMenteesRepository { get; }
        public IGetAllActiveMentorRepository getAllActiveMentorRepository { get; }
        public IGetTasksByProgramIdRepository getTasksByProgramIdRepository { get; }
        public IGetTasksbyEmployeeIdRepository getTasksByEmployeeIdRepository { get; }

        public IGetUserDetailsRepository getUserDetailsRepository { get; }

        public IMentorTaskRepository mentorTaskRepository { get; }
        public IMenteeTaskSubmissionRepository menteeTaskSubmissionRepository { get; }
        public IEmployeeRoleRepository employeeRoleRepository { get; }

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
