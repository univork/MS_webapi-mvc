using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class UserLoginViewModel
    {
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
