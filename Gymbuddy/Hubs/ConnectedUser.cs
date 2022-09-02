using Microsoft.AspNetCore.SignalR;
namespace Gymbuddy.Hubs

{
    public class ConnectedUser : Hub
    {
        public static int totalUsers { get; set; } = 0;

        public override Task OnConnectedAsync()
        {
            totalUsers++;
            Clients.All.SendAsync("updateActiveUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            totalUsers--;
            Clients.All.SendAsync("updateActiveUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
