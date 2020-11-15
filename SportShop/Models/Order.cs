using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Customer")] public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Customer Customer { get; set; }

        public int Sum { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {
            // We don't want users with out any orders to have this field equals to null so we initialize it to an empty list
            OrderProducts = new List<OrderProduct>();
        }
    }
}