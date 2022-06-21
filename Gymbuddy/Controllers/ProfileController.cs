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
                GymDB db = new GymDB();
                var modelAsJson = HttpContext.Session.GetString("loggedUser");
                var model = JsonConvert.DeserializeObject<User>(modelAsJson);
                var userCountry = db.UserCountries.FirstOrDefault(x => x.UserId == model.Id);
                UserCountryViewModel userCountryVM = new UserCountryViewModel()
                {
                    UserName = model.Name,
                    username = model.username,
                    email = model.email,
                    age = model.age,
                    CountryName = userCountry.Name
                };
                return View(userCountryVM);
            }
            ;
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
      
        public IActionResult AddCountry()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddCountry(string Name)
        {
            GymDB db = new GymDB();
            var modelAsJson = HttpContext.Session.GetString("loggedUser");
            var model = JsonConvert.DeserializeObject<User>(modelAsJson);
            var user = db.Users.Find(model.Id);
            UserCountry userCountry = new UserCountry();
            userCountry.Name = Name;
            userCountry.UserId = user.Id;
            db.UserCountries.Add(userCountry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
