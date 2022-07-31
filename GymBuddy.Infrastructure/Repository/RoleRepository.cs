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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly GymDB _db;
        public RoleRepository(GymDB db) : base(db)
        {
            _db = db;

        }
      
        public void Update(Role obj)
        {
            _db.Roles.Update(obj);
        }
    }
}
