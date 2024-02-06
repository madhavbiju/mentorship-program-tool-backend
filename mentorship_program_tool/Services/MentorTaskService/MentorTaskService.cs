using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Threading.Tasks;

namespace mentorship_program_tool.Services.MentorTaskRepository
{
    public class MentorTaskService : IMentorTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly INotificationService _notificationService;

        public MentorTaskService(IUnitOfWork unitOfWork, AppDbContext dbContext, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _notificationService = notificationService;
        }

        public void CreateTask(MentorTaskAPIModel mentortaskapimodel)
        {
            var request = MapToProgramExtension(mentortaskapimodel);
            _unitOfWork.mentorTaskRepository.Add(request);
            _unitOfWork.Complete();

            // Retrieve corresponding program's mentee id
            var program = _dbContext.Programs.FirstOrDefault(p => p.ProgramID == mentortaskapimodel.ProgramID);
            if (program != null)
            {
                var menteeId = program.MenteeID;

                var taskNotification = new Notifications
                {
                    NotifiedEmployeeID = menteeId, // Assign mentee ID
                    Notification = "New task posted notification", // Provide the notification message
                    CreatedBy = mentortaskapimodel.CreatedBy, // Provide the user who triggered the notification if available
                    CreatedTime = DateTime.Now // Provide the creation time
                };

                // Store the notification in the database for the mentee
                _dbContext.Notifications.Add(taskNotification);
                _dbContext.SaveChanges(); // Save changes to the database

                // Send notification
                _notificationService.SendTaskPostedNotificationAsync(menteeId.ToString()).Wait();
            }
        }


        private Models.EntityModel.Task MapToProgramExtension(MentorTaskAPIModel mentortaskapimodel)
        {
            return new Models.EntityModel.Task
            {
                ProgramID = mentortaskapimodel.ProgramID,
                Title = mentortaskapimodel.Title,
                TaskDescription = mentortaskapimodel.TaskDescription,
                StartDate = mentortaskapimodel.StartDate,
                EndDate = mentortaskapimodel.EndDate,
                ReferenceMaterialFilePath = mentortaskapimodel.ReferenceMaterialFilePath,
                FilePath = null,
                SubmissionTime = null,
                TaskStatus = 4,
                CreatedBy = mentortaskapimodel.CreatedBy
            };
        }


        public void UpdateStatusOfTask(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel)
        {
            var existingTask = _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.TaskStatus = 3;

            _unitOfWork.Complete();
        }

        public void UpdateEndDateOfTask(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel)
        {
            var existingTask = _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.ModifiedTime = taskenddateupdationmodel.ModifiedTime;

            _unitOfWork.Complete();

        }
    }
}
