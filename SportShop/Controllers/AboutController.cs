using Microsoft.AspNetCore.Mvc;

namespace SportShop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}