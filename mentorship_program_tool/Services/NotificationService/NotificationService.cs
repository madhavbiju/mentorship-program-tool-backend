using mentorship_program_tool.Data;

namespace mentorship_program_tool.Services.NotificationService
{
    public class NotificationService
    {
        private readonly AppDbContext _context;
        public NotificationService(AppDbContext context)
        {
            _context = context;
        }
        // Store the notification in the database for the mentor
        _context.Notifications.Add(mentorNotification);

        var menteeNotification = new Notifications
        {
            NotifiedEmployeeID = menteeId, // Assign mentee ID
            Notification = "Pair creation notification", // Provide the notification message
            CreatedBy = 4, // Provide the user who triggered the notification if available
            CreatedTime = DateTime.Now // Provide the creation time
        };

        // Store the notification in the database for the mentee
        _dbContext.Notifications.Add(menteeNotification);
    }
}
