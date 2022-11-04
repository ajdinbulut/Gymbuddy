using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Gymbuddy.Utilities;
using Gymbuddy.ViewModels;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.UnitOfWork;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gymbuddy.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager _userManager;
        private readonly FileManager _fileManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProfileController(IUnitOfWork unitOfWork,UserManager userManager,FileManager fileManager,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _fileManager = fileManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            if (!_userManager.isSignedIn())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                //var model = _userManager.Get();
                //var userCountry = _unitOfWork.UserCountry.GetFirstOrDefault(x => x.UserId == model.Id);
                //UserCountryViewModel userCountryVM = new UserCountryViewModel();
                //if (userCountry != null)
                //{
                //    userCountryVM = new UserCountryViewModel()
                //    {
                //        CountryName = userCountry.Name,
                //        Name = model.Name,
                //        username = model.Username,
                //        email = model.Email,
                //        age = model.Age,

                //    };
                //}
                //else
                //{
                //    userCountryVM = new UserCountryViewModel()
                //    {
                //        Name = model.Name,
                //        username = model.Username,
                //        email = model.Email,
                //        age = model.Age,

                //    };

                //}
                ProfileViewModel profile = new ProfileViewModel();
                profile.User = _userManager.Get();
                profile.Posts = _unitOfWork.Post.GetAll(includeProperties: "User");
                profile.Comments = _unitOfWork.Comment.GetAll(includeProperties: "User");
                return View(profile);
            }
            ;
        }
        
        public IActionResult EditPW()
        {
            var model = _userManager.Get();
            return View(model);
        }
      
      
        public IActionResult AddCountry()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddCountry(string Name)
        {
            var model = _userManager.Get();
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == model.Id);
            UserCountry userCountry = new UserCountry();
            userCountry.Name = Name;
            userCountry.UserId = user.Id;
            _unitOfWork.UserCountry.Add(userCountry);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Comment(PostViewModel PostVM, int id)
        {
            var user = _userManager.Get();
            Comment comment = new Comment();
            comment.UserId = user.Id;
            comment.Description = PostVM.Comment.Description;
            comment.PostId = id;
            _unitOfWork.Comment.Add(comment);
            _unitOfWork.Save();
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangePhoto(ProfileViewModel profileVM,IFormFile? file)
        {
            var model = _userManager.Get();
            var user = _unitOfWork.User.GetFirstOrDefault(x => x.Id == model.Id);
            User obj = new User();
            if (file != null)
            {
                obj.ProfilePhoto = _fileManager.profilePhoto(file);
            }
            user.ProfilePhoto = obj.ProfilePhoto;
            _userManager.SignOut();
            _userManager.SignIn(user);
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
