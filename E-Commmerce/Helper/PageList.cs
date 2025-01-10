namespace E_Commmerce.Helper
{
<<<<<<< HEAD
=======
    // A generic class to handle pagination for lists
>>>>>>> 69e884f (Initial project upload)
    public class PageList<T> : List<T> where T : class
    {
        // Current page index (1-based)
        public int PageIndex { get; set; }

        // Total number of pages
        public int TotalPages { get; set; }

<<<<<<< HEAD
=======
        // Constructor to initialize pagination properties
>>>>>>> 69e884f (Initial project upload)
        public PageList(List<T> list, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex; // Set the current page index
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Calculate total pages
            this.AddRange(list); // Add the items for the current page
        }

<<<<<<< HEAD
        public bool HasPreviousPage => PageIndex > 1; // Corrected spelling
        public bool HasNextPage => PageIndex < TotalPages;

        public static PageList<T> Create(IEnumerable<T> list, int pageIndex, int pageSize)
        {
            var count = list.Count();
            var items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
=======
        // Indicates if there is a previous page
        public bool HasPreviousPage => PageIndex > 1;

        // Indicates if there is a next page
        public bool HasNextPage => PageIndex < TotalPages;

        // Static method to create a paginated list
        public static PageList<T> Create(IEnumerable<T> list, int pageIndex, int pageSize)
        {
            var count = list.Count(); // Get the total number of items
            var items = list
                .Skip((pageIndex - 1) * pageSize) // Skip items for previous pages
                .Take(pageSize) // Take items for the current page
                .ToList();

            // Return a new PageList with the items, count, page index, and page size
>>>>>>> 69e884f (Initial project upload)
            return new PageList<T>(items, count, pageIndex, pageSize);
        }
    }
}

