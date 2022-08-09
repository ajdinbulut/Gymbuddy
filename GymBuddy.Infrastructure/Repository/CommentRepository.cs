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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly GymDB _db;
       public CommentRepository(GymDB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Comment obj)
        {
            _db.Comments.Update(obj);
        }
    }
}
