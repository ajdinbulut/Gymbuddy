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

namespace Gymbuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;

        public HomeController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var posts = _unitOfWork.Post.GetAll();
            PostViewModel post = new PostViewModel();
           
            return View(posts);
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
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.username == mod.username || u.email == mod.email);

            if (user != null)
            {
                TempData["failed"] = "Username/email is taken.";
                return View(mod);

            }
            model.username = mod.username;
            model.password = mod.password;
            model.age = mod.age;
            model.email = mod.email;
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

            var user = _unitOfWork.User.GetFirstOrDefault(x => x.username == model.username && x.password == model.password);
            if (user != null)
            {
                HttpContext.Session.SetString("loggedUser", JsonConvert.SerializeObject(user));
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
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index");
        }
        public IActionResult Post()
        {

            return View();
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
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("loggedUser"));
            Post post = new Post();
            post.UserId = user.Id;
            post.ImageUrl = PostVM.Post.ImageUrl;
            post.Description = PostVM.Post.Description;
            _unitOfWork.Post.Add(post);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }





    }
}