namespace E_Commerce.ViewModels
{
    public class CartItemViewModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity; // Calculates the total for this item
    }
}
