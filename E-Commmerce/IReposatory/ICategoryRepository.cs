using E_Commmerce.Models; // For Category and Product models

namespace E_Commmerce.IReposatory
{
    // Interface for managing category-specific repository operations
    public interface ICategoryRepository : IRepository<Category> // Inherits base repository operations
    {
        /// <summary>
        /// Retrieves all products associated with a specific category.
        /// </summary>
        /// <param name="CategoryId">The ID of the category.</param>
        /// <returns>A list of products belonging to the specified category.</returns>
        List<Product> GetProducts(int CategoryId);

        /// <summary>
        /// Searches for products within a specific category based on a search term.
        /// </summary>
        /// <param name="CategoryId">The ID of the category.</param>
        /// <param name="SearchTerm">The term to search for in product name, description, or price.</param>
        /// <returns>A list of products matching the search term within the specified category.</returns>
        List<Product> Search(int CategoryId, string SearchTerm);
    }
}
