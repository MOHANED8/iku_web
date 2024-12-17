using System;
using System.Collections.Generic;

namespace E_Commmerce.Models
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<PaymentItem> Items { get; set; } = new List<PaymentItem>();
    }

    public class PaymentItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
