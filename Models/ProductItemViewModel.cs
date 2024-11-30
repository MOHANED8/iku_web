using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class ProductItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
