using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Services.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISignalNotificationService _notificationService;
        private readonly AppDbContext _dbContext;

        public ProgramService(IUnitOfWork unitOfWork, ISignalNotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
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

            // Trigger pair creation notification after completing the program creation process
            _notificationService.SendPairCreationNotificationAsync(mentorIdAsString, menteeIdAsString).Wait(); // Wait for the notification to be sent
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

            string mentorUser = programDto.MentorID.ToString();

            _notificationService.SendExtensionApprovalNotificationAsync(mentorUser).Wait();
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
            var query = from p in _dbContext.Programs
                        join m in _dbContext.Employees on p.MentorID equals m.EmployeeID
                        join mm in _dbContext.Employees on p.MenteeID equals mm.EmployeeID
                        join s in _dbContext.Statuses on p.ProgramStatus equals s.StatusID
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