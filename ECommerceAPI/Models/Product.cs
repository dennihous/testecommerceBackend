using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}