using Buytopia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class DeliveryController : Controller
{
    // Dummy in-memory data storage for registered delivery users
    private static List<DeliveryRegistrationViewModel> _registeredDeliveryUsers = new List<DeliveryRegistrationViewModel>();

    // GET: Delivery/Register
    public IActionResult Register()
    {
        return View("DeliveryRegistration");
    }

    // POST: Delivery/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(DeliveryRegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Save delivery user details to the in-memory list (for demo purposes)
            _registeredDeliveryUsers.Add(model);

            // Redirect to the login page after successful registration
            return RedirectToAction("Login", "Delivery");
        }
        return View("DeliveryRegistration", model);
    }

    // GET: Delivery/Login
    public IActionResult Login()
    {
        return View("DeliveryLogin");
    }

    // POST: Delivery/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(DeliveryLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Authentication logic - check if the entered email and password match any registered delivery user
            var deliveryUser = _registeredDeliveryUsers.FirstOrDefault(d => d.Email == model.Email && d.Password == model.Password);
            if (deliveryUser != null)
            {
                // Redirect to delivery dashboard after successful login
                return RedirectToAction("DeliveryHome", "Delivery");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View("DeliveryLogin", model);
    }

    // GET: Delivery/Dashboard
    public IActionResult DeliveryHome()
    {
        return View("DeliveryHome");
    }

    // GET: Delivery/Logout
    public IActionResult Logout()
    {
        // Clear the session to log the delivery user out
        HttpContext.Session.Clear();

        // Redirect to the login page after logout
        return RedirectToAction("Login", "Delivery");
    }
}
