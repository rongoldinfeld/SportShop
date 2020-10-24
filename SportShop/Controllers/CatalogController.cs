using Microsoft.AspNetCore.Mvc;
using SportShop.Data;
using System.Linq;

namespace SportShop.Controllers
{
    public class CatalogController : Controller
    {
        
        private readonly SportShopContext _context = new SportShopContext();

        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.ToList();
            
            return View();
        }
    }
}
