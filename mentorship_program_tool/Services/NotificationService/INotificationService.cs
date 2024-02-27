namespace mentorship_program_tool.Services.NotificationService
{
    public interface INotificationService
    {
        void AddNotification(int employeeId, string notificationMessage, int createdBy);
    }
}
