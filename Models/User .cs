using System;
using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class User
    {
        [Key] // Primary Key
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Default to current UTC time
    }
}

