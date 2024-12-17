using E_Commmerce.Helper;
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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.LastPage = LastPage();
            return View();
        }

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
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

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
                    UserName = new MailAddress(model.Email).User
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserIndex", "Category");
                }

                AddErrorsToModelState(result.Errors);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Profile));
            }

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
            if (user == null) return NotFound();

            if (user.ImageName != DefaultImageName)
            {
                user.ImageName = DefaultImageName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }

                AddErrorsToModelState(result.Errors);
            }
            else
            {
                ModelState.AddModelError("", "The user already has the default photo.");
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("UserIndex", "Category");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("UserIndex", "Category");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return RedirectToAction(nameof(Index));

            AddErrorsToModelState(result.Errors);
            return View(nameof(Index));
        }

        private int LastPage()
        {
            int count = _userManager.Users.Count();
            return (int)Math.Ceiling(count / 3d);
        }

        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}

