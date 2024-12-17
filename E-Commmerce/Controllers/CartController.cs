using Microsoft.AspNetCore.Mvc;
using E_Commmerce.Models;
using E_Commmerce.IReposatory;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commmerce.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        public CartController(IProductRepository productRepository, IPaymentHistoryRepository paymentHistoryRepository)
        {
            _productRepository = productRepository;
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int productId)
        {
            var cart = GetCart();
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var product = _productRepository.GetbyId(productId);
                if (product != null)
                {
                    cart.Items.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1,
                        ImageName = product.ImageName ?? "Default.png"
                    });
                }
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                cart.Items.Remove(existingItem);
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpGet("Payment")]
        public IActionResult Payment()
        {
            var cart = GetCart();

            if (cart == null || cart.Items.Count == 0)
            {
                TempData["Error"] = "Your cart is empty. Add items before proceeding to payment.";
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        [HttpPost("ProcessPayment")]
        public IActionResult ProcessPayment()
        {
            var cart = GetCart();

            if (cart == null || cart.Items.Count == 0)
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            // Ödeme geçmişine kaydet
            var paymentHistory = new PaymentHistory
            {
                PaymentDate = DateTime.Now,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.Select(item => new PaymentItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Save the payment history
            _paymentHistoryRepository.SavePayment(paymentHistory);

            // Ensure the payment history is correctly saved
            var savedPayment = _paymentHistoryRepository.GetAllPayments()
                .FirstOrDefault(ph => ph.PaymentDate == paymentHistory.PaymentDate && ph.TotalPrice == paymentHistory.TotalPrice);

            if (savedPayment == null)
            {
                TempData["Error"] = "Payment could not be processed. Please try again.";
                return RedirectToAction("Index");
            }

            // Clear the cart
            HttpContext.Session.Remove("Cart");

            // Redirect to Payment Confirmation
            return RedirectToAction("PaymentConfirmation");
        }

        [HttpGet("PaymentConfirmation")]
        public IActionResult PaymentConfirmation()
        {
            return View();
        }

        private ShoppingCart GetCart()
        {
            return HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
        }

        private void SaveCart(ShoppingCart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }
    }
}
