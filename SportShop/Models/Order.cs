using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }

        public Customer Customer { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderDbContext : SportDbContext
    {
    }
}
