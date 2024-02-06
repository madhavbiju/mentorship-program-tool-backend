using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository.ProgramRepository;
using mentorship_program_tool.Services.NotificationService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly AppDbContext _dbContext; // Inject the database context
    private readonly IProgramRepository _programRepository;

    public NotificationService(IHubContext<NotificationHub> hubContext, IProgramRepository programRepository, AppDbContext dbContext)
    {
        _hubContext = hubContext;
        _programRepository = programRepository;
        _dbContext = dbContext;
    }

    public async System.Threading.Tasks.Task SendPairCreationNotificationAsync(string mentorUser, string menteeUser)
    {
        // Send pair creation notification using SignalR
        await _hubContext.Clients.All.SendAsync("PairCreated", mentorUser, menteeUser);

        int mentorId = int.Parse(mentorUser);
        int menteeId = int.Parse(menteeUser);

        var mentorNotification = new Notifications
        {
            NotifiedEmployeeID = mentorId, // Assign mentor ID
            Notification = "Pair creation notification", // Provide the notification message
            CreatedBy = 4, // Provide the user who triggered the notification if available
            CreatedTime = DateTime.Now // Provide the creation time
        };

        // Store the notification in the database for the mentor
        _dbContext.Notifications.Add(mentorNotification);

        var menteeNotification = new Notifications
        {
            NotifiedEmployeeID = menteeId, // Assign mentee ID
            Notification = "Pair creation notification", // Provide the notification message
            CreatedBy = 4, // Provide the user who triggered the notification if available
            CreatedTime = DateTime.Now // Provide the creation time
        };

        // Store the notification in the database for the mentee
        _dbContext.Notifications.Add(menteeNotification);

        // Save changes to the database
        await _dbContext.SaveChangesAsync();
    }



    /*public async Task SendMeetingScheduledNotificationAsync(string mentorUser, string menteeUser, DateTime meetingDateTime)
    {
        await _hubContext.Clients.All.SendAsync("MeetingScheduledNotification", mentorUser, menteeUser, meetingDateTime);
    }*/


    public async System.Threading.Tasks.Task SendExtensionRequestNotificationAsync(string adminUser)
    {
        await _hubContext.Clients.All.SendAsync("ExtensionRequestNotification", adminUser);
    }

    /* public async Task SendExtensionApprovalNotificationAsync(string mentorUser)
     {
         await _hubContext.Clients.All.SendAsync("ExtensionApprovalNotification", mentorUser);
     }*/

    public async System.Threading.Tasks.Task SendTaskPostedNotificationAsync(string menteeUser)
    {
        await _hubContext.Clients.All.SendAsync("TaskPostedNotification", menteeUser);
    }
    /*public async Task SendTaskSubmittedNotificationAsync(string mentorUser)
    {
        await _hubContext.Clients.All.SendAsync("TaskSubmittedNotification", mentorUser);
    }

    public async Task SendTaskDueDateUpdatedNotificationAsync(string menteeUser)
    {
        await _hubContext.Clients.All.SendAsync("TaskDueDateUpdatedNotification", menteeUser);
    }*/
}
