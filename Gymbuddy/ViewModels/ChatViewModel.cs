using GymBuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class ChatViewModel
    {
        public List<Chat>? Messages { get; set; }
        public string? ConnectionId { get; set; }
        public int ReceiverId { get; set; }
    }
}
