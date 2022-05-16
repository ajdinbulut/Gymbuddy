using Gymbuddy.Entities;
using Gymbuddy.Models;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel mod)
        {
            GymDB db = new GymDB();
            User model = new User();
            var user = db.Users.FirstOrDefault(u => u.username == mod.username || u.email == mod.email);

            if (user != null)
            {
                TempData["failed"] = "Username/email is taken.";
                return View(mod);

            }
            model.username = mod.username;
            model.password = mod.password;
            model.age = mod.age;
            model.email = mod.email;
            model.Name = mod.name;
            db.Users.Add(model);
            db.SaveChanges();
            TempData["success"] = "Thank you for registering!";
            return RedirectToAction("Index");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User model)
        {
            GymDB db = new GymDB();

            var user = db.Users.FirstOrDefault(x => x.username == model.username && x.password == model.password);
            if (user!=null)
            {
                HttpContext.Session.SetString("loggedUser", JsonConvert.SerializeObject(user));
                return RedirectToAction("Index");
            }
            else
            {
                TempData["fail"] = "You have entered an incorrect username or password.";
                return View("Login");
            }
            
        
        }
        
       
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index");
        }
       
        }
        
        
        
        
    
}