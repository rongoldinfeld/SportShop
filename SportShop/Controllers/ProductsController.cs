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

namespace SportShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        // GET: Products
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return View(_context.Products.ToList());
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
            Product product, bool isFacebookShare = false)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                PostProductOnFacebook(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public static WebResponse PostProductOnFacebook(Product product)
        {
            //Data parameter Example
            string data = "";
            string message = $"A new product:  \"{product.Name}\" is now available in our stores for only {product.Price} !!! \n SHOP NOW at sport shop the best prices in the whole universe.";
            string url = $"https://graph.facebook.com/111543207406214/feed?message={message}&access_token=EAAL1cBr10TQBAAmOEAryA5XmEcmC9Vg5foA8lvJBf3QAlytz00rRpKgx7ZBhpvaTgJZACziMNTAKF5mx7ZAGh0XG7DIky2uhA6uCF4ZA2goXvhkqYEkGvUot6E0rZBpUXPy1eUyAlqpwxpIqTSw8zTb6ih0WmA6m3V1x9wutZASwZDZD";
            WebRequest httpRequest = WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = data.Length;

            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Close();

            return httpRequest.GetResponse();
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