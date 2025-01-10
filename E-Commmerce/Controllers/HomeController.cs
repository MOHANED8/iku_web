<<<<<<< HEAD
﻿using E_Commmerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
=======
﻿using E_Commmerce.Models; // Models for data entities used in the application
using Microsoft.AspNetCore.Mvc; // Base class for controllers and MVC features
using Microsoft.AspNetCore.Mvc.Filters; // For implementing custom action filters
using System.Diagnostics; // Provides classes for interacting with processes and system diagnostics
>>>>>>> 69e884f (Initial project upload)

namespace E_Commmerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger to handle logging within the controller

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

<<<<<<< HEAD
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
=======
        // Handles rendering the error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Disables caching for the error view
>>>>>>> 69e884f (Initial project upload)
        public IActionResult Error()
        {
            // Creates an ErrorViewModel with the current activity ID or HTTP request trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

<<<<<<< HEAD
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }

=======
        // Executes before every action to set the cart item count in the ViewBag
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve the shopping cart from the session, or create a new one if it doesn't exist
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();

            // Set the total number of items in the cart to the ViewBag
            ViewBag.CartItemCount = cart.TotalQuantity;

            // Call the base method to ensure standard action execution continues
            base.OnActionExecuting(context);
        }
>>>>>>> 69e884f (Initial project upload)
    }
}
