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
                obj.FirstName = items.FirstName;
                obj.LastName = items.LastName;
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
            editAcc.FirstName = user.FirstName;
            editAcc.LastName = user.LastName;
            editAcc.Username = user.Username;
            editAcc.Age = user.Age;
            editAcc.Email = user.Email;
            editAcc.UserRoles = _unitOfWork.UserRole.GetAll(includeProperties:"Role,Roles").Where(x => x.UserId == user.Id);
            var roles = _unitOfWork.Role.GetAll();
            var userRoles = _db.UserRoles.Where(x => x.UserId == id);
            editAcc.selectedRole = roles.Select(r => new SelectedRoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
                Selected = userRoles.Any(ur=>ur.RoleId == r.Id)
            });
            ;
            return View(editAcc);
        }
        [HttpPost]
        public IActionResult EditAcc(EditAccViewModel editAcc,int id)
        {
            UserRole userRole = new UserRole();
            var user = _unitOfWork.User.GetFirstOrDefault(u=>u.Id== editAcc.Id);
            user.FirstName = editAcc.FirstName;
            user.LastName = editAcc.LastName;
            user.Username = editAcc.Username;
            user.Age = editAcc.Age;
            user.PasswordHash = editAcc.PasswordHash;
            user.Email = editAcc.Email;
            userRole.UserId = editAcc.Id;
            //foreach (var item in editAcc.UserRoles)
            //{
            //    if (!_db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.Id))
            //    {
            //        userRole.RoleId = item.RoleId;
            //        _unitOfWork.UserRole.Add(userRole);

            //    }
            //}
            foreach (var item in editAcc.selectedRole)
            {

                if (!_db.UserRoles.Any(x => x.UserId == editAcc.Id && x.RoleId == editAcc.RoleId) && item.Selected == true)
                {
                    userRole.RoleId = item.RoleId;

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
