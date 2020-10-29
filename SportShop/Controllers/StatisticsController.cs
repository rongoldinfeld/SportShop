using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportShop.Controllers
{
    public class StatisticsController : Controller
    {
        SportShopContext context = new SportShopContext();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return View();
            }
            else
            {
                TempData["AdminErrorMessage"] = "Sorry, this page is for admins only. Log in now!";
                return Redirect("/Login");
            }
        }

        // GET: Statistics/SumPurchasesPerMonth
        public string SumPurchasesPerMonth()
        {
            var groupBy = context.OrderProducts.GroupBy(order => order.Order.CreationDate.Month).Select(g => new
            {
                Month = g.Key,
                Sum = g.Sum(c => c.Quantity * c.Product.Price)
            });
            return JsonConvert.SerializeObject(groupBy.ToList());
        }

        // GET: Statistics/MostSellingProduct
        public string MostSellingProduct()
        {
            var groupBy = context.OrderProducts.GroupBy(orderProduct => orderProduct.Product.Id)
                .Select(x => new {ProductId = x.Key, Quantity = x.Sum(a => a.Quantity)})
                .OrderByDescending(x => x.Quantity)
                .ToList();
            return JsonConvert.SerializeObject(groupBy.ToList());
        }

        // GET: Statistics/ProductIdAndName
        public string ProductIdAndName()
        {
            var products = context.Products.Select(x => new
            {
                id = x.Id,
                name = x.Name
            }).ToList();

            return JsonConvert.SerializeObject(products);
        }
    }
}