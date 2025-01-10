<<<<<<< HEAD
﻿using E_Commmerce.Helper;
using E_Commmerce.Models;
using E_Commmerce.ViewModels;
using E_Commmerce.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mail;

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AccountController : Controller
    {
        private const string DefaultImageName = "Default.jpg";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
=======
﻿using E_Commmerce.Helper; // Helper methods or utilities specific to the application
using E_Commmerce.Models; // Models representing data entities
using E_Commmerce.ViewModels; // General ViewModel classes for the application
using E_Commmerce.ViewModels.User; // ViewModel classes specific to the User entity
using Microsoft.AspNetCore.Authorization; // For authorization attributes
using Microsoft.AspNetCore.Identity; // Identity framework for user management
using Microsoft.AspNetCore.Mvc; // Base class for controllers and MVC features
using Microsoft.AspNetCore.Mvc.Filters; // For implementing custom filters
using System.Net.Mail; // To extract username from email addresses

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))] // Restricts access to admin users only
    public class AccountController : Controller
    {
        private const string DefaultImageName = "Default.jpg"; // Default user profile image
        private readonly UserManager<ApplicationUser> _userManager; // Manages user data
        private readonly SignInManager<ApplicationUser> _signInManager; // Handles user sign-in operations
        private readonly IWebHostEnvironment _webHostEnvironment; // Provides web hosting environment details
>>>>>>> 69e884f (Initial project upload)

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

<<<<<<< HEAD
=======
        // Displays a paginated list of users
>>>>>>> 69e884f (Initial project upload)
        public async Task<IActionResult> Index(int? PageNumber)
        {
            var users = _userManager.Users.ToList()
                .Select(async u => new UserViewModel
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = (await _userManager.GetRolesAsync(u)).FirstOrDefault() ?? Roles.User.ToString()
                })
                .Select(task => task.Result) // Resolve async tasks in Select
                .ToList();

            var paginatedUsers = PageList<UserViewModel>.Create(users, PageNumber ?? 1, 3);
            return View(paginatedUsers);
        }

<<<<<<< HEAD
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.LastPage = LastPage();
            return View();
        }

=======
        // Displays the user creation form
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.LastPage = LastPage(); // Passes the last page number to the view
            return View();
        }

        // Handles user creation logic
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ImageName = DefaultImageName,
<<<<<<< HEAD
                    UserName = new MailAddress(model.Email).User
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = model.IsAdmin ? Roles.Admin.ToString() : Roles.User.ToString();
                    await _userManager.AddToRoleAsync(user, role);
                    return RedirectToAction(nameof(Index));
                }

                AddErrorsToModelState(result.Errors);
=======
                    UserName = new MailAddress(model.Email).User // Extracts username from email
                };

                var result = await _userManager.CreateAsync(user, model.Password); // Creates the user
                if (result.Succeeded)
                {
                    var role = model.IsAdmin ? Roles.Admin.ToString() : Roles.User.ToString(); // Assigns role
                    await _userManager.AddToRoleAsync(user, role);
                    return RedirectToAction(nameof(Index)); // Redirects to user list
                }

                AddErrorsToModelState(result.Errors); // Adds errors to ModelState
>>>>>>> 69e884f (Initial project upload)
            }
            return View(model);
        }

<<<<<<< HEAD
=======
        // Displays the registration form
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

<<<<<<< HEAD
=======
        // Handles user registration logic
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ImageName = DefaultImageName,
<<<<<<< HEAD
                    UserName = new MailAddress(model.Email).User
=======
                    UserName = new MailAddress(model.Email).User // Extracts username from email
>>>>>>> 69e884f (Initial project upload)
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
<<<<<<< HEAD
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserIndex", "Category");
=======
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString()); // Assigns default role
                    await _signInManager.SignInAsync(user, isPersistent: false); // Signs in the user
                    return RedirectToAction("UserIndex", "Category"); // Redirects to Category page
>>>>>>> 69e884f (Initial project upload)
                }

                AddErrorsToModelState(result.Errors);
            }
            return View(model);
        }

<<<<<<< HEAD
=======
        // Displays the user's profile
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Profile()
        {
<<<<<<< HEAD
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();
=======
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Gets the current user
            if (user == null) return NotFound(); // Returns 404 if user not found
>>>>>>> 69e884f (Initial project upload)

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                ImageName = user.ImageName ?? DefaultImageName
            };
            return View(model);
        }

