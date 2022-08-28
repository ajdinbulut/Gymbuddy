using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class isPostLikedViewModel
    {
        public int PostId { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Likes { get; set; }
        public bool? isLiked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostLikes> PostLikes { get; set; }
    }
}
