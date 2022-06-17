using Gymbuddy.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            GymDB db = new GymDB();
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("loggedUser"));
            var userRole = db.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            var role = db.Roles.FirstOrDefault(x => x.Id == userRole.RoleId);
            if (role.Name != "Admin")
            {
               return RedirectToAction("Index","Home");
            }
                return View();
        }
    }
}
