using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gymbuddy.ViewModels
{
    public class PostViewModel
    {
        public PostComment PostComment { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostComment> PostComments { get; set; }

    }
}
