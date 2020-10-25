using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportShop.Data;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportShop.Controllers
{
    public class OrderController : Controller
    {
        
        private readonly SportShopContext _context = new SportShopContext();

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var orderViewModel = new Order();

            products.ForEach(x => orderViewModel.OrderProducts.Add(new OrderProduct
            {
                ProductId = x.Id,
                Quantity = 0
            }));

            ViewBag.Products = products;

            return View(orderViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderProducts")] Order order)
        {
            // TODO Add validations
            order.CreationDate = DateTime.Now;
            order.CustomerId = int.Parse(HttpContext.Session.GetString("UserId"));

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}