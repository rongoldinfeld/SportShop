using Microsoft.AspNetCore.Mvc;
using SportShop.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Controllers
{
    public class CatalogController : Controller
    {
        
        private readonly SportShopContext _context = new SportShopContext();

        public  IActionResult Index(string catalogSearch)
        {
            var products = from m in _context.Products
                         select m;

            if (!String.IsNullOrEmpty(catalogSearch))
            {
                products = products.Where(product => product.Name.Contains(catalogSearch));
            }

            ViewBag.Products =  products.ToList();
            return View();
        }
    }
}
