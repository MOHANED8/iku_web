namespace E_Commmerce.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public int TotalQuantity => Items.Sum(item => item.Quantity);
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
