using System;
using System.Data.Entity.Migrations;
using SportShop.Models;
using SportShop.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SportShop.Controllers
{
    [SessionCheck]
    public class ProductsController : Controller
    {
        private readonly SportShopContext _context = new SportShopContext();
        private IHostEnvironment Environment;

        public ProductsController(IHostEnvironment _enviorment)
        {
            this.Environment = _enviorment;
        }

        // GET: Products
        public ActionResult Index(string? name, string? description, int? above, int? below)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,VideoUrl")]
            Product product, bool isFacebookShare = false)
        {
            if (ModelState.IsValid)
            {
                // Generate random file locally
                var image = Request.Form.Files[0];
                if (image.Length > 0)
                {
                    var ImageExtension = image.FileName.Split('.')[1];
                    // generate unique name for each uploaded image so it won't collide with other users uploaded images
                    var ImageName = Guid.NewGuid().ToString() + '.' + ImageExtension;

                    product.ImageName = ImageName;
                    var imagePath = Path.Combine(this.Environment.ContentRootPath, "wwwroot\\images",
                        product.ImageName);

                    // Creates the images in ImagePath
                    using (var stream = System.IO.File.Create(imagePath))
                    {
                        await image.CopyToAsync(stream);
                    }
                }

                _context.Products.Add(product);
                _context.SaveChanges();

                if (isFacebookShare)
                {
                    PostProductOnFacebook(product);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public static WebResponse PostProductOnFacebook(Product product)
        {
            string message =
                $"A new product:  \"{product.Name}\" is now available in our stores for only {product.Price} !!! \n SHOP NOW at sport shop the best prices in the whole universe.";

            string url =
                $"https://graph.facebook.com/111543207406214/feed?message={message}&access_token=EAAL1cBr10TQBAAmOEAryA5XmEcmC9Vg5foA8lvJBf3QAlytz00rRpKgx7ZBhpvaTgJZACziMNTAKF5mx7ZAGh0XG7DIky2uhA6uCF4ZA2goXvhkqYEkGvUot6E0rZBpUXPy1eUyAlqpwxpIqTSw8zTb6ih0WmA6m3V1x9wutZASwZDZD";

            WebRequest httpRequest = WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
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
        // The image name in the parameters is the OLD image name
        async public Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,VideoUrl,ImageName")]
            Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // If user uploaded a new image
                if (Request.Form.Files.Count > 0)
                {
                    // Get the image content
                    var image = Request.Form.Files[0];

                    // Check if the image provided is not an empty file
                    if (image.Length > 0)
                    {
                        var ImageExtension = image.FileName.Split('.')[1];
                        // generate unique name for each uploaded image so it won't collide with other users uploaded images
                        var ImageName = Guid.NewGuid().ToString() + '.' + ImageExtension;

                        product.ImageName = ImageName;
                        var imagePath = Path.Combine(this.Environment.ContentRootPath, "wwwroot\\images",
                            product.ImageName);
                        using (var stream = System.IO.File.Create(imagePath))
                        {
                            await image.CopyToAsync(stream);
                        }
                    }
                }

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