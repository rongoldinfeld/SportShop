using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.Models
{
    public class OrderProduct
    {
        [Key]
        [ForeignKey("Order")]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        [Key]
        [ForeignKey("Product")]
        [Column(Order = 2)]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}