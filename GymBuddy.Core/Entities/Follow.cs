using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Core.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FollowingUserId { get; set; }
        public User FollowingUser { get; set; }
    }
}
