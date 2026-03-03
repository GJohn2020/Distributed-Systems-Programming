using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class ChatroomHub:Hub
    {
            public async Task BroadcastMessage(string user, string message)
            {
                await Clients.All.SendAsync("GetMessage", user, message);
        }
    }
}
