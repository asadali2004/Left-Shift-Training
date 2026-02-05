using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem;

namespace LMS
{
    // Utility class providing operations for library management
    public class LibraryUtility : ILibraryUtility
    {
        // Adds a new book to the library with auto-incremented ID
        public void AddBook(string title, string author, string genre, int year)
        {
            // Create new book with next available ID and add to dictionary
            Program.BookDetails.Add(Program.BookDetails.Count + 1, new Book(title, author, genre, year));
        }

        // Groups all books by their genre in alphabetical order
        public SortedDictionary<string, List<Book>> GroupBooksByGenre()
        {
            // Group books by genre, order alphabetically, and convert to sorted dictionary
            var result = Program.BookDetails.Values
                .GroupBy(b => b.Genre)
                .OrderBy(e => e.Key)
                .ToDictionary(k => k.Key, b => b.ToList());
            return new SortedDictionary<string, List<Book>>(result);
        }

        // Returns all books written by a specific author
        public List<Book> GetBooksByAuthor(string author)
        {
            // Filter books where author name matches
            List<Book> result = Program.BookDetails.Values.Where(b => b.Author == author).ToList();
            return result;
        }

        // Returns the total count of books in the library
        public int GetTotalBooksCount()
        {
            // Return the count of all books in the dictionary
            int result = Program.BookDetails.Count;
            return result;
        }
    }
}
