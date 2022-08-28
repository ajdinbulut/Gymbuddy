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
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        private readonly GymDB _db;
        public FollowRepository(GymDB db) : base(db)
        {
            _db = db;

        }

        public void Update(Follow obj)
        {
            _db.Follows.Update(obj);
        }
    }
}
