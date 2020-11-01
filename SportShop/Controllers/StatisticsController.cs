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
            var groupBy = context.OrderProducts.Join(context.Orders,
                op => op.OrderId,
                o => o.Id,
                (orderProducts, order) => new
                {
                    month = order.CreationDate.Month, quantity = orderProducts.Quantity,
                    productId = orderProducts.ProductId
                }
            ).Join(context.Products,
                arg => arg.productId,
                product => product.Id,
                (orders, products) => new
                {
                    month = orders.month,
                    quantity = orders.quantity,
                    price = products.Price
                }
            ).GroupBy(g => g.month).Select(g => new
            {
                Month = g.Key,
                Sum = g.Sum(c => c.quantity * c.price)
            });
            ;

            return JsonConvert.SerializeObject(groupBy.ToList());
        }

        // GET: Statistics/MostSellingProduct
        public string MostSellingProduct()
        {
            var groupBy = context.OrderProducts.Join(
                    context.Products,
                    orderProduct => orderProduct.ProductId,
                    product => product.Id,
                    (orderProduct, product) => new
                    {
                        productId = product.Id,
                        amount = orderProduct.Quantity
                    }
                ).GroupBy(orderProduct => orderProduct.productId)
                .Select(x => new {ProductId = x.Key, Quantity = x.Sum(a => a.amount)})
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