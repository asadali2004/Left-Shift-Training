namespace LMS
{
    public interface ILibraryUtility
    {
        /// <summary>
        /// Adds a new book to the library with auto-incremented ID
        /// </summary>
        void AddBook(string title, string author, string genre, int year);

        /// <summary>
        /// Groups all books by genre in alphabetical order
        /// </summary>
        SortedDictionary<string, List<Book>> GroupBooksByGenre();

        /// <summary>
        /// Retrieves all books written by a specific author
        /// </summary>
        List<Book> GetBooksByAuthor(string author);

        /// <summary>
        /// Returns the total number of books in the library
        /// </summary>
        int GetTotalBooksCount();
    }
}
