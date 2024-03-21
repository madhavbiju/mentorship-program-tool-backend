using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Services.AdminApprovalRequestService
{
    public class AdminApprovalRequestService : IAdminApprovalRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly ISignalNotificationService _notificationService;

        public AdminApprovalRequestService(IUnitOfWork unitOfWork, AppDbContext dbContext, ISignalNotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _notificationService = notificationService;
        }

        // Admin updating request
        public async void UpdateRequest(int id, AdminApprovalAPIModel adminApprovalApiModel)
        {
            var existingRequest = await _unitOfWork.adminApprovalRequestRepository.GetById(id);

            if (existingRequest == null)
            {
                return;
            }

            // Update properties based on admin api model
            existingRequest.RequestStatusID = adminApprovalApiModel.RequestStatusID;
            existingRequest.ModifiedBy = adminApprovalApiModel.ModifiedBy;

            _unitOfWork.Complete();

            var program = _dbContext.Programs.FirstOrDefault(p => p.ProgramID == existingRequest.ProgramID);

            // Check if the request status is approved (RequestStatusID == 3)
            if (adminApprovalApiModel.RequestStatusID == 3)
            {
                // Create a new notification entry for the approval
                var notification = new Notifications
                {
                    NotifiedEmployeeID = program.MentorID, // Assuming CreatedBy is the ID of the user who made the request
                    Notification = "Request approved notification",
                    CreatedBy = program.MenteeID,
                    CreatedTime = DateTime.UtcNow
                };

                // Add notification to the database
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                // Trigger notification service to send the notification
                _notificationService.SendExtensionApprovalNotificationAsync(program.MentorID.ToString()).Wait();
            }

        }
    }
}
