using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string VideoUrl { get; set; }
        public string Image { get; set; }
    }
}