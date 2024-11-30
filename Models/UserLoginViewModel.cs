using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
