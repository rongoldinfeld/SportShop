using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class HomeController : Controller
    {

        SportShopContext context = new SportShopContext();
        public IActionResult Index()
        {
            return View();
        }
    }
}
