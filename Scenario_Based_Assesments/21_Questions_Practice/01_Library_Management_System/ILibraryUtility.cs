using System.Collections.Generic;

namespace LMS
{
    public interface ILibraryUtility
    {
        void AddBook(string title, string author, string genre, int year);
        SortedDictionary<string, List<Book>> GroupBooksByGenre();
        List<Book> GetBooksByAuthor(string author);
        int GetTotalBooksCount();
    }
}
