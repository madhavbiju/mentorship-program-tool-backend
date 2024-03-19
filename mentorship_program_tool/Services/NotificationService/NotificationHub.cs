namespace mentorship_program_tool.Services.NotificationService;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{

    // Hub methods can be implemented here
    public async Task SendMessage(string message, int Id)
    {
        // Clients.All.SendAsync sends the message to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", message, Id);
    }
}
