using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly GymDB _db;
       public PostRepository(GymDB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Post obj)
        {
            _db.Posts.Update(obj);
        }
    }
}
