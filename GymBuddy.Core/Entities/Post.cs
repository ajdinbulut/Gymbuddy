using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostLikes> PostLikes { get; set; }

    }
}
