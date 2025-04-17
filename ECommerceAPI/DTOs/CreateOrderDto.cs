using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new();
    }

    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity  { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}