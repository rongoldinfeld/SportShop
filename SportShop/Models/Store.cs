using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public double Lat { get; set; }
        [Required] public double Lng { get; set; }
    }
}
