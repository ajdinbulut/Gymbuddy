using Gymbuddy.Entities;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class CompetitionController : Controller
    {
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("loggedUser") != null)
            {
                GymDB db = new GymDB();
                var user = db.CompetingUser.OrderByDescending(x => x.total).ToList();
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
            GymDB db = new GymDB();
            var getId = db.Users.FirstOrDefault(x => x.username == user.username);
            CompetingUser model = new CompetingUser();
            model.username = user.username;
            model.bench = user.bench;
            model.deadlift = user.deadlift;
            model.squat = user.squat;
            model.total = user.squat + user.bench + user.deadlift;
            model.UserId = getId.Id;

            var check = db.CompetingUser.FirstOrDefault(x => x.username == user.username);
            if (check == null)
            {

                db.CompetingUser.Add(model);
                db.SaveChanges();
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
            GymDB db = new GymDB();
            var user = db.CompetingUser.OrderByDescending(x => x.bench).ToList();
            return View("Index",user);
        }
        public IActionResult SortDeadlift()
        {
            GymDB db = new GymDB();
            var user = db.CompetingUser.OrderByDescending(x => x.deadlift).ToList();
            return View("Index", user);
        }
        public IActionResult SortSquat()
        {
            GymDB db = new GymDB();
            var user = db.CompetingUser.OrderByDescending(x => x.squat).ToList();
            return View("Index", user);
        }
        public IActionResult SortTotal()
        {
            GymDB db = new GymDB();
            var user = db.CompetingUser.OrderByDescending(x => x.total).ToList();
            return View("Index", user);
        }
    }
}
