namespace mentorship_program_tool.Services.NotificationService;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    // Hub methods can be implemented here
    public async Task SendMessage(string user, string message)
    {
        // Clients.All.SendAsync sends the message to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
