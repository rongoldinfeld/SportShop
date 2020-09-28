using System;
using System.Collections.Generic;
using System.Linq;
using static SportShop.Models.OrderProduct;

namespace SportShop.Models
{
    public static class RawData
    {
        public static void Initialize()
        {
            InitialzieProducts();
            InitialzieCustomers();
            // InitialzieOrders();
            // InitialzieOrderProducts();
        }

        public static void CleanUp()
        {
            using (var context = new CustomerDbContext())
            {
                var all = context.Customers.ToList();
                context.Customers.RemoveRange(all);
                context.SaveChanges();
            }

            using (var context = new OrderDbContext())
            {
                var all = context.Orders.ToList();
                context.Orders.RemoveRange(all);
                context.SaveChanges();
            }

            using (var context = new ProductDbContext())
            {
                var all = context.Products.ToList();
                context.Products.RemoveRange(all);
                context.SaveChanges();
            }

            using (var context = new OrderProductDbContext())
            {
                var all = context.OrderProducts.ToList();
                context.OrderProducts.RemoveRange(all);
                context.SaveChanges();

                context.Database.Delete();
                context.SaveChanges();
            }
        }

        private static void InitialzieProducts()
        {
            using (var context = new ProductDbContext())
            {
                // Look for any Customerss.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new List<Product>() {
                        new Product
                        {
                            Id = 1,
                            Name = "First Product",
                            Description = "Super fancy",
                            VideoUrl="www.youtube.com/video-to-be-added-here",
                            Price = 23,
                        },
                         new Product
                        {
                            Id = 2,
                            Name = "Second Product",
                            Description = "Super fancy",
                            VideoUrl="www.youtube.com/video-to-be-added-here",
                            Price = 23,
                        },
                          new Product
                        {
                            Id = 3,
                            Name = "Third Product",
                            Description = "Super fancy",
                            VideoUrl="www.youtube.com/video-to-be-added-here",
                            Price = 23,
                        },
                         new Product
                            {
                            Id = 4,
                            Name = "Fourth Product",
                            Description = "Super fancy",
                            VideoUrl="www.youtube.com/video-to-be-added-here",
                            Price = 23,
                        },
                         new Product
                        {
                            Id = 5,
                            Name = "Fifth Product",
                            Description = "Super fancy",
                            VideoUrl="www.youtube.com/video-to-be-added-here",
                            Price = 23,
                        },

                     }
                );
                context.SaveChanges();
            }
        }

        private static void InitialzieCustomers()
        {
            using (var context = new CustomerDbContext())
            {
                // Look for any Customerss.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customers.AddRange(
                    new List<Customer>() {
                        new Customer
                        {
                            Id = 1,
                            FirstName = "Admin",
                            LastName = "Admin",
                            BirthDate = new DateTime(1995, 11, 14),
                            UserName = "admin",
                            Password = "admin",
                            Address = "admin",
                            City = "admin",
                            ZipCode = 1,
                            IsAdmin = true
                        },
                         new Customer
                         {
                             Id = 2,
                             FirstName = "Ron",
                             LastName = "Goldinfeld",
                             BirthDate = new DateTime(1995, 11, 14),
                             UserName = "ron",
                             Password = "ronchuk",
                             Address = "natur 3",
                             City = "rishon",
                             ZipCode = 123123,
                             IsAdmin = false
                         }
                     }
                );
                context.SaveChanges();
            }
        }


        private static void InitialzieOrders()
        {
            using (var context = new OrderDbContext())
            {
                // Look for any Customerss.
                if (context.Orders.Any())
                {
                    return;   // DB has been seeded
                }

                context.Orders.AddRange(
                    new List<Order>() {
                        new Order
                        {
                            Id = 1,
                            CustomerId = 2,
                            CreationDate = new DateTime(2018, 1, 1)
                        },
                         new Order
                         {
                            Id = 2,
                            CustomerId = 2,
                            CreationDate = new DateTime(2018, 1, 1)
                         }
                     }
                );
                context.SaveChanges();
            }
        }

        private static void InitialzieOrderProducts()
        {
            using (var context = new OrderProductDbContext())
            {
                // Look for any Customerss.
                if (context.OrderProducts.Any())
                {
                    return;   // DB has been seeded
                }

                context.OrderProducts.AddRange(
                    new List<OrderProduct>() {
                        new OrderProduct
                        {
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 5
                        },
                         new OrderProduct
                         {
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 3
                         }
                     }
                );
                context.SaveChanges();
            }
        }

    }
}