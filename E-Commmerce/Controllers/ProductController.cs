<<<<<<< HEAD
﻿using E_Commmerce.Helper;
using E_Commmerce.IReposatory;
using E_Commmerce.Models;
using E_Commmerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

=======
﻿using E_Commmerce.Helper; // Utility and helper classes
using E_Commmerce.IReposatory; // Interfaces for repositories
using E_Commmerce.Models; // Models for products, categories, and related data
using E_Commmerce.ViewModels; // ViewModel classes for data representation
using Microsoft.AspNetCore.Authorization; // For authorization attributes
using Microsoft.AspNetCore.Mvc; // For controller functionalities
using Microsoft.AspNetCore.Mvc.Filters; // For implementing action filters
using Microsoft.AspNetCore.Mvc.Rendering; // For generating select list items

namespace E_Commmerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))] // Restrict access to admin users only
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository; // Repository for managing products
        private readonly ICategoryRepository _categoryRepository; // Repository for managing categories
        private readonly IWebHostEnvironment _webHostEnvironment; // Provides hosting environment details

        // Constructor to initialize dependencies
>>>>>>> 69e884f (Initial project upload)
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

<<<<<<< HEAD
=======
        // Displays a paginated list of products, optionally filtered by category
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        public IActionResult Index(int? CatId, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
<<<<<<< HEAD
                .Where(p => !CatId.HasValue || p.CategoryId == CatId.Value)
=======
                .Where(p => !CatId.HasValue || p.CategoryId == CatId.Value) // Filter by category if provided
>>>>>>> 69e884f (Initial project upload)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
<<<<<<< HEAD
                    ImageViewModel = new UpdateImageViewModel { ImageName = p.ImageName ?? "Default.png" }
                })
                .ToList();

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);
=======
                    ImageViewModel = new UpdateImageViewModel { ImageName = p.ImageName ?? "Default.png" } // Use default image if none provided
                })
                .ToList();

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize); // Create paginated list
>>>>>>> 69e884f (Initial project upload)

            var categories = _categoryRepository.GetAll.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
<<<<<<< HEAD
                Selected = CatId.HasValue && c.Id == CatId.Value
            }).ToList();

            ViewBag.Categories = categories;
=======
                Selected = CatId.HasValue && c.Id == CatId.Value // Mark selected category
            }).ToList();

            ViewBag.Categories = categories; // Pass categories to the view for filtering
>>>>>>> 69e884f (Initial project upload)

            return View(paginatedList);
        }



<<<<<<< HEAD
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(new ProductViewModel());
=======
        // Displays the form for creating a new product
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll); // Populate category dropdown
            return View(new ProductViewModel()); // Render the view with an empty product model
>>>>>>> 69e884f (Initial project upload)
        }

        // Handles the creation of a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel model)
        {
<<<<<<< HEAD
            if (ModelState.IsValid)
=======
            if (ModelState.IsValid) // Validate the input model
>>>>>>> 69e884f (Initial project upload)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
<<<<<<< HEAD
                    Category = _categoryRepository.GetbyId(model.CategoryId ?? 0)
                };
                _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetbyId(id);
            if (product == null) return NotFound();
=======
                    Category = _categoryRepository.GetbyId(model.CategoryId ?? 0) // Retrieve associated category
                };
                _productRepository.Add(product); // Add the new product to the repository
                return RedirectToAction(nameof(Index)); // Redirect to the product list
            }
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll); // Repopulate category dropdown
            return View(model); // Render the view with validation errors
        }

        // Displays the form for editing an existing product
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetbyId(id); // Retrieve product by ID
            if (product == null) return NotFound(); // Return 404 if not found
>>>>>>> 69e884f (Initial project upload)

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Category = _categoryRepository.GetbyId(product.CategoryId ?? 0),
                ImageViewModel = new UpdateImageViewModel { ImageName = product.ImageName ?? "Default.png" }
            };

<<<<<<< HEAD
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(model);
=======
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll); // Populate category dropdown
            return View(model); // Render the edit view
>>>>>>> 69e884f (Initial project upload)
        }

        // Handles the editing of an existing product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid) // Validate the input model
            {
                var product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    Category = _categoryRepository.GetbyId(model.CategoryId ?? 0)
                };
<<<<<<< HEAD
                _productRepository.Update(model.Id, product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(model);
        }

