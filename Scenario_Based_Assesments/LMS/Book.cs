namespace LMS
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int PublicationYear { get; set; }

        public Book(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublicationYear = year;
        }
    }
}