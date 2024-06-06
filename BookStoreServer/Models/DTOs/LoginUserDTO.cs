using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
