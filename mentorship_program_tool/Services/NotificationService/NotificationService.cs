using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public void AddNotification(int employeeId, string notificationMessage, int createdBy)
        {
            var notification = new Notifications
            {
                NotifiedEmployeeID = employeeId,
                Notification = notificationMessage,
                CreatedBy = createdBy,
                CreatedTime = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}
