using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Data;
using System.Linq;

namespace SportShop.Controllers
{
    public class RegisterController : Controller
    {
        SportShopContext context = new SportShopContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Register/Create
        public ActionResult Success()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("Id,FirstName,LastName,BirthDate,UserName,Password,Address,City,ZipCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var userNameFound = context.Customers.Count(c => c.UserName == customer.UserName);
                if (userNameFound > 0)
                {
                    ModelState.AddModelError("UserNameExists", "User name like this already exists");
                }
                else
                {
                    customer.IsAdmin = false;
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    return RedirectToAction("Success");
                }
            }

            return View(customer);
        }
    }
}