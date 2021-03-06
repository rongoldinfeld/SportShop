using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SportShop.Models;

namespace SportShop.Data
{
    public class SportShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Store> Stores { get; set; }

        public SportShopContext():base("SportShopContext") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<OrderProduct>()
                .HasKey(e => new { e.OrderId, e.ProductId });

        }
    }
}