using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Repository.IRepository
{
    public interface IUserCountryRepository : IRepository<UserCountry>
    {
        public void Update(UserCountry obj);
    }
}
