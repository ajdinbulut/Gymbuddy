using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.UnitOfWork;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


    public class ChatHub : Hub
    {
    private readonly GymDB _db;
    private readonly UserManager _userManager;
    public static int number { get; set; } = 0;

    public ChatHub(GymDB db,UserManager userManager)
    {
        _userManager = userManager;
        _db = db;
    }
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
    public async Task SendToUser(string receiverConnectionId, string message)
        {
        var user = _db.Connection.FirstOrDefault(x => x.ConnectionId == receiverConnectionId);
        NewMessagesViewModel newMessages = new NewMessagesViewModel();
        var chat = _db.Chats.ToList();
        List<Chat> chats = new List<Chat>();
        foreach (var item in chat)
        {
            if(item.UserReceiverId == user.UserId && item.IsSeen == false)
            {
                Chat model = new Chat();
                model.Id = item.Id;
                model.UserReceiverId = item.UserReceiverId;
                model.UserReceiver = item.UserReceiver;
                model.UserSenderId = item.UserSenderId;
                model.UserSender = item.UserSender;
                model.SentAt = item.SentAt;
                model.Message = item.Message;
                newMessages.NewMessagesNumber++;
                chats.Add(model);
            }
            
        }
        newMessages.Messages = chats;

        await Clients.Client(receiverConnectionId).SendAsync("Message", message);
        await Clients.Client(receiverConnectionId).SendAsync("Notification",newMessages);
        }

        public string GetConnectionId() => Context.ConnectionId;
    public override Task OnConnectedAsync()
    {
        var user = _userManager.Get();
        if (user != null)
        {
            //var model = _db.Connection.FirstOrDefault(x=>x.Id == user.Id);
        var connectionId = Context.ConnectionId;
        Connection connection = new Connection();
         
                connection.UserId = user.Id;
                connection.ConnectionId = connectionId;
                _db.Connection.Add(connection);
                _db.SaveChanges();

        }
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _userManager.Get();
        if (user != null)
        {
            var model = _db.Connection.FirstOrDefault(x => x.UserId == user.Id);


            if (model != null)
            {
                _db.Connection.Remove(model);
                _db.SaveChanges();
            }
        }
        return base.OnDisconnectedAsync(exception);
    }


}
