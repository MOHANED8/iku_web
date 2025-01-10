<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
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

=======
﻿using Microsoft.AspNetCore.Mvc; // For ControllerBase class and MVC functionality
using E_Commmerce.Models; // Models for the shopping cart, cart items, and payment history
using E_Commmerce.IReposatory; // Interfaces for product and payment history repositories
using Microsoft.AspNetCore.Mvc.Filters; // For implementing custom action filters

namespace E_Commmerce.Controllers
{
    [Route("Cart")] // Base route for the CartController
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository; // Repository for managing product data
        private readonly IPaymentHistoryRepository _paymentHistoryRepository; // Repository for payment history

        // Constructor to initialize repositories
>>>>>>> 69e884f (Initial project upload)
        public CartController(IProductRepository productRepository, IPaymentHistoryRepository paymentHistoryRepository)
        {
            _productRepository = productRepository;
            _paymentHistoryRepository = paymentHistoryRepository;
        }

<<<<<<< HEAD
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
=======
        // Displays the shopping cart
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var cart = GetCart(); // Retrieve the current cart from the session
            return View(cart); // Render the cart view
        }

        // Adds a product to the cart
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int productId)
        {
            var cart = GetCart(); // Retrieve the current cart
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId); // Check if the product is already in the cart

            if (existingItem != null)
            {
                existingItem.Quantity++; // Increment quantity if product exists
            }
            else
            {
                var product = _productRepository.GetbyId(productId); // Get product details
                if (product != null)
                {
                    cart.Items.Add(new CartItem // Add a new item to the cart
>>>>>>> 69e884f (Initial project upload)
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1,
<<<<<<< HEAD
                        ImageName = product.ImageName ?? "Default.png"
=======
                        ImageName = product.ImageName ?? "Default.png" // Use default image if not provided
>>>>>>> 69e884f (Initial project upload)
                    });
                }
            }

<<<<<<< HEAD
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
=======
            SaveCart(cart); // Save updated cart back to the session
            return RedirectToAction("Index"); // Redirect to the cart page
        }

        // Removes a product from the cart
        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart(); // Retrieve the current cart
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId); // Find the product in the cart

            if (existingItem != null)
            {
                cart.Items.Remove(existingItem); // Remove the product if it exists
            }

            SaveCart(cart); // Save updated cart back to the session
            return RedirectToAction("Index"); // Redirect to the cart page
        }

        // Displays the payment page
        [HttpGet("Payment")]
        public IActionResult Payment()
        {
            var cart = GetCart(); // Retrieve the current cart

            if (cart == null || cart.Items.Count == 0) // Check if the cart is empty
            {
                TempData["Error"] = "Your cart is empty. Add items before proceeding to payment."; // Set error message
                return RedirectToAction("Index"); // Redirect to cart page
            }

            return View(cart); // Render the payment view with the cart details
        }

        // Processes the payment
        [HttpPost("ProcessPayment")]
        public IActionResult ProcessPayment()
        {
            var cart = GetCart(); // Retrieve the current cart

            if (cart == null || cart.Items.Count == 0) // Check if the cart is empty
            {
                TempData["Error"] = "Your cart is empty."; // Set error message
                return RedirectToAction("Index"); // Redirect to cart page
            }

            // Create a payment history record
            var paymentHistory = new PaymentHistory
            {
                PaymentDate = DateTime.Now, // Current date and time
                TotalPrice = cart.TotalPrice, // Total price of the cart
                Items = cart.Items.Select(item => new PaymentItem // Map cart items to payment items
>>>>>>> 69e884f (Initial project upload)
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Save the payment history
            _paymentHistoryRepository.SavePayment(paymentHistory);

<<<<<<< HEAD
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

=======
            // Verify that the payment history is saved correctly
            var savedPayment = _paymentHistoryRepository.GetAllPayments()
                .FirstOrDefault(ph => ph.PaymentDate == paymentHistory.PaymentDate && ph.TotalPrice == paymentHistory.TotalPrice);

            if (savedPayment == null) // Check if payment was not saved
            {
                TempData["Error"] = "Payment could not be processed. Please try again."; // Set error message
                return RedirectToAction("Index"); // Redirect to cart page
            }

            HttpContext.Session.Remove("Cart"); // Clear the cart session

            return RedirectToAction("PaymentConfirmation"); // Redirect to payment confirmation page
        }

        // Displays the payment confirmation page
        [HttpGet("PaymentConfirmation")]
        public IActionResult PaymentConfirmation()
        {
            return View(); // Render the payment confirmation view
        }

        // Retrieves the shopping cart from the session or creates a new one
>>>>>>> 69e884f (Initial project upload)
        private ShoppingCart GetCart()
        {
            return HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
        }

<<<<<<< HEAD
=======
        // Saves the shopping cart to the session
>>>>>>> 69e884f (Initial project upload)
        private void SaveCart(ShoppingCart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

<<<<<<< HEAD
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
=======
        // Sets the cart item count in ViewBag before executing an action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart(); // Get the current cart
            ViewBag.CartItemCount = cart.TotalQuantity; // Set the total item count in ViewBag
            base.OnActionExecuting(context); // Call the base method
>>>>>>> 69e884f (Initial project upload)
        }
    }
}
