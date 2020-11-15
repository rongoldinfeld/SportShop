using Microsoft.AspNetCore.Mvc;

namespace SportShop.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}