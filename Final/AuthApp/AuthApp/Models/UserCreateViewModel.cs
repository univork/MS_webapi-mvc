using AuthApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    [PasswordValidationAttribute]
    public class UserCreateViewModel: UserViewModel
    {
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
