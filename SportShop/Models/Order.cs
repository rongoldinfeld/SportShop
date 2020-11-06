using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SportShop.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Customer")] public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }

        public double Sum
        {
            get
            {
                if (this.OrderProducts != null)
                {
                    var calculatedOrder = OrderProducts.Where(o => o.OrderId == this.Id).GroupBy(o => o.OrderId).Select(
                        x => new
                        {
                            Id = x.Key,
                            Sum = x.Sum(a => a.Product.Price * a.Quantity)
                        }).ToList()[0];

                    return calculatedOrder.Sum;
                }
                else
                {
                    return 0;
                }
            }
            private set { }
        }

        public Customer Customer { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}