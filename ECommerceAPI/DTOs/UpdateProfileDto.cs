using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dtos
{
    public class UpdateProfileDto
    {
        [Required] public string Name { get; set; } = null!;

        [Required] public string Email { get; set; } = null!;    }
}