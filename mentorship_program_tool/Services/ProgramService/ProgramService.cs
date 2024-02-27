using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace mentorship_program_tool.Services.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        private readonly INotificationService _notificationService;

        public ProgramService(IUnitOfWork unitOfWork, AppDbContext context, IMailService mailService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mailService = mailService;
            _notificationService = notificationService;
        }

        public ProgramDetailsResponseAPIModel GetProgram(int status, int pageNumber, int pageSize)
        {
            var programs = _unitOfWork.Program
                             .GetAll()
                             .Where(p => p.ProgramStatus == status);

            // Get total count of programs
            int totalCount = programs.Count();

            // Implement pagination
            var paginatedPrograms = programs
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();

            // Create result object
            var result = new ProgramDetailsResponseAPIModel
            {
                Programs = paginatedPrograms,
                TotalCount = totalCount
            };

            return result;
        }

       


        public Models.EntityModel.Program GetProgramById(int id)
        {
            var program = _unitOfWork.Program.GetById(id);
            return program;
        }

        public void CreateProgram(Models.EntityModel.Program programDto)
        {

            _unitOfWork.Program.Add(programDto);
            _unitOfWork.Complete();

            //for updating notification table
            _notificationService.AddNotification(programDto.MentorID, "New Pair created", programDto.CreatedBy);
            _notificationService.AddNotification(programDto.MenteeID, "New Pair created", programDto.CreatedBy);


            //send mail
            var mentorEmail = _unitOfWork.Employee.GetById(programDto.MentorID)?.EmailId;
            var menteeEmail = _unitOfWork.Employee.GetById(programDto.MenteeID)?.EmailId;

            // Call SendProgramCreatedEmailAsync method on the mailService instance
            _mailService.SendProgramCreatedEmailAsync(mentorEmail, menteeEmail, programDto.ProgramName);
        }

        public void UpdateProgram(int id, Models.EntityModel.Program programDto)
        {
            var existingProgram = _unitOfWork.Program.GetById(id);

            if (existingProgram == null)
            {
                // Handle not found scenario
                return;
            }

            existingProgram.MentorID = programDto.MentorID;
            existingProgram.ModifiedBy = programDto.ModifiedBy;
            existingProgram.ModifiedTime = programDto.ModifiedTime;
            existingProgram.EndDate = programDto.EndDate;


            _unitOfWork.Complete();


            _notificationService.AddNotification(programDto.MentorID, "Program Extension Request approved", (int)programDto.ModifiedBy);

            var mentorEmail = _unitOfWork.Employee.GetById(programDto.MentorID)?.EmailId;
            _mailService.SendExtensionApprovalEmailAsync(mentorEmail, programDto.ProgramName, programDto.EndDate);
        }

        public void DeleteProgram(int id)
        {
            var program = _unitOfWork.Program.GetById(id);

            if (program == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.Program.Delete(program);
            _unitOfWork.Complete();
        }
        public GetPairByProgramIdAPIModel GetPairDetailsById(int id)
        {
            var query = from p in _context.Programs
                        join m in _context.Employees on p.MentorID equals m.EmployeeID
                        join mm in _context.Employees on p.MenteeID equals mm.EmployeeID
                        join s in _context.Statuses on p.ProgramStatus equals s.StatusID
                        where p.ProgramID == id
                        select new GetPairByProgramIdAPIModel
                        {
                            MentorName = m.FirstName,
                            MenteeName = mm.FirstName,
                            ProgramName = p.ProgramName,
                            ProgramStatus = s.StatusValue,
                            EndDate = p.EndDate
                        };




            return query.FirstOrDefault();
        }


    }
}