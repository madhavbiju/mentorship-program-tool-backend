using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public class MentorRequestService : IMentorRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _dbContext;

        public MentorRequestService(IUnitOfWork unitOfWork, INotificationService notificationService, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _dbContext = dbContext;
        }


        // Mentor adding a request (status ID will be 4 and modified by will be null)
        public void CreateRequest(MentorRequestAPIModel mentorRequestAPIModel)
        {
            var request = MapToProgramExtension(mentorRequestAPIModel);
            _unitOfWork.mentorRequestRepository.Add(request);
            _unitOfWork.Complete();

            if (request != null)
            {
                var adminRoleUsers = _dbContext.EmployeeRoleMappings
                    .Where(rm => rm.RoleID == 1) // Assuming admin role ID is 1
                    .Select(rm => rm.EmployeeID)
                    .ToList();

                // Add notification for each admin user
                foreach (var adminUserId in adminRoleUsers)
                {
                    var notification = new Notifications
                    {
                        NotifiedEmployeeID = adminUserId,
                        Notification = "Extension request notification", // Provide the notification message
                        CreatedBy = 4, // Assuming ModifiedBy holds mentor ID
                        CreatedTime = DateTime.Now // Provide the creation time
                    };

                    _dbContext.Notifications.Add(notification);
                    _notificationService.SendExtensionRequestNotificationAsync(adminUserId.ToString()).Wait(); // Wait for the notification to be sent
                }

                // Save changes to the database
                _dbContext.SaveChanges();
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
        public IEnumerable<ProgramExtension> GetPendingRequests()
        {
            var pendingRequests = _unitOfWork.mentorRequestRepository.GetAll().Where(n => n.RequestStatusID == 4);
            return pendingRequests;
        }
    }
}
