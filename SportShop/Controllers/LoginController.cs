using SportShop.Data;
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
            if (TempData.ContainsKey("AdminErrorMessage"))
            {
                ViewBag.Message = TempData["AdminErrorMessage"] as string;
            }

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string userName, string password)
        {
            var user = context.Customers.SingleOrDefault(customer =>
                customer.UserName == userName && customer.Password == password);
            if (user == null)
            {
                ViewBag.Message = "Invalid username or password";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("UserFullName", user.FirstName + " " + user.LastName);
                if (user.IsAdmin)
                {
                    HttpContext.Session.SetString("Admin", "true");
                }
                else
                {
                    HttpContext.Session.SetInt32("User", user.Id);
                }
            }


            return Redirect("/Home/");
        }
    }
}