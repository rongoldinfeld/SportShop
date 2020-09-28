using System;
using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class CustomerDbContext : SportDbContext
    {
    }
}
