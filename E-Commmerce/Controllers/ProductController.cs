using E_Commmerce.Helper;
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

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int? CatId, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
                .Where(p => !CatId.HasValue || p.CategoryId == CatId.Value)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
                    ImageViewModel = new UpdateImageViewModel { ImageName = p.ImageName ?? "Default.png" }
                })
                .ToList();

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);

            var categories = _categoryRepository.GetAll.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = CatId.HasValue && c.Id == CatId.Value
            }).ToList();

            ViewBag.Categories = categories;

            return View(paginatedList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
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

            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
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
                _productRepository.Update(model.Id, product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = CategoryDropDownList(_categoryRepository.GetAll);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePhoto(ProductImage model)
        {
            if (ModelState.IsValid && model.Image != null)
            {
                var product = _productRepository.GetbyId(model.Id);
                if (product != null)
                {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetbyId(id);
            if (product == null) return NotFound();

            _productRepository.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserIndex(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
                .Where(p => string.IsNullOrEmpty(searchTerm) ||
                            p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm))
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

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);
            ViewBag.SearchTerm = searchTerm; // Pass search term back to the view
            return View(paginatedList);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ListByCategory(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAll
                .Where(p => p.CategoryId == categoryId &&
                           (string.IsNullOrEmpty(searchTerm) ||
                            p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm)))
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

            var paginatedList = PageList<ProductViewModel>.Create(products, pageIndex, pageSize);
            ViewBag.SearchTerm = searchTerm; // Pass search term back to the view
            return View("UserIndex", paginatedList);
        }

        private IEnumerable<SelectListItem> CategoryDropDownList(IEnumerable<Category> categories)
        {
            return categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            ViewBag.CartItemCount = cart.TotalQuantity;
            base.OnActionExecuting(context);
        }
    }
}
