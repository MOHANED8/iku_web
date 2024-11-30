using System;
using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class Company
    {
        [Key] // Primary Key
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductType { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Default to current UTC time
    }
}
