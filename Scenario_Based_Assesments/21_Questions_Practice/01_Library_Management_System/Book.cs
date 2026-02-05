namespace LMS
{
    // Represents a book in the library with its details
    public class Book
    {
        // Title of the book
        public string Title { get; set; }
        
        // Author of the book
        public string Author { get; set; }
        
        // Genre/category of the book
        public string Genre { get; set; }
        
        // Year when the book was published
        public int PublicationYear { get; set; }

        // Constructor to initialize a book with all required details
        public Book(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublicationYear = year;
        }
    }
}
