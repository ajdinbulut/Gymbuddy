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

namespace Gymbuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly UserManager _userManager;
        private readonly GymDB _db;


        public HomeController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment, UserManager userManager, GymDB db)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            PostViewModel PostVM = new PostViewModel();
            PostVM.Posts = _unitOfWork.Post.GetAll(includeProperties:"User");
            PostVM.PostComments = _unitOfWork.PostComment.GetAll(includeProperties:"Post,Post.User,Comment,Comment.User");
            PostVM.Comments = _unitOfWork.Comment.GetAll(includeProperties:"User");
            return View(PostVM);

            
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
        public IActionResult Register(RegisterViewModel mod)
        {
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
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\posts");
                var extension = Path.GetExtension(file.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                PostVM.Post.ImageUrl = @"\images\posts\" + fileName + extension;
            }
            var user = _userManager.Get();
            Post post = new Post();
            post.UserId = user.Id;
            post.ImageUrl = PostVM.Post.ImageUrl;
            post.Description = PostVM.Post.Description;
            _unitOfWork.Post.Add(post);
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
            PostComment postComment = new PostComment();
            Comment comment = new Comment();
            comment.UserId = user.Id;
            comment.Description = PostVM.Comment.Description;
            _unitOfWork.Comment.Add(comment);
            _unitOfWork.Save();
            postComment.CommentId = comment.Id;
            postComment.PostId = id;
            _unitOfWork.PostComment.Add(postComment);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }



    }
}