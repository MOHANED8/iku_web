// Controller: HomeController.cs
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Buytopia.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Buytopia.Controllers  // Ensure namespace matches your project setup
{
    public class HomeController : Controller
    {
        // Dummy in-memory data storage for registered users
        private static List<UserRegistrationViewModel> _registeredUsers = new List<UserRegistrationViewModel>();

        // GET: Home/Register
        public IActionResult Register()
        {
            return View("UserRegistration");
        }

        // POST: Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save user details to the in-memory list (for demo purposes)
                _registeredUsers.Add(model);

                // Redirect to the login page after successful registration
                return RedirectToAction("Login", "Home");
            }
            return View("UserRegistration", model);
        }

        // GET: Home/Login
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authentication logic - check if the entered email and password match any registered user
                var user = _registeredUsers.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Redirect to user dashboard after successful login
                    return RedirectToAction("UserHome", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View("UserLogin", model);
        }

        // GET: Home/Dashboard
        public IActionResult UserHome()
        {
            return View("UserHome");
        }

        // POST: Home/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign the user out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page after logout
            return RedirectToAction("Login", "Home");
        }
    }
}