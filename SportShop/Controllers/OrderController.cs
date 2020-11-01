﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public  IActionResult Index(string catalogSearch)
        {
            var products = from m in _context.Products
                         select m;

            if (!string.IsNullOrEmpty(catalogSearch))
            {
                products = products.Where(product => product.Name.Contains(catalogSearch));
            }

            ViewBag.Products =  products.ToList();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public IActionResult Create([Bind("OrderProducts")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CustomerId = int.Parse(HttpContext.Session.GetString("UserId"));
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

            return JsonConvert.SerializeObject(
                products,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}