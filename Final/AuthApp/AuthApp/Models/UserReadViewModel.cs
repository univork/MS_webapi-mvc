using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class UserReadViewModel: UserViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
