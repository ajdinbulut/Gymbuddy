using Gymbuddy.Entities;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return RedirectToAction("Index", "Home");
            }
            List<AdministrationViewModel> AdministrationVM = new List<AdministrationViewModel>();
            var users = db.Users.Include(x=>x.UserRoles).ToList();
            foreach (var items in users)
            {
                AdministrationViewModel obj = new AdministrationViewModel();
                
                obj.Id = items.Id;
                obj.Name = items.Name;
                obj.Age = items.age;
                obj.Username = items.username;
                obj.UserRoles = items.UserRoles;
                AdministrationVM.Add(obj);
            }
            return View(AdministrationVM);
        }
        public IActionResult EditAcc(int id)
        {
            GymDB db = new GymDB();
            var user = db.Users.Find(id);
            var userID = db.UserRoles.FirstOrDefault(x => x.UserId == id);
            var roleID = db.Roles.FirstOrDefault(x => x.Id == userID.RoleId);
            EditAccViewModel editAcc = new EditAccViewModel();
            editAcc.Id = user.Id;
            editAcc.Name = user.Name;
            editAcc.Username = user.username;
            editAcc.Age = user.age;
            editAcc.email = user.email;
            editAcc.password = user.password;
            editAcc.RoleID = userID.RoleId;
            return View(editAcc);
        }
        [HttpPost]
        public IActionResult EditAcc(EditAccViewModel editAcc)
        {
            GymDB db = new GymDB();
            UserRole userRole = new UserRole();
            var user = db.Users.Find(editAcc.Id);
            user.Name = editAcc.Name;
            user.username = editAcc.Username;
            user.age = editAcc.Age;
            user.password = editAcc.password;
            user.email = editAcc.email;
            userRole.UserId = editAcc.Id;
            userRole.RoleId = editAcc.RoleID;
            if (!db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.RoleID))
            {
                db.UserRoles.Add(userRole);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
