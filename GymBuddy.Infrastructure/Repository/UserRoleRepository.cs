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
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly GymDB _db;
        public UserRoleRepository(GymDB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(UserRole obj)
        {
            _db.UserRoles.Update(obj);
        }
    }
}
