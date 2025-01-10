<<<<<<< HEAD
﻿using E_Commmerce.Helper;
using E_Commmerce.IReposatory;
using E_Commmerce.Migrations;
using E_Commmerce.Models;
using E_Commmerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Packaging.Core;
using System.ComponentModel.DataAnnotations;
=======
﻿using E_Commmerce.Helper; // Utility methods or helpers
using E_Commmerce.IReposatory; // Interfaces for repositories
using E_Commmerce.Migrations; // Migrations for database updates
using E_Commmerce.Models; // Models for categories and related entities
using E_Commmerce.ViewModels; // ViewModel classes
using Microsoft.AspNetCore.Authorization; // For authorization attributes
using Microsoft.AspNetCore.Mvc; // Base class for controllers and MVC features
using Microsoft.AspNetCore.Mvc.Filters; // For implementing action filters
using Microsoft.AspNetCore.Mvc.ModelBinding; // For model binding attributes
using NuGet.Packaging.Core; // For NuGet packaging functionalities
using System.ComponentModel.DataAnnotations; // For validation attributes
>>>>>>> 69e884f (Initial project upload)

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))] // Restricts access to admin users only
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository; // Repository for managing categories
        private readonly IWebHostEnvironment _webHostEnvironment; // Provides web hosting environment details

        // Constructor to initialize dependencies
        public CategoryController(ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
        }

        // Displays a paginated list of categories
        public IActionResult Index(int PageNumber = 1)
        {
            var list = _categoryRepository.GetAll.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageViewModel = new UpdateImageViewModel() { ImageName = c.ImageName ?? "Defualt.png" }, // Default image if none provided
                ProductsCount = c.Products.Count // Count of products in the category
            }).ToList();

            PageList<CategoryViewModel> Categories = PageList<CategoryViewModel>.Create(list, PageNumber, 5); // Paginate categories
            return View(Categories); // Render the view with paginated categories
        }

        // Displays the form for creating a new category
        public IActionResult Create()
        {
            return View(); // Render the create category view
        }

        // Handles the creation of a new category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid) // Validate the input model
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };

                _categoryRepository.Add(category); // Add the new category to the repository

                // Calculate total pages for pagination after adding a new category
                var count = _categoryRepository.GetAll.Count();
                var PageNumber = (int)Math.Ceiling(count / 5d);
                ViewBag.TotalPages = PageNumber;

                return RedirectToAction(nameof(Index), new { PageNumber = PageNumber }); // Redirect to the category list
            }
            return View(model); // Render the view with validation errors
        }

        // Displays the form to edit an existing category
        [HttpGet]
        public IActionResult Edit(int Id, int PageNumber)
        {
            var Category = _categoryRepository.GetbyId(Id); // Get category by ID
            if (Category != null)
            {
                var model = new CategoryViewModel()
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Description = Category.Description,
                    ImageViewModel = new UpdateImageViewModel() { ImageName = Category.ImageName ?? "Defualt.png" }
                };

                ViewBag.pageIndex = PageNumber; // Pass current page number to the view
                return View(model); // Render the edit category view
            }
            return NotFound(); // Return 404 if category not found
        }

        // Handles the editing of an existing category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model, int PageNumber)
        {
            if (ModelState.IsValid) // Validate the input model
            {
                var category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description
                };

                _categoryRepository.Update(model.Id, category); // Update the category in the repository
                return RedirectToAction(nameof(Index), new { PageNumber = PageNumber }); // Redirect to the category list
            }
            return View(model); // Render the view with validation errors
        }

        // Handles updating the category image
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePhoto(CategoryImage model, int PageNumber)
        {
            if (ModelState.IsValid) // Validate the input model
            {
                var Category = _categoryRepository.GetbyId(model.Id); // Get category by ID
                if (Category != null)
                {
                    if (model.Image != null)
                    {
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "CategoryImages", model.Image.FileName); // Define file path
                        using var stream = new FileStream(path, FileMode.Create); // Save the image
                        model.Image.CopyTo(stream);

                        Category.ImageName = model.Image.FileName; // Update the category's image
                        _categoryRepository.Update(model.Id, Category); // Save changes to the repository
                        return RedirectToAction(nameof(Index), new { PageNumber = PageNumber }); // Redirect to the category list
                    }
                }
                ModelState.AddModelError("", "No Image Selected !!"); // Add error if no image is selected
            }
            return RedirectToAction(nameof(Edit), new { Id = model.Id }); // Redirect back to the edit view
        }

        // Deletes a category by ID
        public IActionResult Delete(int Id, int PageNumber)
        {
            var Category = _categoryRepository.GetbyId(Id); // Get category by ID
            if (Category != null)
            {
                _categoryRepository.Remove(Category); // Remove the category from the repository
                return RedirectToAction(nameof(Index), new { PageNumber = PageNumber }); // Redirect to the category list
            }
            return NotFound(); // Return 404 if category not found
        }

        // Displays the details of a category
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var Category = _categoryRepository.GetbyId(Id); // Get category by ID
            if (Category != null)
            {
                var CategoryViewModel = new CategoryViewModel()
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Description = Category.Description,
                    ImageViewModel = new UpdateImageViewModel { ImageName = Category.ImageName ?? "Defualt.png" },
                    ProductsCount = Category.Products.Count
                };

                return View(CategoryViewModel); // Render the details view with category data
            }
            return NotFound(); // Return 404 if category not found
        }

        // Displays all products within a category
        [HttpGet]
        public IActionResult ShowAll(int CategoryId)
        {
            var products = _categoryRepository.GetProducts(CategoryId); // Get products for the category
            ViewBag.CatName = _categoryRepository?.GetbyId(CategoryId)?.Name; // Set category name in ViewBag
            ViewBag.CatId = CategoryId; // Set category ID in ViewBag
            return View(products); // Render the view with the list of products
        }

        // Searches for products within a category based on a term
        [HttpPost]
        public IActionResult ShowAll(int catid, string term)
        {
            var list = _categoryRepository.Search(catid, term); // Search for products in the category
            ViewBag.CatName = _categoryRepository?.GetbyId(catid)?.Name; // Set category name in ViewBag
            return View(list); // Render the view with the search results
        }

        // Displays categories for regular users with pagination
        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserIndex(int PageNumber = 1)
        {
            var categories = _categoryRepository.GetAll.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageViewModel = new UpdateImageViewModel() { ImageName = c.ImageName ?? "Defualt.png" }
            }).ToList();

            PageList<CategoryViewModel> categoryViews = PageList<CategoryViewModel>.Create(categories, PageNumber, 9); // Paginate categories
            return View(categoryViews); // Render the user index view
        }

<<<<<<< HEAD
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }



=======
        // Executes before every action to set cart item count in the ViewBag
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart(); // Get cart from session
            ViewBag.CartItemCount = cart.TotalQuantity; // Set total quantity in ViewBag
            base.OnActionExecuting(context); // Call the base method
        }
>>>>>>> 69e884f (Initial project upload)
    }
}
