using System;
using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Customer
    {
        [Key] public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string  Phone { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public bool IsAdmin { get; set; }
    }
}