using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Data.Entity;
using SportShop.Data;

namespace SportShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new DataInitializer());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}