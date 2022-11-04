using Gymbuddy.Infrastructure;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.SignalR;
namespace Gymbuddy.Hubs

{
    public class ConnectedUser : Hub
    {
        private readonly GymDB _db;
        private readonly UserManager _userManager;
        public ConnectedUser(GymDB db, UserManager userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        public static int totalUsers { get; set; } = 0;
        public string? connectionId { get; set; }
        public string GetConnectionId() => Context.ConnectionId;
        public override Task OnConnectedAsync()
        {
            totalUsers++;
            //var user = _userManager.Get();
            //var connectionId = Context.ConnectionId;
            //Connection connection = new Connection();
            //if (user != null)
            //{
            //    connection.UserId = user.Id;
            //    connection.ConnectionId = connectionId;
            //    _db.Connection.Add(connection);
            //    _db.SaveChanges();

            //}
            Clients.All.SendAsync("updateActiveUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            totalUsers--;
            //var user = _userManager.Get();
            //var model = _db.Connection.FirstOrDefault(x => x.UserId == user.Id);
            //if(model != null)
            //{
            //    _db.Connection.Remove(model);
            //    _db.SaveChanges();
            //}
            Clients.All.SendAsync("updateActiveUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
