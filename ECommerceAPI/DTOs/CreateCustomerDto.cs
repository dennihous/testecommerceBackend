using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dtos
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name  { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class UpdateCustomerDto
    {
        [Required]
        public string Name  { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}
