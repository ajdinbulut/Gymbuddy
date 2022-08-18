using Gymbuddy.Core.Entities;
using Gymbuddy.Models;
using Gymbuddy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using Gymbuddy.Infrastructure;
using GymBuddy.Infrastructure.Repository.IRepository;
using GymBuddy.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gymbuddy.Utilities;

namespace Gymbuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly UserManager _userManager;
        private readonly FileManager _fileManager;
        private readonly GymDB _db;


        public HomeController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment, UserManager userManager, GymDB db,FileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
            _userManager = userManager;
            _db = db;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            PostViewModel PostVM = new PostViewModel();
            PostVM.Posts = _unitOfWork.Post.GetAll(includeProperties:"User,Comments");
            //PostVM.Comments = _unitOfWork.Comment.GetAll(includeProperties:"User,Post");
            if (PostVM.Posts != null)
            {
                return View(PostVM);
            }
            return View();
            
            //PostVM.Comments = _unitOfWork.Comment.GetAll();
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
        public IActionResult Details(int id)
        {

            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            return View(user);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel mod, IFormFile? file)
        {
            string wwwRootPath = _hostEnviroment.WebRootPath;
            User model = new User();
            UserRole userRole = new UserRole();
            var role = _unitOfWork.Role.GetFirstOrDefault(x => x.Name == "User");
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Username == mod.username || u.Email == mod.email);

            if (user != null)
            {
                TempData["failed"] = "Username/email is taken.";
                return View(mod);

            }
            model.Username = mod.username;
            model.Password = mod.password;
            model.Age = mod.age;
            model.Email = mod.email;
            model.Name = mod.name;
            model.ProfilePhoto = "/images/profilePhotos/profilephoto.jpg";
            _unitOfWork.User.Add(model);
            _unitOfWork.Save();
            userRole.UserId = model.Id;
            userRole.RoleId = role.Id;
            _unitOfWork.UserRole.Add(userRole);
            _unitOfWork.Save();
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

            var user = _unitOfWork.User.GetFirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null)
            {
                _userManager.SignIn(user);
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
            _userManager.SignOut();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Post(PostViewModel PostVM, IFormFile? file)
        {
            string wwwRootPath = _hostEnviroment.WebRootPath;
            if(file != null)
            {
             PostVM.Post.ImageUrl = _fileManager.postUpload(file);
            }
            var user = _userManager.Get();
            Post post = new Post();
            post.UserId = user.Id;
            post.ImageUrl = PostVM.Post.ImageUrl;
            post.Description = PostVM.Post.Description;
            _unitOfWork.Post.Add(post);
            _unitOfWork.Save();
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
        public IActionResult DeletePost(int id)
        {
            var post = _db.Posts.Find(id);
            _unitOfWork.Post.Remove(post);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Comment(PostViewModel PostVM,int id)
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
        //[HttpPost]
        //public JsonResult LikePost(bool like,int id)
        //{
        //    if (like)
        //    {
        //        var post = _unitOfWork.Post.GetFirstOrDefault(x => x.Id == id);
        //        post.Likes += 1;
        //    }
        //    return new JsonResult(Ok());
        //}



    }
}