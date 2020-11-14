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
            OrderProducts = new List<OrderProduct>();
        }
    }
}