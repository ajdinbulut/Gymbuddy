using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gymbuddy.ViewModels
{
    public class PostViewModel
    {
        public User User { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostLikes> PostLikes { get; set; }

    }
}
