using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Price { get; set; }

        [Required] public string VideoUrl { get; set; }
        [Required] public string ImageName { get; set; }
    }
}