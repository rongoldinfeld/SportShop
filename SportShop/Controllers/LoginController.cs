using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SportShop.Controllers
{
    public class LoginController : Controller
    {
        private CustomerDbContext _Customer = new CustomerDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            this.HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string userName, string password)
        {
            var user = _Customer.Customers.SingleOrDefault(customer => customer.UserName == userName && customer.Password == password);
            if (user == null)
            {
                ViewBag.Message = "Invalid username or password";
                return View();
            }
            else if (user.IsAdmin)
            {

                HttpContext.Session.SetString("Admin", "true");
            }
            else
            {

                HttpContext.Session.SetString("UserId", "user.Id");
            }

            return Redirect("/Home/");
        }
    }
}
