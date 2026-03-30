using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}