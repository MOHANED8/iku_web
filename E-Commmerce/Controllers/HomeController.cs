using E_Commmerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace E_Commmerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }

    }
}