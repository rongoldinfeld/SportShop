﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SportShop.Data;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SportShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();

        public IActionResult Index(string name, string? description, int? above, int? below)
        {
            var products = from m in _context.Products select m;

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(product => product.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(description))
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

            // In case no user is logged in - because we can't get recommendation
            ViewBag.RecProduct = -1;
            List<Product> productsList = products.ToList();
            if (HttpContext.Session.GetInt32("User") != null)
            {
                int? recProductid = recommendedProduct();
                if (recProductid != null)
                {
                    ViewBag.RecProduct = recProductid;

                    // Setting the recommanded item on the top of the list;
                    var recProductIndex = productsList.FindIndex(x => x.Id == recProductid);
                    if (recProductIndex >= 0)
                    {
                        var recProduct = productsList[recProductIndex];
                        productsList[recProductIndex] = productsList[0];
                        productsList[0] = recProduct;
                    }
                }
                else
                {
                    // Setting the first as default.
                    ViewBag.RecProduct = productsList[0].Id;
                }
            }

            ViewBag.Products = productsList;
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public IActionResult Create([Bind("OrderProducts, Sum")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CustomerId = (int) HttpContext.Session.GetInt32("User");
                order.CreationDate = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        //Order/Products
        public string Products(List<int> productsIds)
        {
            var products = _context.Products
                .Where(x => productsIds.Contains(x.Id))
                .ToList();

            // For the client-side to use normal json standards for example. Instead of using order.Price we use order.price
            return JsonConvert.SerializeObject(
                products,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        public int? recommendedProduct()
        {
            if (HttpContext.Session.GetInt32("User") != null)
            {
                int CustomerId = (int) HttpContext.Session.GetInt32("User");
                var customer = _context.Customers.SingleOrDefault(customer => customer.Id == CustomerId);
                int customerYearsOld = Convert.ToInt32(((DateTime.Now - customer.BirthDate).TotalDays / 365));
                int YEAR_RADIUS = 5;


                var mostBoughtProduct = _context.Orders.Join(_context.OrderProducts,
                        order => order.Id,
                        orderproduct => orderproduct.OrderId,
                        (order, orderproduct) => new
                        {
                            customerId = order.CustomerId,
                            orderId = order.Id,
                            productId = orderproduct.ProductId,
                            quantity = orderproduct.Quantity
                        }).Join(_context.Customers,
                        ordprod => ordprod.customerId,
                        customer => customer.Id,
                        (ordprod, customer) => new
                        {
                            yearsOld = DbFunctions.DiffYears(customer.BirthDate, DateTime.Now),
                            ordprod.customerId,
                            ordprod.orderId,
                            ordprod.productId,
                            ordprod.quantity
                        }).Where(x =>
                        (customerYearsOld - YEAR_RADIUS <= x.yearsOld && x.yearsOld <= customerYearsOld + YEAR_RADIUS))
                    .GroupBy(g => g.productId).Select(g => new
                        {ProductId = g.Key, Quantity = g.Select(x => x.quantity).Sum()})
                    .OrderByDescending(x => x.Quantity).FirstOrDefault();

                if (mostBoughtProduct != null)
                    return mostBoughtProduct.ProductId;
            }

            return null;
        }
    }
}