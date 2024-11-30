// ASP.NET Core MVC - Company Registration Page and Login Page

// Controller: CompanyController.cs
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Buytopia.Models;

namespace Buytopia.Controllers  // Ensure namespace matches your project setup
{
    public class CompanyController : Controller
    {
        // Dummy in-memory data storage for registered companies
        private static List<CompanyRegistrationViewModel> _registeredCompanies = new List<CompanyRegistrationViewModel>();

        // GET: Company/Register
        public IActionResult Register()
        {
            return View("CompanyRegistration");
        }

        // POST: Company/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(CompanyRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save company details to the in-memory list (for demo purposes)
                _registeredCompanies.Add(model);

                // Redirect to the login page after successful registration
                return RedirectToAction("Login", "Company");
            }
            return View("CompanyRegistration", model);
        }

        // GET: Company/Login
        public IActionResult Login()
        {
            return View("CompanyLogin");
        }

        // POST: Company/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(CompanyLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authentication logic - check if the entered email and password match any registered company
                var company = _registeredCompanies.FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);
                if (company != null)
                {
                    // Redirect to company dashboard after successful login
                    return RedirectToAction("Companyhome", "Company");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View("CompanyLogin", model);
        }

        // GET: Company/Dashboard
        public IActionResult Companyhome()
        {
            return View("Companyhome");
        }

        // GET: Company/Logout
        public IActionResult Logout()
        {
            // Clear the session to log the company out
            HttpContext.Session.Clear();

            // Redirect to the login page after logout
            return RedirectToAction("Login", "Company");
        }
    }
}
