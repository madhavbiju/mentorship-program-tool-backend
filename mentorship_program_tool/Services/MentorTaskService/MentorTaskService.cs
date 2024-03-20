using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.MentorTaskRepository
{
    public class MentorTaskService : IMentorTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly ISignalNotificationService _signalnotificationService;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _context;

        public MentorTaskService(IUnitOfWork unitOfWork, INotificationService notificationService, IMailService mailService, ISignalNotificationService signalnotificationService, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _signalnotificationService = signalnotificationService;
            _notificationService = notificationService;
            _context = context;
        }

        public async Task CreateTask(MentorTaskAPIModel mentortaskapimodel)
        {
            var request = MapToProgramExtension(mentortaskapimodel);
            await _unitOfWork.mentorTaskRepository.Add(request);
            _unitOfWork.Complete();

            //to find mentee
            var menteeID = _context.Programs
                .Where(program => program.ProgramID == mentortaskapimodel.ProgramID)
                .Select(program => program.MenteeID)
                .FirstOrDefault();

            _notificationService.AddNotification(menteeID, "New Task is Posted", mentortaskapimodel.CreatedBy);

            var menteeEmail = (await _unitOfWork.Employee.GetById(menteeID))?.EmailId;

            // Call SendProgramCreatedEmailAsync method on the mailService instance
            _mailService.SendTaskPostedEmailAsync(menteeEmail, mentortaskapimodel.Title, mentortaskapimodel.EndDate);

            string menteeUser = menteeID.ToString();
            _signalnotificationService.SendTaskPostedNotificationAsync(menteeUser).Wait();

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
                TaskStatus = 1,
                CreatedBy = mentortaskapimodel.CreatedBy
            };
        }


        public async void UpdateStatusOfTask(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel)
        {
            var existingTask = await _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.TaskStatus = 3;

            _unitOfWork.Complete();
        }

        public async void UpdateEndDateOfTask(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel)
        {
            var existingTask = await _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Get the existing end date for comparison
            DateTime existingEndDate = existingTask.EndDate;

            // Update properties based on the provided model
            existingTask.EndDate = taskenddateupdationmodel.EndDate;
            existingTask.ModifiedTime = DateTime.Now;

            _unitOfWork.Complete();

            // Check if the end date has been changed
            if (existingEndDate != taskenddateupdationmodel.EndDate)
            {
                // Get the program associated with the task
                var program = _context.Programs.FirstOrDefault(p => p.ProgramID == existingTask.ProgramID);

                // Create a new notification entry for the end date update
                var notification = new Notifications
                {
                    NotifiedEmployeeID = program.MenteeID, // Assuming MenteeID is the ID of the user who should receive the notification
                    Notification = "Task end date updated notification",
                    CreatedBy = program.MentorID, // Assuming MentorID is the ID of the mentor associated with the program
                    CreatedTime = DateTime.UtcNow
                };

                // Add notification to the database
                _context.Notifications.Add(notification);
                _context.SaveChanges();

                // Trigger notification service to send the notification
                _signalnotificationService.SendTaskDueDateUpdatedNotificationAsync(program.MenteeID.ToString(), existingTask.Title).Wait();
            }
        }

    }
}
