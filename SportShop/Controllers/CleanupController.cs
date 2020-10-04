using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class CleanupController : Controller
    {
        private SportShopContext _context;

        public CleanupController(SportShopContext context)
        {
            this._context = context;
        }


        // GET: /CleanUp/ 
        public string Index()
        {
            DataInitializer.CleanUp(this._context);
            return "System entities cleaned up...";
        }

        // 
        // GET: /CleanUp/Welcome/ 
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
