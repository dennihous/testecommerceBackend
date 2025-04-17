using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; }  = null!;
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}