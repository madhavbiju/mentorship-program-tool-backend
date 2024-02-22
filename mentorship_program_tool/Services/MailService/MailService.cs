using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace mentorship_program_tool.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        public MailService()
        {
            _smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mentorshipteam@outlook.com", "jsyjcdkjefbgjzog"),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(toEmail);
            message.From = new MailAddress("mentorshipteam@outlook.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                await _smtpClient.SendMailAsync(message);
                Console.WriteLine("mail sent succesfuly");
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
            finally
            {
                message.Dispose();
            }
        }

        public async Task SendProgramCreatedEmailAsync(string mentorEmail, string menteeEmail, string ProgramName)
        {
            // Compose email content
            var mentorSubject = "New Program Created";
            var mentorBody = $"Dear Mentor,\n\nA new program \" {ProgramName}\" has been created. Please check your mentor dashboard for details.";

            var menteeSubject = "New Program Assigned";
            var menteeBody = $"Dear Mentee,\n\nYou have been assigned to a new program \"{ProgramName}\". Please check your mentee dashboard for details.";

            // Send emails asynchronously
            await SendEmailAsync(mentorEmail, mentorSubject, mentorBody);
            await SendEmailAsync(menteeEmail, menteeSubject, menteeBody);
        }
        public async Task SendProgramExtensionRequestEmailAsync(string adminEmail, string programName)
        {
            // Compose email content
            var mailSubject = "New Program Extension Request";
            var mailBody = $"Dear Admin,\n\nA program extension request has created for the program \" {programName} \". Please check your admin dashboard for details.";

            await SendEmailAsync(adminEmail, mailSubject, mailBody);

        }

        public async Task SendMeetingScheduledEmailAsync(string menteeEmail, string programName, DateTime scheduleDate)
        {
            // Compose email content
            var mailSubject = "New Meeting Scheduled";
            var mailBody = $"Dear Mentee,\n\nYour mentor has scheduled a meeting for the program \"{programName}\" on {scheduleDate}. Please check your dashboard for more details.";

            // Send email to the mentee
            await SendEmailAsync(menteeEmail, mailSubject, mailBody);
        }

        public async Task SendExtensionApprovalEmailAsync(string mentorEmail, string programName, DateTime endDate)
        {
            var mailSubject = "Program Extension Approved";
            var mailBody = $"Dear Mentor,\n\nThe program extension for \"{programName}\" has been approved until {endDate:MMMM dd, yyyy}. Please check your dashboard for more details.";

            // Send email to the mentor
            await SendEmailAsync(mentorEmail, mailSubject, mailBody);
        }
    }
}
