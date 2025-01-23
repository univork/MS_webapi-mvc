using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "First Name length can't be more than 50")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "Last Name length can't be more than 50")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal Number must be 11 digits long")]
        public string PersonalNumber { get; set; } = null!;

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

    }
}
