using System;
using System.Data.Entity.Migrations;
using SportShop.Models;
using SportShop.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        // GET: Products
        public ActionResult Index(string? name, string? description, int? above, int? below)
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                var products = from m in _context.Products select m;

                if (!String.IsNullOrEmpty(name))
                {
                    products = products.Where(p => p.Name.Contains(name));
                }

                if (!String.IsNullOrEmpty(description))
                {
                    products = products.Where(p => p.Description.Contains(description));
                }

                if (above != null)
                {
                    products = products.Where(p => p.Price >= above);
                }

                if (below != null)
                {
                    products = products.Where(p => p.Price <= below);
                }

                return View(products.ToList());
            }
            else
            {
                TempData["AdminErrorMessage"] = "Sorry, this page is for admins only. Log in now!";
                return Redirect("/Login");
            }
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Price,VideoUrl")]
            Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Price,VideoUrl")]
            Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Products.AddOrUpdate(product);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!ProductExists(product.Id))
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

            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}