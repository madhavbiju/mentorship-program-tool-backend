using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public class MentorRequestService : IMentorRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        private readonly ISignalNotificationService _signalnotificationService;

        public MentorRequestService(IUnitOfWork unitOfWork, INotificationService notificationService, AppDbContext context, IMailService mailService, ISignalNotificationService signalnotificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _context = context;
            _mailService = mailService;
            _signalnotificationService = signalnotificationService;
        }


        // Mentor adding a request (status ID will be 4 and modified by will be null)
        public void CreateRequest(MentorRequestAPIModel mentorRequestAPIModel)
        {
            var request = MapToProgramExtension(mentorRequestAPIModel);
            _unitOfWork.mentorRequestRepository.Add(request);
            _unitOfWork.Complete();

            //to find the program name
            var programName = _context.Programs
                .Where(program => program.ProgramID == mentorRequestAPIModel.ProgramID)
                .Select(program => program.ProgramName)
                .FirstOrDefault();

            //to find who is submitting the request
            var mentorID = _context.Programs
                .Where(program => program.ProgramID == mentorRequestAPIModel.ProgramID)
                .Select(program => program.MentorID)
                .FirstOrDefault();

            var mentorName = _context.Employees
                .Where(program => program.EmployeeID == mentorID)
                .Select(program => program.FirstName)
                .FirstOrDefault();

            //to get all admins
            var admins = _context.EmployeeRoleMappings
                      .Where(mapping => mapping.RoleID == 1) // Filter by admin role ID
                      .Select(mapping => mapping.EmployeeID)
                      .ToList();

            // Add notification for each admin and updating notification table
            foreach (var adminId in admins)
            {
                _notificationService.AddNotification(adminId, "New Program Extension", mentorID);
                string adminIdAsString = adminId.ToString();
                string mentorIdAsString = mentorID.ToString();
                _signalnotificationService.SendExtensionRequestNotificationAsync(adminIdAsString, mentorIdAsString, mentorName).Wait();
            }

            //send mail
            // Initialize a list to store admins' email IDs
            var adminEmails = new List<string>();

            // Loop through admin IDs to get their email IDs
            foreach (var adminId in admins)
            {
                var admin = _unitOfWork.Employee.GetById(adminId);
                if (admin != null)
                {
                    adminEmails.Add(admin.EmailId);
                }
            }

            foreach (var adminEmail in adminEmails)
            {
                _mailService.SendProgramExtensionRequestEmailAsync(adminEmail, programName);
            }
        }

        private ProgramExtension MapToProgramExtension(MentorRequestAPIModel mentorRequestAPIModel)
        {
            return new ProgramExtension
            {
                ProgramID = mentorRequestAPIModel.ProgramID,
                NewEndDate = mentorRequestAPIModel.NewEndDate,
                Reason = mentorRequestAPIModel.Reason,
                RequestStatusID = 4,
                ModifiedBy = mentorRequestAPIModel.ModifiedBy,
                CreatedTime = DateTime.Now

            };
        }

        // Get all pending requests
        public MentorRequestResponseAPIModel GetPendingRequests(int status, int pageNumber, int pageSize)
        {
            // Filter pending requests by status
            var pendingRequests = _unitOfWork.mentorRequestRepository
                                            .GetAll()
                                            .Where(n => n.RequestStatusID == status);

            // Get total count of pending requests
            int totalCount = pendingRequests.Count();

            // Implement pagination
            var paginatedRequests = pendingRequests
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            // Create result object
            var result = new MentorRequestResponseAPIModel
            {
                Requests = paginatedRequests,
                TotalCount = totalCount
            };

            return result;
        }
    }
}
