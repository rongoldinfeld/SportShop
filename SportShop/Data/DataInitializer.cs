using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SportShop.Models;

namespace SportShop.Data
{
    public class DataInitializer: DropCreateDatabaseAlways<SportShopContext>
    {
        protected override void Seed(SportShopContext context)
        {
            InitialzieProducts(context);
            InitialzieCustomers(context);
            InitialzieOrders(context);
            InitialzieOrderProducts(context);
            InitialzieStores(context);
        }

        private static void InitialzieProducts(SportShopContext context)
        {
            // Look for any Customerss.
            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            context.Products.AddRange(
                new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "4 Kg Weights",
                        Description = "Super fancy",
                        VideoUrl = "https://www.youtube.com/watch?v=ln1PtuewNRA",
                        ImageName = "4-kg-weightsjpg.jpg",
                        Price = 60,
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Basketball",
                        Description = "Super fancy",
                        VideoUrl = "https://www.youtube.com/watch?v=WUHjBWmVRb0",
                        ImageName = "basketball.jpg",
                        Price = 50,
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Dri Fit Shirt",
                        Description = "Super fancy",
                        VideoUrl = "https://www.youtube.com/watch?v=dfPWKL1qazs",
                        ImageName = "dryfit-shirt.jpg",
                        Price = 79,
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "Running Pants",
                        Description = "Super fancy",
                        VideoUrl = "https://www.youtube.com/watch?v=RRXhyQ8u42g",
                        ImageName = "running-pants.jpg",
                        Price = 99,
                    },
                    new Product{
                        Id = 5,
                        Name = "Gym Shirt",
                        Description = "Super fancy",
                        VideoUrl = "https://www.youtube.com/watch?v=dfPWKL1qazs",
                        ImageName = "gym-shirt.jpg",
                        Price = 39,
                    },
                }
            );
            context.SaveChanges();
        }

        private static void InitialzieCustomers(SportShopContext context)
        {
            // Look for any Customerss.
            if (context.Customers.Any())
            {
                return; // DB has been seeded
            }

            context.Customers.AddRange(
                new List<Customer>()
                {
                    new Customer
                    {
                        Id = 1,
                        FirstName = "Ron",
                        LastName = "Goldinfeld",
                        BirthDate = new DateTime(1998, 11, 14),
                        UserName = "admin",
                        Password = "admin",
                        Phone = "052-1111111",
                        Address = "natur",
                        City = "rishon",
                        ZipCode = 18437,
                        IsAdmin = true
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "Tomer",
                        LastName = "Avisar",
                        BirthDate = new DateTime(1998, 10, 3),
                        UserName = "tomer",
                        Password = "avi",
                        Phone = "052-1111111",
                        Address = "harav",
                        City = "rishon",
                        ZipCode = 123123,
                        IsAdmin = false
                    },
                    new Customer
                    {
                        Id = 3,
                        FirstName = "Sapir",
                        LastName = "Muallem",
                        BirthDate = new DateTime(1998, 3, 6),
                        UserName = "sap",
                        Password = "1234",
                        Phone = "052-1111111",
                        Address = "habroshim",
                        City = "ramot hashavi,",
                        ZipCode = 459300,
                        IsAdmin = false
                    }
                }
            );
            context.SaveChanges();
        }


        private static void InitialzieOrders(SportShopContext context)
        {
            // Look for any Customerss.
            if (context.Orders.Any())
            {
                return; // DB has been seeded
            }

            context.Orders.AddRange(
                new List<Order>()
                {
                    new Order
                    {
                        Id = 1,
                        CustomerId = 2,
                        CreationDate = new DateTime(2018, 1, 1),
                        Sum = 100
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 2,
                        CreationDate = new DateTime(2018, 2, 1),
                        Sum = 200
                    },
                    new Order
                    {
                        Id = 3,
                        CustomerId = 3,
                        CreationDate = new DateTime(2018, 4, 2),
                        Sum = 300
                    },
                    new Order
                    {
                        Id = 4,
                        CustomerId = 2,
                        CreationDate = new DateTime(2020, 10, 23),
                        Sum = 400
                    }
                }
            );
            context.SaveChanges();
        }

        private static void InitialzieOrderProducts(SportShopContext context)
        {
            // Look for any Customerss.
            if (context.OrderProducts.Any())
            {
                return; // DB has been seeded
            }

            context.OrderProducts.AddRange(
                new List<OrderProduct>()
                {
                    new OrderProduct
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 5,
                    },
                    new OrderProduct
                    {
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 3
                    },
                    new OrderProduct
                    {
                        OrderId = 4,
                        ProductId = 4,
                        Quantity = 2
                    },
                    new OrderProduct
                    {
                        OrderId = 4,
                        ProductId = 1,
                        Quantity = 5
                    },
                    new OrderProduct
                    {
                        OrderId = 3,
                        ProductId = 1,
                        Quantity = 5
                    },
                }
            );
            context.SaveChanges();
        }



        private static void InitialzieStores(SportShopContext context)
        {
            // Look for any Customerss.
            if (context.Stores.Any())
            {
                return; // DB has been seeded
            }

            context.Stores.AddRange(
                new List<Store>()
                {
                    new Store
                    {
                        Id = 1,
                        Name = "G סניף מתחם",
                        Lat = 31.984065,
                        Lng = 34.770841
                    },
                    new Store
                    {
                        Id = 2,
                        Name = "סניף קניון עזריאלי",
                        Lat = 32.074600,
                        Lng = 34.791315
                    }
                }
            );
            context.SaveChanges();
        }
    }
}