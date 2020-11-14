using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using SportShop.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class OrdersHistoryController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        // GET: Orders
        public ActionResult Index(DateTime? start, DateTime? end, int? above, int? below)
        {
            var isAdmin = HttpContext.Session.GetString("Admin");
            var user = HttpContext.Session.GetInt32("User");

            if (user != null || isAdmin != null)
            {
                var customerId = HttpContext.Session.GetInt32("User");

                var orders = isAdmin != null ? GetProductWithDetails() : GetProductWithDetails().Where(order => order.CustomerId == customerId);

                if (start != null)
                {
                    orders = orders.Where(p => p.CreationDate > start);
                }

                if (end != null)
                {
                    orders = orders.Where(p => p.CreationDate < end);
                }

                if (above != null)
                {
                    orders = orders.Where(p => p.Sum >= above);
                }

                if (below != null)
                {
                    orders = orders.Where(p => p.Sum <= below);
                }


                return View(orders.ToList());
            }
            else
            {
                TempData["AdminErrorMessage"] = "Sorry, this page is for logged in users only. Log in now!";
                return Redirect("/Login");
            }
        }

        // GET: OrdersHistory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = GetProductWithDetails()
                .FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersHistory/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders
                .FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [SessionCheck]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = GetProductWithDetails().FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Sum,OrderProducts,ProductId,Quantity")]
            Order order)
        {
            if (ModelState.IsValid)
            {
                var orderToUpdate = _context.Orders.SingleOrDefault(x => x.Id == order.Id);

                if(orderToUpdate == null)
                {
                    return NotFound();
                }

                orderToUpdate.Sum = order.Sum;
                
                var orderProducts = order.OrderProducts.Where(x => x.Quantity > 0).ToList();

                _context.OrderProducts.AddOrUpdate(orderProducts.ToArray());
                _context.Orders.AddOrUpdate(orderToUpdate);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private IQueryable<Order> GetProductWithDetails()
        {
            return _context.Orders
                .Include(order => order.OrderProducts)
                .Include(order => order.Customer)
                .AsQueryable();
        }
    }
}