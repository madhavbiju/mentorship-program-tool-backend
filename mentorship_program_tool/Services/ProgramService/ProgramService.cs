using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
namespace mentorship_program_tool.Services.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly ISignalNotificationService _notificationService;
        private readonly AppDbContext _context;

        public ProgramService(IUnitOfWork unitOfWork, IMailService mailService, AppDbContext context, ISignalNotificationService notificationService)
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

            string mentorIdAsString = programDto.MentorID.ToString();
            string menteeIdAsString = programDto.MenteeID.ToString();

            //send mail
            var mentorEmail = _unitOfWork.Employee.GetById(programDto.MentorID)?.EmailId;
            var menteeEmail = _unitOfWork.Employee.GetById(programDto.MenteeID)?.EmailId;

            // Call SendProgramCreatedEmailAsync method on the mailService instance
            _mailService.SendProgramCreatedEmailAsync(mentorEmail, menteeEmail, programDto.ProgramName);
            // Trigger pair creation notification after completing the program creation process
            _notificationService.SendPairCreationNotificationAsync(mentorIdAsString, menteeIdAsString).Wait(); // Wait for the notification to be sent
        }

        public void UpdateProgram(int id, ProgramAPIModel programDto)
        {
            var existingProgram = _unitOfWork.Program.GetById(id);

            if (existingProgram == null)
            {
                // Handle not found scenario
                return;
            }
            if (programDto.MentorID.HasValue)
            {
                existingProgram.MentorID = programDto.MentorID.Value;
            }
            existingProgram.ModifiedBy = programDto.ModifiedBy;
            existingProgram.ModifiedTime = programDto.ModifiedTime;

                existingProgram.EndDate = programDto.EndDate;
            
            existingProgram.ProgramName = programDto.ProgramName;
           
                existingProgram.StartDate = programDto.StartDate;
            

            _unitOfWork.Complete();

            /* var mentorEmail = _unitOfWork.Employee.GetById(programDto.MentorID)?.EmailId;
             _mailService.SendExtensionApprovalEmailAsync(mentorEmail, programDto.ProgramName, programDto.EndDate);
             string mentorUser = programDto.MentorID.ToString();

             _notificationService.SendExtensionApprovalNotificationAsync(mentorUser).Wait();*/
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


        public GetProgramsofMentorResponseApiModel GetProgramsofMentor(int id, int pageNumber, int pageSize)
        {
            int offset = (pageSize - 1) * pageSize;

            var programList = from program in _context.Programs
                              join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                              join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                              where mentor.EmployeeID == id
                              select new GetProgramsofMentorApiModel
                              {
                                  ProgramID = program.ProgramID,
                                  ProgramName = program.ProgramName,
                                  MentorFirstName = mentor.FirstName,
                                  MentorLastName = mentor.LastName,
                                  MenteeFirstName = mentee.FirstName,
                                  MenteeLastName = mentee.LastName,
                                  EndDate = program.EndDate,
                                  StartDate = program.StartDate,
                                  ProgramStatus = program.ProgramStatus
                              };



            // Apply sorting

            programList = programList.OrderByDescending(p => p.ProgramName);

            int totalCount = programList.Count();

            // Apply pagination
            /* if (pageSize != 0)
             {
                 programList = programList.Skip(offset).Take(pageSize);
             }*/
            return new GetProgramsofMentorResponseApiModel
            {
                Programs = programList.ToList(),
                TotalCount = totalCount
            };
        }


    }
}