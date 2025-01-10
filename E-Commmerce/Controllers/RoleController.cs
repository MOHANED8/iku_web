<<<<<<< HEAD
﻿using E_Commmerce.Models;
using E_Commmerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
=======
﻿using E_Commmerce.Models; // Models for data entities such as roles
using E_Commmerce.ViewModels; // ViewModel classes for role representation
using Microsoft.AspNetCore.Authorization; // For authorization attributes
using Microsoft.AspNetCore.Identity; // Identity framework for role management
using Microsoft.AspNetCore.Mvc; // For controller functionalities
using Microsoft.AspNetCore.Mvc.Filters; // For action filters
>>>>>>> 69e884f (Initial project upload)

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))] // Restricts access to admin users only
    public class RoleController : Controller
    {
<<<<<<< HEAD
        private readonly RoleManager<IdentityRole> _roleManager;

=======
        private readonly RoleManager<IdentityRole> _roleManager; // Manages roles in the Identity framework

        // Constructor to initialize dependencies
>>>>>>> 69e884f (Initial project upload)
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Displays a list of all roles
        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
=======
            // Fetches all roles and maps them to the RoleViewModel
>>>>>>> 69e884f (Initial project upload)
            var roles = _roleManager.Roles.Select(role => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

<<<<<<< HEAD
=======
            // Passes the list of roles to the view
>>>>>>> 69e884f (Initial project upload)
            return View(roles);
        }

        // Handles role creation
        [HttpPost]
<<<<<<< HEAD
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
=======
        [ValidateAntiForgeryToken] // Ensures the request is from a valid source
        public async Task<IActionResult> Index(RoleViewModel model)
        {
            if (!ModelState.IsValid) // Validates the input model
            {
                return View(model); // Re-renders the view with validation errors
>>>>>>> 69e884f (Initial project upload)
            }

            var identityRole = new IdentityRole
            {
<<<<<<< HEAD
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

=======
                Name = model.Name // Sets the role name
            };

            // Creates the role using the Identity framework
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the role list if successful
            }

            // Adds any errors to ModelState for display
>>>>>>> 69e884f (Initial project upload)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

<<<<<<< HEAD
            return View(model);
=======
            return View(model); // Re-renders the view with errors
>>>>>>> 69e884f (Initial project upload)
        }

        // Handles role deletion
        public async Task<IActionResult> Delete(string id)
        {
<<<<<<< HEAD
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

=======
            // Finds the role by ID
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(); // Returns 404 if the role is not found
            }

            // Deletes the role
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the role list if successful
            }

            // Adds any errors to ModelState for display
>>>>>>> 69e884f (Initial project upload)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

<<<<<<< HEAD
            return RedirectToAction(nameof(Index));
=======
            return RedirectToAction(nameof(Index)); // Redirects back to the role list with errors
>>>>>>> 69e884f (Initial project upload)
        }

        // Displays the edit form for a role
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
<<<<<<< HEAD
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

=======
            // Finds the role by ID
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(); // Returns 404 if the role is not found
            }

            // Maps the role data to a RoleViewModel
>>>>>>> 69e884f (Initial project upload)
            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

<<<<<<< HEAD
            return View(model);
=======
            return View(model); // Renders the edit view with the model
>>>>>>> 69e884f (Initial project upload)
        }

        // Handles role updates
        [HttpPost]
<<<<<<< HEAD
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

=======
        [ValidateAntiForgeryToken] // Ensures the request is from a valid source
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (!ModelState.IsValid) // Validates the input model
            {
                return View(model); // Re-renders the view with validation errors
            }

            // Finds the role by ID
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound(); // Returns 404 if the role is not found
            }

            role.Name = model.Name; // Updates the role name
            var result = await _roleManager.UpdateAsync(role); // Updates the role in the Identity framework

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the role list if successful
            }

            // Adds any errors to ModelState for display
>>>>>>> 69e884f (Initial project upload)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

<<<<<<< HEAD
            return View(model);
=======
            return View(model); // Re-renders the view with errors
>>>>>>> 69e884f (Initial project upload)
        }

        // Adds cart item count to ViewBag for all actions
        public override void OnActionExecuting(ActionExecutingContext context)
        {
<<<<<<< HEAD
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
=======
            // Retrieves the shopping cart from the session or initializes a new one
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();

            // Sets the total cart item count in ViewBag
            ViewBag.CartItemCount = cart.TotalQuantity;

            base.OnActionExecuting(context); // Calls the base method
>>>>>>> 69e884f (Initial project upload)
        }
    }
}
