using E_Commmerce.Models;
using E_Commmerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Displays a list of all roles
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.Select(role => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

            return View(roles);
        }

        // Handles role creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identityRole = new IdentityRole
            {
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // Handles role deletion
        public async Task<IActionResult> Delete(string id)
        {
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

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction(nameof(Index));
        }

        // Displays the edit form for a role
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        // Handles role updates
        [HttpPost]
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

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // Adds cart item count to ViewBag for all actions
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }
    }
}
