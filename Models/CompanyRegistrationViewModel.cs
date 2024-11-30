
using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class CompanyRegistrationViewModel
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Physical Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Business Registration Number")]
        public string BusinessRegistrationNumber { get; set; }

        [Display(Name = "Website (Optional)")]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact Person Phone Number")]
        public string ContactPersonPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Industry Type")]
        public string IndustryType { get; set; }

        [Required]
        [Display(Name = "Agree to Terms and Conditions")]
        public bool AgreeToTerms { get; set; }
    }

}
