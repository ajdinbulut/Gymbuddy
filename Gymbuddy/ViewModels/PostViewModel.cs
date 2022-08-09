using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gymbuddy.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

    }
}
