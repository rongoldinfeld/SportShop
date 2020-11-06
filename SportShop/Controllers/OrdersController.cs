using System;
using System.Data.Entity.Migrations;
using SportShop.Models;
using SportShop.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace SportShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        // GET: Orders
        public ActionResult Index(string? name, string? description, int? above, int? below)
        {
            if (HttpContext.Session.GetInt32("User") != null)
            {

                int customerId = (int)HttpContext.Session.GetInt32("User");

                var orders = from m in _context.Orders where m.CustomerId == customerId select m ;

                // if (!String.IsNullOrEmpty(name))
                // {
                //     orders = orders.Where(p => p.Name.Contains(name));
                // }
                //
                // if (!String.IsNullOrEmpty(description))
                // {
                //     orders = orders.Where(p => p.Description.Contains(description));
                // }

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

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
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

        // GET: Orders/Delete/5
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

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}