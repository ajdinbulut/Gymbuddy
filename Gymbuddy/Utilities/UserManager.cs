using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Utilities
{
    public class UserManager
    {
        private const string _loggedUserKey = "loggedUser";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GymDB _db;
        public UserManager(IHttpContextAccessor httpContextAccessor,GymDB db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public User? Get()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetString(_loggedUserKey);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<User>(_httpContextAccessor.HttpContext.Session.GetString(_loggedUserKey));
            }
            else
            {
                return null;
            }
        }
        public void SignIn(User user)
        {
            _httpContextAccessor.HttpContext.Session.SetString(_loggedUserKey, JsonConvert.SerializeObject(user));
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Remove(_loggedUserKey);
        }
       
        public bool isAdmin()
        {
            var user = Get();
            var role = _db.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (_db.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id))
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        public bool isUser()
        {
            var user = Get();
            var role = _db.Roles.FirstOrDefault(x => x.Name == "User");
            if (_db.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isSignedIn()
        {
            var user = Get();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
