using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Core.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int UserSenderId { get; set; }
        public User UserSender { get; set; }
        public int UserReceiverId { get; set; }
        public User UserReceiver { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; }
        public DateTime? SentAt { get; set; } 
    }
}
