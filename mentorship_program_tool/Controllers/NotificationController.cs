using Microsoft.AspNetCore.Mvc;
using mentorship_program_tool.Services.NotificationService;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /* [HttpPost("send/notification/pairCreated")]
         public async Task<IActionResult> SendNotification(string user, string message)
         {
             // Call SendNotificationAsync method of NotificationService
             await _notificationService.SendPairCreationNotificationAsync(user, message);  //here mentor mentee id should be passed

             return Ok("Notification sent successfully.");
         }*/

        /*[HttpPost("send/notification/meetingScheduled")]
        public async Task<IActionResult> SendNotification(string id, string id, DateTime meetingDateTime)
        {
            // Call SendNotificationAsync method of NotificationService
            await _notificationService.SendMeetingScheduledNotificationAsync(user, message, date);

            return Ok("Notification sent successfully.");
        }*/

        /*[HttpPost("send/notification/extensionRequest")]
        public async Task<IActionResult> SendNotification(string adminUser)
        {
            // Call SendNotificationAsync method of NotificationService
            await _notificationService.SendExtensionRequestNotificationAsync(adminUser);

            return Ok("Notification sent successfully.");
        }*/

    }
}
