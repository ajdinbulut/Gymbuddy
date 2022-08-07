using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using GymBuddy.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GymDB _db;

        public AdministrationController(IUnitOfWork unitOfWork, GymDB db)
        {
            _unitOfWork = unitOfWork;
            _db = db;   
        }
        public IActionResult Index()
        {
            //var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("loggedUser"));
            //var userRole = _unitOfWork.UserRole.GetFirstOrDefault(x => x.UserId == user.Id);
            //var role = _unitOfWork.Role.GetFirstOrDefault(x => x.Id == userRole.RoleId);
            ////if (role.Name != "Admin")
            ////{
            ////    return RedirectToAction("Index", "Home");
            ////}
            List<AdministrationViewModel> AdministrationVM = new List<AdministrationViewModel>();
            var users = _unitOfWork.User.GetAll(includeProperties:"UserRoles,UserRoles.Role");
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
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            var userID = _unitOfWork.UserRole.GetFirstOrDefault(x => x.UserId == id);
            var roleID = _unitOfWork.Role.GetFirstOrDefault(x => x.Id == userID.RoleId);
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
            UserRole userRole = new UserRole();
            var user = _unitOfWork.User.GetFirstOrDefault(u=>u.Id== editAcc.Id);
            user.Name = editAcc.Name;
            user.username = editAcc.Username;
            user.age = editAcc.Age;
            user.password = editAcc.password;
            user.email = editAcc.email;
            userRole.UserId = editAcc.Id;
            userRole.RoleId = editAcc.RoleID;
            if (!_db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.RoleID))
            {
                _unitOfWork.UserRole.Add(userRole);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAcc(int id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.User.Remove(user);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
