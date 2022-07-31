using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using GymBuddy.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly GymDB _db;
        public UserRepository(GymDB db) : base(db)
        {
            _db = db;
        }

     
        public void Update(User obj)
        {
            _db.Users.Update(obj);
        }
    }
}