<<<<<<< HEAD
=======
        // Updates the user's profile details
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
<<<<<<< HEAD
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
=======
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Gets the current user
>>>>>>> 69e884f (Initial project upload)
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

<<<<<<< HEAD
            var result = await _userManager.UpdateAsync(user);
=======
            var result = await _userManager.UpdateAsync(user); // Updates the user
>>>>>>> 69e884f (Initial project upload)
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Profile)); // Redirects to profile page
            }
<<<<<<< HEAD

            AddErrorsToModelState(result.Errors);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePhoto(UpdateImageViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            if (ModelState.IsValid && model.Image != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserProfileImages", model.Image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                user.ImageName = model.Image.FileName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }

                AddErrorsToModelState(result.Errors);
            }
            else
            {
                ModelState.AddModelError("", "File not found.");
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoto()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
=======

            AddErrorsToModelState(result.Errors);
            return View(model);
        }

        // Updates the user's profile photo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePhoto(UpdateImageViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Gets the current user
            if (user == null) return NotFound();

            if (ModelState.IsValid && model.Image != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserProfileImages", model.Image.FileName); // Saves the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                user.ImageName = model.Image.FileName;

                var result = await _userManager.UpdateAsync(user); // Updates the user's image
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile)); // Redirects to profile page
                }

                AddErrorsToModelState(result.Errors);
            }
            else
            {
                ModelState.AddModelError("", "File not found."); // Adds error if file is missing
            }

            return RedirectToAction(nameof(Profile));
        }

        // Removes the user's profile photo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoto()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Gets the current user
>>>>>>> 69e884f (Initial project upload)
            if (user == null) return NotFound();

            if (user.ImageName != DefaultImageName)
            {
<<<<<<< HEAD
                user.ImageName = DefaultImageName;
=======
                user.ImageName = DefaultImageName; // Resets to default image
>>>>>>> 69e884f (Initial project upload)
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }

                AddErrorsToModelState(result.Errors);
            }
            else
            {
<<<<<<< HEAD
                ModelState.AddModelError("", "The user already has the default photo.");
=======
                ModelState.AddModelError("", "The user already has the default photo."); // Error if photo is already default
>>>>>>> 69e884f (Initial project upload)
            }

            return RedirectToAction(nameof(Profile));
        }

<<<<<<< HEAD
=======
        // Displays the login form
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

<<<<<<< HEAD
=======
        // Handles user login logic
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserIndex", "Category");
                }

                ModelState.AddModelError("", "Invalid email or password.");
=======
                var user = await _userManager.FindByEmailAsync(model.Email); // Finds the user by email
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) // Verifies password
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); // Signs in the user
                    return RedirectToAction("UserIndex", "Category"); // Redirects to Category page
                }

                ModelState.AddModelError("", "Invalid email or password."); // Adds login error
>>>>>>> 69e884f (Initial project upload)
            }

            return View(model);
        }

<<<<<<< HEAD
=======
        // Handles user logout
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
<<<<<<< HEAD
            await _signInManager.SignOutAsync();
            return RedirectToAction("UserIndex", "Category");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return RedirectToAction(nameof(Index));
=======
            await _signInManager.SignOutAsync(); // Signs out the user
            return RedirectToAction("UserIndex", "Category"); // Redirects to Category page
        }

        // Deletes a user by ID
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // Finds the user by ID
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user); // Deletes the user
            if (result.Succeeded) return RedirectToAction(nameof(Index)); // Redirects to user list
>>>>>>> 69e884f (Initial project upload)

            AddErrorsToModelState(result.Errors);
            return View(nameof(Index));
        }

<<<<<<< HEAD
        private int LastPage()
        {
            int count = _userManager.Users.Count();
            return (int)Math.Ceiling(count / 3d);
        }

=======
        // Calculates the last page number for pagination
        private int LastPage()
        {
            int count = _userManager.Users.Count(); // Total number of users
            return (int)Math.Ceiling(count / 3d); // Calculates total pages with 3 users per page
        }

        // Adds identity errors to the ModelState
>>>>>>> 69e884f (Initial project upload)
        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
<<<<<<< HEAD
                ModelState.AddModelError("", error.Description);
=======
                ModelState.AddModelError("", error.Description); // Adds each error to ModelState
>>>>>>> 69e884f (Initial project upload)
            }
        }
    }
}

