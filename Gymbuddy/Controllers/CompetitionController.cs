using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly GymDB _db;

        public CompetitionController(GymDB db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("loggedUser") != null)
            {
                var user = _db.CompetingUsers.OrderByDescending(x => x.total).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult CompetitionSignUp()
        {
            var modelAsJson = HttpContext.Session.GetString("loggedUser");
            var user = JsonConvert.DeserializeObject<CompetingUser>(modelAsJson);
           

            return View(user);
        }
        [HttpPost]
        public IActionResult CompetitionSignUp(CompetingUser user)
        {
            var getId = _db.Users.FirstOrDefault(x=>x.username == user.username);
            CompetingUser model = new CompetingUser();
            model.username = user.username;
            model.bench = user.bench;
            model.deadlift = user.deadlift;
            model.squat = user.squat;
            model.total = user.squat + user.bench + user.deadlift;
            model.UserId = getId.Id;

            var check = _db.CompetingUsers.FirstOrDefault(x => x.username == user.username);
            if (check == null)
            {

                _db.CompetingUsers.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                TempData["fail"] = "You have already signed up";
                return RedirectToAction("Index");

            }
        }
        public IActionResult SortBench()
        {
            var user = _db.CompetingUsers.OrderByDescending(x => x.bench).ToList();
            return View("Index",user);
        }
        public IActionResult SortDeadlift()
        {
            var user = _db.CompetingUsers.OrderByDescending(x => x.deadlift).ToList();
            return View("Index", user);
        }
        public IActionResult SortSquat()
        {
            var user = _db.CompetingUsers.OrderByDescending(x => x.squat).ToList();
            return View("Index", user);
        }
        public IActionResult SortTotal()
        {
            var user = _db.CompetingUsers.OrderByDescending(x => x.total).ToList();
            return View("Index", user);
        }
    }
}
