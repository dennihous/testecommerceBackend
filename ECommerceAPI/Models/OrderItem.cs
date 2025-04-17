using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}