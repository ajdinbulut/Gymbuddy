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
using Gymbuddy.Hubs;

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
            var user = _userManager.Get();
            PostViewModel PostVM = new PostViewModel();
            var post = _unitOfWork.Post.GetAll(includeProperties:"PostLikes,User,Comments");
            List<isPostLikedViewModel> isPostLiked = new List<isPostLikedViewModel>();
            if (user != null)
            {
                foreach (var item in post)
                {

                    isPostLikedViewModel model = new isPostLikedViewModel();
                    model.PostLikes = item.PostLikes;
                    model.ImageUrl = item.ImageUrl;
                    model.Comments = item.Comments;
                    model.PostId = item.Id;
                    model.User = item.User;
                    model.UserId = item.UserId;
                    model.Likes = item.Likes;
                    model.Description = item.Description;
                    if (_db.PostLikes.Any(x => x.UserId == user.Id && x.PostId == item.Id))
                    {
                        model.isLiked = true;
                    }
                    else
                    {
                        model.isLiked = false;
                    }
                    isPostLiked.Add(model);


                }
            }
            PostVM.Posts = isPostLiked;
            PostVM.PostLikes = _unitOfWork.PostLikes.GetAll();
            PostVM.User = user;
         
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
            var user = _userManager.Get();
            if(user.Id == id)
            {
               return RedirectToAction("Index", "Profile");
            }
            DetailsViewModel detailsVM = new DetailsViewModel();
            detailsVM.PostLikes = _unitOfWork.PostLikes.GetAll();
            var follow = _unitOfWork.Follow.GetAll();
            var post = _unitOfWork.Post.GetAll(includeProperties: "PostLikes,User,Comments");
            List<isPostLikedViewModel> isPostLiked = new List<isPostLikedViewModel>();
            foreach (var item in post)
            {

                isPostLikedViewModel model = new isPostLikedViewModel();
                model.PostLikes = item.PostLikes;
                model.ImageUrl = item.ImageUrl;
                model.Comments = item.Comments;
                model.PostId = item.Id;
                model.User = item.User;
                model.Likes = item.Likes;
                model.Description = item.Description;
                if (_db.PostLikes.Any(x => x.UserId == user.Id && x.PostId == item.Id))
                {
                    model.isLiked = true;
                }
                else
                {
                    model.isLiked = false;
                }
                isPostLiked.Add(model);


            }
            detailsVM.Posts = isPostLiked;
            detailsVM.User = _unitOfWork.User.GetFirstOrDefault(x => x.Id == id);
            foreach (var item in follow)
            {
                if (user.Id == item.UserId && id == item.FollowingUserId)
                {
                   detailsVM.isFollowing = true;
                }
                else
                {
                    detailsVM.isFollowing = false;
                }
            }
            if (detailsVM.Posts != null)
            {
                return View(detailsVM);
            }
            return View();
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
            return RedirectToAction("Privacy");
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

        [HttpPost]
        public JsonResult LikePost(int PostId)
        {
            var user = _userManager.Get();
            if (!_db.PostLikes.Any(x => x.PostId == PostId && x.UserId == user.Id))
            {
                var post = _unitOfWork.Post.GetFirstOrDefault(x => x.Id == PostId);
                PostLikes PostLikes = new PostLikes();
                PostLikes.PostId = PostId;
                PostLikes.UserId = user.Id;
                post.Likes++;
                _unitOfWork.PostLikes.Add(PostLikes);
                _unitOfWork.Post.Update(post);
                _unitOfWork.Save();
                return new JsonResult(Ok(true));
            }
            else if (_db.PostLikes.Any(x => x.PostId == PostId && x.UserId == user.Id))
            {
                var postlikes = _unitOfWork.PostLikes.GetFirstOrDefault(x => x.PostId == PostId && x.UserId == user.Id);
                var post = _unitOfWork.Post.GetFirstOrDefault(x => x.Id == PostId);
                post.Likes--;
                _unitOfWork.PostLikes.Remove(postlikes);
                _unitOfWork.Post.Update(post);
                _unitOfWork.Save();
                return new JsonResult(Ok(false));
            }
            else return null;

        }
      
        [HttpPost]
        public IActionResult Search(string search)
        {
            UserSearch userSearch = new UserSearch();
            var user = _userManager.Get();
            userSearch.Users = _db.Users.Where(x => x.Username == search).ToList();
            var follow = _unitOfWork.Follow.GetAll();
            foreach (var item in follow)
            {
                foreach (var obj in userSearch.Users) {
                    if (user.Id == item.UserId &&  obj.Id == item.FollowingUserId)
                    {
                        userSearch.isFollowing = true;
                    }
                    else
                    {
                        userSearch.isFollowing = false;
                    }
                }
            }
            return View("Search",userSearch);
        }
        public JsonResult Follow(int UserId,int FollowUserId)
        {
            if (!_db.Follows.Any(x => x.UserId == UserId && x.FollowingUserId == FollowUserId))
            {
                Follow follow = new Follow();
                follow.UserId = UserId;
                follow.FollowingUserId = FollowUserId;
                _unitOfWork.Follow.Add(follow);
                _unitOfWork.Save();
                return new JsonResult(Ok(true));
            }
            else
            {
                var follow = _unitOfWork.Follow.GetFirstOrDefault(x => x.UserId == UserId && x.FollowingUserId == FollowUserId);
                _unitOfWork.Follow.Remove(follow);
                _unitOfWork.Save();
                return new JsonResult(Ok(false));
            }
        }


    }
}