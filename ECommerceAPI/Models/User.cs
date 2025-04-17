using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }

    public class UserRegisterModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}