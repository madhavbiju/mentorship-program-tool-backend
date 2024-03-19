using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository.ProgramRepository;
using mentorship_program_tool.Services.NotificationService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class SignalNotificationService : ISignalNotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly AppDbContext _dbContext; // Inject the database context
    private readonly IProgramRepository _programRepository;

    public SignalNotificationService(IHubContext<NotificationHub> hubContext, IProgramRepository programRepository, AppDbContext dbContext)
    {
        _hubContext = hubContext;
        _programRepository = programRepository;
        _dbContext = dbContext;
    }

    public async System.Threading.Tasks.Task SendPairCreationNotificationAsync(string mentorUser, string menteeUser)
    {

        int mentorId = int.Parse(mentorUser);
        int menteeId = int.Parse(menteeUser);

        var mentorNotification = new Notifications
        {
            NotifiedEmployeeID = mentorId, // Assign mentor ID
            Notification = "New Pair created", // Provide the notification message
            CreatedBy = 4, // Provide the user who triggered the notification if available
            CreatedTime = DateTime.Now // Provide the creation time
        };

        // Store the notification in the database for the mentor
        _dbContext.Notifications.Add(mentorNotification);

        var menteeNotification = new Notifications
        {
            NotifiedEmployeeID = menteeId, // Assign mentee ID
            Notification = " New Pair created", // Provide the notification message
            CreatedBy = 4, // Provide the user who triggered the notification if available
            CreatedTime = DateTime.Now // Provide the creation time
        };

        // Store the notification in the database for the mentee
        _dbContext.Notifications.Add(menteeNotification);

        // Save changes to the database
        await _dbContext.SaveChangesAsync();

        // Send pair creation notification using SignalR
        await _hubContext.Clients.All.SendAsync("PairCreated", "New Pair Created", mentorUser, menteeUser);
    }



    public async System.Threading.Tasks.Task SendMeetingScheduledNotificationAsync(string menteeUser, DateTime meetingDateTime)
    {
        await _hubContext.Clients.All.SendAsync("MeetingScheduledNotification", $"New meeting is scheduled by mentor on {meetingDateTime}", menteeUser);
    }


    public async System.Threading.Tasks.Task SendExtensionRequestNotificationAsync(string adminUser, string mentorID, string mentorName)
    {
        await _hubContext.Clients.All.SendAsync("ExtensionRequestNotification", $"A Program extension request raised by {mentorName}", adminUser, mentorID, mentorName);
    }

    public async System.Threading.Tasks.Task SendExtensionApprovalNotificationAsync(string mentorUser)
    {
        await _hubContext.Clients.All.SendAsync("ExtensionApprovalNotification", "Your program extension request has been approved.", mentorUser);
    }

    public async System.Threading.Tasks.Task SendTaskPostedNotificationAsync(string menteeUser)
    {
        await _hubContext.Clients.All.SendAsync("TaskPostedNotification", "New Task is Assigned to You", menteeUser);
    }


    public async System.Threading.Tasks.Task SendTaskSubmittedNotificationAsync(string mentorUser, string menteeName)
    {
        await _hubContext.Clients.User(mentorUser).SendAsync("TaskSubmittedNotification", $"Task assigned is submitted by Your mentee {menteeName}", mentorUser);
    }

    public async System.Threading.Tasks.Task SendTaskDueDateUpdatedNotificationAsync(string menteeUser, string taskName)
    {
        await _hubContext.Clients.User(menteeUser).SendAsync("TaskDueDateUpdatedNotification", $"Task {taskName} due date is extented", menteeUser);
    }
}
