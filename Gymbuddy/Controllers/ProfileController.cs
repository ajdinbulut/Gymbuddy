using Gymbuddy.Entities;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("loggedUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var modelAsJson = HttpContext.Session.GetString("loggedUser");
                var model = JsonConvert.DeserializeObject<User>(modelAsJson);
                return View(model);
            }
            ;
        }
        public IActionResult EditUN()
        {
            var modelAsJson = HttpContext.Session.GetString("loggedUser");
            var model = JsonConvert.DeserializeObject<User>(modelAsJson);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditUN(string username,int id)
        {
            GymDB db = new GymDB();
            var model = db.Users.Find(id);
            model.username = username;
            db.Users.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EditPW()
        {
            var modelAsJson = HttpContext.Session.GetString("loggedUser");
            var model = JsonConvert.DeserializeObject<User>(modelAsJson);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditPW(string password, int id)
        {
            GymDB db = new GymDB();
            var model = db.Users.Find(id);
            model.password = password;
            db.Users.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
      
    }
}
