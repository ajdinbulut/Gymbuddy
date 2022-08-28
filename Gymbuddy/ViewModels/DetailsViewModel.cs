using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gymbuddy.ViewModels
{
    public class DetailsViewModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
        public Post Post { get; set; }
        public bool isFollowing { get; set; }
        public IEnumerable<isPostLikedViewModel> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostLikes> PostLikes { get; set; }

    }
}
