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

namespace mentorship_program_tool.Services.MentorTaskRepository
{
    public class MentorTaskService : IMentorTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _context;

        public MentorTaskService(IUnitOfWork unitOfWork, IMailService mailService, INotificationService notificationService, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _notificationService = notificationService;
            _context = context;
        }

        public void CreateTask(MentorTaskAPIModel mentortaskapimodel)
        {
            var request = MapToProgramExtension(mentortaskapimodel);
            _unitOfWork.mentorTaskRepository.Add(request);
            _unitOfWork.Complete();

            //to find mentee
            var menteeID = _context.Programs
                .Where(program => program.ProgramID == mentortaskapimodel.ProgramID)
                .Select(program => program.MenteeID)
                .FirstOrDefault();

            _notificationService.AddNotification(menteeID, "New Task is Posted", mentortaskapimodel.CreatedBy);

            var menteeEmail = _unitOfWork.Employee.GetById(menteeID)?.EmailId;

            // Call SendProgramCreatedEmailAsync method on the mailService instance
            _mailService.SendTaskPostedEmailAsync(menteeEmail, mentortaskapimodel.Title, mentortaskapimodel.EndDate);

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
