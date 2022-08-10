using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly GymDB _db;
        private readonly UserManager _userManager;

        public CompetitionController(GymDB db,UserManager userManager)
        {
            _db = db;
            _userManager = userManager;

        }
        public IActionResult Index()
        {

            if (_userManager.isSignedIn())
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
            var user = _userManager.Get();
           

            return View(user);
        }
        [HttpPost]
        public IActionResult CompetitionSignUp(CompetingUser user)
        {
            var getId = _db.Users.FirstOrDefault(x=>x.Username == user.username);
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
