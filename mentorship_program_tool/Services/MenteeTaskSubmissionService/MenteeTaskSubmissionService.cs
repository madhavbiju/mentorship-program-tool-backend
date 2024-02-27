using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Services.MenteeTaskSubmissionService
{
    public class MenteeTaskSubmissionService : IMenteeTaskSubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISignalNotificationService _notificationService;
        private readonly AppDbContext _dbContext;

        public MenteeTaskSubmissionService(IUnitOfWork unitOfWork, ISignalNotificationService notificationService, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _dbContext = dbContext;
        }

        public void SubmitTask(int ID, MenteeTaskSubmissionAPIModel menteeTaskSubmissionAPIModel)
        {
            try
            {
                var existingTask = _unitOfWork.menteeTaskSubmissionRepository.GetById(ID);

                if (existingTask == null)
                {
                    return;
                }

                // Update properties based on adminapi model
                existingTask.TaskID = ID;
                existingTask.FilePath = menteeTaskSubmissionAPIModel.filepath;
                existingTask.SubmissionTime = DateTime.Now;
                existingTask.TaskStatus = 6;


                _unitOfWork.Complete();

                // Retrieve program ID associated with the task
                int programId = existingTask.ProgramID;

                // Retrieve mentor ID using the program ID
                var program = _dbContext.Programs.FirstOrDefault(p => p.ProgramID == programId);
                if (program == null)
                {
                    // Handle case where program is not found
                    return;
                }

                var mentorUserId = program.MentorID;
                var menteeUserId = program.MenteeID;

                var notification = new Notifications
                {
                    NotifiedEmployeeID = mentorUserId,
                    Notification = "Task submitted notification",
                    CreatedBy = menteeUserId,
                    CreatedTime = DateTime.UtcNow
                };

                // Add notification to the database
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                // Trigger notification service to send the notification
                _notificationService.SendTaskSubmittedNotificationAsync(mentorUserId.ToString()).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
