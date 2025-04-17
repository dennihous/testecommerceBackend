using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name        { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public decimal Price       { get; set; }
        [Required]
        public int Stock           { get; set; }
    }

    public class UpdateProductDto : CreateProductDto
    {
        [Required]
        public int Id { get; set; }
    }
}