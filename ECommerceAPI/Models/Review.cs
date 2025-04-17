using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
        [Range(1,5)]
        public int Rating { get; set; }
    }
}