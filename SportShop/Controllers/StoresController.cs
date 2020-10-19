using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class StoresController : Controller
    {
        SportShopContext context = new SportShopContext();
        async public Task<IActionResult> Index()
        {
            List<Store> stores = await context.Stores.ToListAsync();
            return View(stores);
        }
    }
}
