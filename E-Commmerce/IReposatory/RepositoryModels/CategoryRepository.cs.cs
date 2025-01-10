using E_Commmerce.Data; // For ApplicationDbcontext
using E_Commmerce.Models; // For Category and Product models
using Microsoft.EntityFrameworkCore; // For DbContext and LINQ extensions
using NuGet.Versioning; // (Unused in this class, consider removing)

namespace E_Commmerce.IReposatory.RepositoryModels
{
    // Repository implementation for Category-related database operations
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbcontext _dbcontext; // Database context for interacting with the database

        // Constructor to initialize the database context
        public CategoryRepository(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Retrieves all categories, including their associated products
        public IEnumerable<Category>? GetAll => _dbcontext.Categories.Include(c => c.Products).ToList();

        // Adds a new category to the database
        public void Add(Category entity)
        {
            if (entity != null)
            {
                _dbcontext.Categories.Add(entity); // Add the new category
                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new ArgumentNullException(nameof(entity)); // Throw exception if entity is null
        }

        // Retrieves a category by its ID, including its associated products
        public Category? GetbyId(int id)
            => _dbcontext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

        // Retrieves all products associated with a specific category
        public List<Product> GetProducts(int CategoryId)
        {
            var list = _dbcontext.Products.Where(p => p.CategoryId == CategoryId).ToList();
            return list; // Return the list of products
        }

        // Removes a category from the database
        public void Remove(Category entity)
        {
            if (entity != null)
            {
                _dbcontext.Categories.Remove(entity); // Remove the category
                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new ArgumentNullException(nameof(entity)); // Throw exception if entity is null
        }

        // Searches for products within a specific category based on a search term
        public List<Product> Search(int CategoryId, string SearchTerm)
        {
            var list = _dbcontext
                .Products
                .Where(p => p.CategoryId == CategoryId && // Match category
                    (p.Name.Contains(SearchTerm) || // Match product name
                     p.Description.Contains(SearchTerm) || // Match product description
                     p.Price.ToString().Contains(SearchTerm) // Match product price
                    ))
                .ToList();
            return list; // Return the matching products
        }

        // Updates an existing category in the database
        public void Update(int Id, Category entity)
        {
            var category = _dbcontext.Categories.Find(Id); // Find the category by ID
            var oldPicture = category?.ImageName; // Store the old image name

            if (category != null)
            {
                // Update category properties
                category.Name = entity.Name;
                category.Description = entity.Description;

                // Preserve the old image name if applicable
                if (!string.IsNullOrEmpty(oldPicture) && oldPicture != "Defualt.png")
                {
                    category.ImageName = oldPicture;
                }
                else
                {
                    category.ImageName = entity.ImageName;
                }

                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new ArgumentNullException(nameof(entity)); // Throw exception if category is null
        }
    }
}
