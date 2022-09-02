using Microsoft.AspNetCore.Mvc;

namespace Gymbuddy.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
