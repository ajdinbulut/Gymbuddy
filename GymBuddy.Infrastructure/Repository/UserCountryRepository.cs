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
    public class UserCountryRepository : Repository<UserCountry>, IUserCountryRepository
    {
        private readonly GymDB _db;
       public UserCountryRepository(GymDB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(UserCountry obj)
        {
            _db.UserCountries.Update(obj);
        }
    }
}
