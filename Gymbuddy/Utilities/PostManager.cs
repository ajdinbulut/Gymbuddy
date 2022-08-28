using Gymbuddy.Infrastructure;
using GymBuddy.Infrastructure.UnitOfWork;
using GymBuddy.Infrastructure.Utilities;

namespace Gymbuddy.Utilities
{
    public class PostManager
    {
        private readonly GymDB _db;
        public PostManager(GymDB db)
        {
            _db = db;
        }
        public bool isLiked(int postId, int userId)
        {
            var post = _db.PostLikes.ToList();
            if(post.Any(x=>x.PostId == postId && x.UserId == userId)){
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
