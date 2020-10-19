using SportShop.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportShop.Controllers
{
    public class LoginController : Controller
    {
        SportShopContext context = new SportShopContext();

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
            var user = context.Customers.SingleOrDefault(customer => customer.UserName == userName && customer.Password == password);
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
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetString("UserFullName", user.FirstName + " " + user.LastName);
            }

            return Redirect("/Home/");
        }
    }
}