=======
                _productRepository.Update(model.Id, product); // Update the product in the repository
                return RedirectToAction(nameof(Index)); // Redirect to the product list
            }
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll); // Repopulate category dropdown
            return View(model); // Render the view with validation errors
        }

        // Handles updating the product image
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePhoto(ProductImage model)
        {
<<<<<<< HEAD
            if (ModelState.IsValid && model.Image != null)
=======
            if (ModelState.IsValid && model.Image != null) // Validate input and ensure an image is provided
>>>>>>> 69e884f (Initial project upload)
            {
                var product = _productRepository.GetbyId(model.Id); // Retrieve product by ID
                if (product != null)
                {
<<<<<<< HEAD
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages", model.Image.FileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    model.Image.CopyTo(stream);

                    product.ImageName = model.Image.FileName;
                    _productRepository.Update(model.Id, product);
                }
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "No Image Selected!");
            return RedirectToAction(nameof(Edit), new { Id = model.Id });
        }

=======
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages", model.Image.FileName); // Define file path
                    using var stream = new FileStream(path, FileMode.Create); // Save the image
                    model.Image.CopyTo(stream);

                    product.ImageName = model.Image.FileName; // Update the product's image
                    _productRepository.Update(model.Id, product); // Save changes to the repository
                }
                return RedirectToAction(nameof(Index)); // Redirect to the product list
            }
            ModelState.AddModelError("", "No Image Selected!"); // Add error if no image is selected
            return RedirectToAction(nameof(Edit), new { Id = model.Id }); // Redirect back to the edit view
        }

        // Deletes a product by ID
>>>>>>> 69e884f (Initial project upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
<<<<<<< HEAD
            var product = _productRepository.GetbyId(id);
            if (product == null) return NotFound();

            _productRepository.Remove(product);
            return RedirectToAction(nameof(Index));
        }

=======
            var product = _productRepository.GetbyId(id); // Retrieve product by ID
            if (product == null) return NotFound(); // Return 404 if not found

            _productRepository.Remove(product); // Remove the product from the repository
            return RedirectToAction(nameof(Index)); // Redirect to the product list
        }

        // Displays products for users, optionally filtered by search term
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserIndex(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
                .Where(p => string.IsNullOrEmpty(searchTerm) ||
                            p.Name.Contains(searchTerm) ||
<<<<<<< HEAD
                            p.Description.Contains(searchTerm))
=======
                            p.Description.Contains(searchTerm)) // Filter by search term
>>>>>>> 69e884f (Initial project upload)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Category = _categoryRepository.GetbyId(p.CategoryId ?? 0),
                    ImageViewModel = new UpdateImageViewModel { ImageName = p.ImageName ?? "Default.png" }
                })
                .ToList();
<<<<<<< HEAD

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);
            ViewBag.SearchTerm = searchTerm; // Pass search term back to the view
            return View(paginatedList);
        }

=======

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize); // Paginate products
            ViewBag.SearchTerm = searchTerm; // Pass search term to the view
            return View(paginatedList); // Render the user index view
        }

        // Displays products in a specific category, optionally filtered by search term
>>>>>>> 69e884f (Initial project upload)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ListByCategory(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
                .Where(p => p.CategoryId == categoryId &&
                           (string.IsNullOrEmpty(searchTerm) ||
                            p.Name.Contains(searchTerm) ||
<<<<<<< HEAD
                            p.Description.Contains(searchTerm)))
=======
                            p.Description.Contains(searchTerm))) // Filter by category and search term
>>>>>>> 69e884f (Initial project upload)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Category = _categoryRepository.GetbyId(p.CategoryId ?? 0),
                    ImageViewModel = new UpdateImageViewModel { ImageName = p.ImageName ?? "Default.png" }
                })
                .ToList();

<<<<<<< HEAD
            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);
            ViewBag.SearchTerm = searchTerm; // Pass search term back to the view
            return View("UserIndex", paginatedList);
        }

=======
            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize); // Paginate products
            ViewBag.SearchTerm = searchTerm; // Pass search term to the view
            return View("UserIndex", paginatedList); // Render the user index view
        }

        // Helper method to generate a dropdown list of categories
>>>>>>> 69e884f (Initial project upload)
        private IEnumerable<SelectListItem> CategoryDropDownList(IEnumerable<Category> categories)
        {
            return categories.Select(c => new SelectListItem
            {
<<<<<<< HEAD
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
=======
                Text = c.Name, // Display text for the dropdown
                Value = c.Id.ToString() // Value for the dropdown
            });
        }

        // Executes before every action to set cart item count in the ViewBag
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart(); // Retrieve cart from session
            ViewBag.CartItemCount = cart.TotalQuantity; // Set total item count in the ViewBag
            base.OnActionExecuting(context); // Call base method for default behavior
>>>>>>> 69e884f (Initial project upload)
        }
    }
}
