using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }


    }
}
