using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.ViewModels;
using GymBuddy.Infrastructure.UnitOfWork;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GymDB _db;
        private readonly UserManager _userManager;

        public AdministrationController(IUnitOfWork unitOfWork, GymDB db,UserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            
            if (_userManager.isAdmin() != true)
            {
               return RedirectToAction("Index", "Home");
            }
            List<AdministrationViewModel> AdministrationVM = new List<AdministrationViewModel>();
            var users = _unitOfWork.User.GetAll(includeProperties:"UserRoles,UserRoles.Role");
            foreach (var items in users)
            {
                AdministrationViewModel obj = new AdministrationViewModel();

                obj.Id = items.Id;
                obj.Name = items.Name;
                obj.Age = items.Age;
                obj.Username = items.Username;
                obj.UserRoles = items.UserRoles;
                AdministrationVM.Add(obj);
            }
            return View(AdministrationVM);
        }
        public IActionResult EditAcc(int id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            EditAccViewModel editAcc = new EditAccViewModel();
            editAcc.Id = user.Id;
            editAcc.Name = user.Name;
            editAcc.Username = user.Username;
            editAcc.Age = user.Age;
            editAcc.email = user.Email;
            editAcc.password = user.Password;
            editAcc.UserRoles = _unitOfWork.UserRole.GetAll(includeProperties:"Role,Roles").Where(x => x.UserId == user.Id);
            editAcc.Roles = _unitOfWork.Role.GetAll();
            return View(editAcc);
        }
        [HttpPost]
        public IActionResult EditAcc(EditAccViewModel editAcc,int id)
        {
            UserRole userRole = new UserRole();
            var user = _unitOfWork.User.GetFirstOrDefault(u=>u.Id== editAcc.Id);
            user.Name = editAcc.Name;
            user.Username = editAcc.Username;
            user.Age = editAcc.Age;
            user.Password = editAcc.password;
            user.Email = editAcc.email;
            userRole.UserId = editAcc.Id;
            //foreach (var item in editAcc.UserRoles)
            //{
            //    if (!_db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.Id))
            //    {
            //        userRole.RoleId = item.RoleId;
            //        _unitOfWork.UserRole.Add(userRole);

            //    }
            //}
            foreach (var item in editAcc.Roles)
            {

                if (!_db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.RoleId))
                {
                    userRole.RoleId = item.Id;

                }
            }
            _unitOfWork.UserRole.Add(userRole);
            _unitOfWork.User.Update(user);
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
