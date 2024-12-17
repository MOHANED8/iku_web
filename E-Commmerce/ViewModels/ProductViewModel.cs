using E_Commmerce.CustomValidation;
using E_Commmerce.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commmerce.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; } = 0.0m;

        [MinLength(3), MaxLength(500)]
        [StringLength(500)]
        [NotEqual(ErrorMessage = "Description must not contain restricted words.")]
        public string Description { get; set; } = string.Empty;

        public Category? Category { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public UpdateImageViewModel? ImageViewModel { get; set; }
    }
}
