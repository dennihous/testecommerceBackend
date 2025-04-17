using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}