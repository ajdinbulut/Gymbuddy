using GymBuddy.Infrastructure.UnitOfWork;
using Gymbuddy.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymBuddy.Core.Entities;
using System.Data.Entity;
using Microsoft.AspNetCore.SignalR;
using Gymbuddy.ViewModels;

namespace Gymbuddy.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GymDB _db;
        private readonly UserManager _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext ,IUnitOfWork unitOfWork, GymDB db, UserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public IActionResult Index(int id)
        { 
            var user = _userManager.Get();
            if (user != null)
            {
                ChatViewModel Chat = new ChatViewModel();
                var connectionId = _unitOfWork.Connection.GetFirstOrDefault(x=>x.UserId == id);
                if(connectionId != null) {
                    Chat.ConnectionId = connectionId.ConnectionId;
                }
                Chat.ReceiverId = id;
                var chat = _unitOfWork.Chat.GetAll(includeProperties: "UserSender");
                List<Chat> modelList = new List<Chat>();
                if (chat != null)
                {
                    foreach (var item in chat)
                    {
                        if((item.UserSenderId == id && item.UserReceiverId == user.Id) || (item.UserReceiverId == id && item.UserSenderId == user.Id))
                        {
                            Chat model = new Chat();

                            model.UserReceiverId = item.UserReceiverId;
                            model.UserReceiver = item.UserReceiver;
                            model.UserSenderId = item.UserSenderId;
                            model.UserSender = item.UserSender;
                            model.Message = item.Message;
                            if (user.Id == item.UserReceiverId)
                            {
                                model.IsSeen = true;
                                item.IsSeen = true;
                                _unitOfWork.Chat.Update(item);
                                _unitOfWork.Save();
                            }
                            
                            modelList.Add(model);
                        }
                        
                        
                    }
                }
                Chat.Messages = modelList.OrderByDescending(x => x.SentAt).ToList(); 
                return View(Chat);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public JsonResult Message(string message,int receiver)
        {
            var chat = new Chat();
            var user = _userManager.Get();
            chat.UserSenderId = user.Id;
            chat.UserReceiverId = receiver;
            chat.Message = message;
            chat.IsSeen = false;
            chat.SentAt = DateTime.UtcNow;
            var connection = _unitOfWork.Connection.GetFirstOrDefault(x => x.UserId == receiver);
            MessageViewModel messageModel = new MessageViewModel();
            messageModel.User = user;
            messageModel.ConnectionId = connection.ConnectionId;
            _unitOfWork.Chat.Add(chat);
            _unitOfWork.Save();
            if(connection != null) {
                return new JsonResult(Ok(messageModel));
            }
            else
            {
                return new JsonResult(Ok(user));
            }
        }

    }
}
