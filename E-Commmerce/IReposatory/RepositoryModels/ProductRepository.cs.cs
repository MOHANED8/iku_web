using E_Commmerce.Data; // For ApplicationDbcontext
using E_Commmerce.Models; // For Product and related models
using Microsoft.EntityFrameworkCore; // For Entity Framework Core functionalities

namespace E_Commmerce.IReposatory.RepositoryModels
{
    // Repository implementation for managing Product-related database operations
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbcontext _dbcontext; // Database context for interacting with the database

        // Constructor to initialize the database context
        public ProductRepository(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Retrieves all products, including their associated category
        public IEnumerable<Product>? GetAll => _dbcontext.Products.Include(p => p.Category).ToList();

        // Adds a new product to the database
        public void Add(Product entity)
        {
            if (entity != null)
            {
                _dbcontext.Products.Add(entity); // Add the new product
                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new NullReferenceException(nameof(entity)); // Throw exception if entity is null
        }

        // Retrieves a product by its ID
        public Product? GetbyId(int id)
        {
            return _dbcontext.Products.Find(id); // Find and return the product by ID
        }

        // Removes a product from the database
        public void Remove(Product entity)
        {
            if (entity != null)
            {
                _dbcontext.Remove(entity); // Remove the product
                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new NullReferenceException(nameof(entity)); // Throw exception if entity is null
        }

        // Updates an existing product in the database
        public void Update(int Id, Product entity)
        {
            var product = _dbcontext.Products.Find(Id); // Find the product by ID
            var oldPicture = product?.ImageName; // Store the old image name

            if (product != null)
            {
                // Update product properties
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Description = entity.Description;
                product.CategoryId = entity.CategoryId;
                product.Category = entity.Category;

                // Preserve the old image name if applicable
                if (!string.IsNullOrEmpty(oldPicture) && oldPicture != "Defualt.png")
                {
                    product.ImageName = oldPicture;
                }
                else
                {
                    product.ImageName = entity.ImageName;
                }

                _dbcontext.SaveChanges(); // Save changes to the database
                return;
            }
            throw new NullReferenceException(nameof(entity)); // Throw exception if product is null
        }
    }
}
