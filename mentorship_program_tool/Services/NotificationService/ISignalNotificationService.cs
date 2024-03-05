namespace mentorship_program_tool.Services.NotificationService
{
    public interface ISignalNotificationService
    {
        Task SendPairCreationNotificationAsync(string mentorUser, string menteeUser);
        Task SendMeetingScheduledNotificationAsync(string menteeUser, DateTime meetingDateTime);
        Task SendExtensionRequestNotificationAsync(string adminUser, string mentorID, string mentorName);
        Task SendExtensionApprovalNotificationAsync(string mentorUser);
        Task SendTaskPostedNotificationAsync(string menteeUser);
        Task SendTaskSubmittedNotificationAsync(string mentorUser, string menteeName);
        Task SendTaskDueDateUpdatedNotificationAsync(string menteeUser, string taskName);
    }
}
