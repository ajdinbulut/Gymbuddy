using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public PostComment PostComment { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostComment> PostComments { get; set; }


    }
}
