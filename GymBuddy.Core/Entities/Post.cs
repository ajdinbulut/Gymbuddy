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
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
