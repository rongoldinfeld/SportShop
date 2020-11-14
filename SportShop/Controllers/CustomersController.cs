using Microsoft.AspNetCore.Mvc;
using SportShop.Data;
using SportShop.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SportShop.Controllers
{
    [SessionCheck]
    public class CustomersController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        // GET: Customers
        public IActionResult Index(string searchString, string userName, bool? isAdmin)
        {
            var customers = from s in _context.Customers select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                                 || s.FirstName.Contains(searchString));
            }

            if (isAdmin != null)
            {
                customers = customers.Where(c => c.IsAdmin == isAdmin);
            }

            if (!String.IsNullOrEmpty(userName))
            {
                customers = customers.Where(c => c.UserName.Contains(userName));
            }


            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id,FirstName,LastName,BirthDate,UserName,Password,Phone,Address,City,ZipCode,IsAdmin")]
            Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id,FirstName,LastName,BirthDate,UserName,Password,Phone,Address,City,ZipCode,IsAdmin")]
            Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.AddOrUpdate(customer);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers
                .FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }


        // GET: Customers/Filter
        public IActionResult Filter(
            [Bind("Id,FirstName,LastName,BirthDate,UserName,Password,Phone,Address,City,ZipCode,IsAdmin")]
            Customer customer)
        {
            // var customers = _context.Customers;
            //
            // if (!String.IsNullOrEmpty(firstName))
            // {
            //     return View(customers.Where(c => c.FirstName.Contains(firstName)).ToList());
            // }

            return View(_context.Customers.ToList());
        }
    }
}