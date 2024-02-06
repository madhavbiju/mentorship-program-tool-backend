namespace mentorship_program_tool.Services.NotificationService
{
    public interface INotificationService
    {
        Task SendPairCreationNotificationAsync(string mentorUser, string menteeUser);
        /*Task SendMeetingScheduledNotificationAsync(string mentorUser, string menteeUser, DateTime meetingDateTime);*/
        Task SendExtensionRequestNotificationAsync(string adminUser);
        Task SendExtensionApprovalNotificationAsync(string mentorUser);
        Task SendTaskPostedNotificationAsync(string menteeUser);
        Task SendTaskSubmittedNotificationAsync(string mentorUser);
        Task SendTaskDueDateUpdatedNotificationAsync(string menteeUser);
    }
}
