using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SabrasSmoothie.Controllers
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
                customer.IsAdmin = false;
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(customer);
        }
    }
}