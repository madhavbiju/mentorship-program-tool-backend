using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Services.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _dbContext;

        public ProgramService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public IEnumerable<Models.EntityModel.Program> GetProgram()
        {

            var program = _unitOfWork.Program.GetAll();
            return program;
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


    }
}