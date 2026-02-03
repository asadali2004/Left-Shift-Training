using System.Collections.Concurrent;
namespace LMS
{
    public class LibraryUtility : ILibraryUtility
    {
        public void AddBook(string title, string author, string genre, int year)
        {
            UserInterface.BookDetails.Add(UserInterface.BookDetails.Count + 1, new Book(title, author, genre, year));
        }

        public SortedDictionary<string, List<Book>> GroupBooksByGenre()
        {
            var result = UserInterface.BookDetails.Values
                .GroupBy(b => b.Genre)
                .OrderBy(e => e.Key)
                .ToDictionary(k => k.Key, b => b.ToList());
            return new SortedDictionary<string, List<Book>>(result);
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> result = UserInterface.BookDetails.Values.Where(b => b.Author == author).ToList();
            return result;
        }

        public int GetTotalBooksCount()
        {
            int result = UserInterface.BookDetails.Count;
            return result;
        }
    }
}