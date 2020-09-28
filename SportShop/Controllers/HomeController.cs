using Microsoft.AspNetCore.Mvc;

namespace SportShop.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
