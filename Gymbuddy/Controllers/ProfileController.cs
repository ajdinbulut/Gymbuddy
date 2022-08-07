using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using GymBuddy.Infrastructure.UnitOfWork;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (HttpContext?.Session.GetString("loggedUser") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var modelasJson = HttpContext?.Session.GetString("loggedUser");
                var model = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString(modelasJson));
                var userCountry = _unitOfWork.UserCountry.GetFirstOrDefault(x => x.UserId == model.Id);
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
            var model = _unitOfWork.User.GetFirstOrDefault(u=>u.Id == id);
            model.password = password;
            _unitOfWork.User.Update(model);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Home");
        }
      
        public IActionResult AddCountry()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddCountry(string Name)
        {
            var modelAsJson = HttpContext.Session.GetString("loggedUser");
            var model = JsonConvert.DeserializeObject<User>(modelAsJson);
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == model.Id);
            UserCountry userCountry = new UserCountry();
            userCountry.Name = Name;
            userCountry.UserId = user.Id;
            _unitOfWork.UserCountry.Add(userCountry);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
