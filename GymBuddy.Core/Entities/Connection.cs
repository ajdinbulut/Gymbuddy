using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Core.Entities
{
    public class Connection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ConnectionId { get; set; }
    }
}
