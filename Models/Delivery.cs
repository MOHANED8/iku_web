using System.ComponentModel.DataAnnotations;

namespace Buytopia.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Courier Name")]
        public string CourierName { get; set; }

        [Required]
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
