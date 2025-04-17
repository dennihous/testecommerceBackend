using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dtos
{
    public class CreateReviewDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId  { get; set; }
        [Required]
        public string Content { get; set; } = null!;
        [Range(1,5)]
        public int Rating     { get; set; }
    }
}