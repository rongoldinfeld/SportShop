using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabrasSmoothie.Controllers
{
    public class RegisterController : Controller
    {
        private CustomerDbContext db = new CustomerDbContext();

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

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(customer);
        }
    }
}