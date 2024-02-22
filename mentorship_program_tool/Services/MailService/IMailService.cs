namespace mentorship_program_tool.Services.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendProgramCreatedEmailAsync(string mentorEmail, string menteeEmail, string ProgramName);
        Task SendProgramExtensionRequestEmailAsync(string adminEmail, string programName);
        Task SendMeetingScheduledEmailAsync(string menteeEmail, string programName, DateTime scheduleDate);
        Task SendExtensionApprovalEmailAsync(string mentorEmail, string programName, DateTime endDate);
        Task SendTaskPostedEmailAsync(string menteeEmail, string title, DateTime endDate);
    }
}
