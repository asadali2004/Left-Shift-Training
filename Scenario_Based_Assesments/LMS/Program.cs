namespace LMS
{
    public class UserInterface
    {
        public static SortedDictionary<int, Book> BookDetails = new SortedDictionary<int, Book>();

        public static void Main(string[] args)
        {
            ILibraryUtility util = new LibraryUtility();
            InitializeSampleData(util);

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewBook(util);
                        break;
                    case "2":
                        DisplayBooksByGenre(util);
                        break;
                    case "3":
                        SearchBooksByAuthor(util);
                        break;
                    case "4":
                        DisplayStatistics(util);
                        break;
                    case "5":
                        DisplayAllBooks();
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("\n✓ Thank you for using Library Management System. Goodbye!\n");
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║       📚 LIBRARY MANAGEMENT SYSTEM 📚             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.WriteLine("1. ➕ Add New Book");
            Console.WriteLine("2. 📖 View Books by Genre");
            Console.WriteLine("3. 🔍 Search Books by Author");
            Console.WriteLine("4. 📊 View Library Statistics");
            Console.WriteLine("5. 📋 View All Books");
            Console.WriteLine("6. 🚪 Exit");
            Console.Write("\nEnter your choice (1-6): ");
        }

        private static void AddNewBook(ILibraryUtility util)
        {
            Console.WriteLine("\n========== Add New Book ==========");
            Console.Write("Enter Title: ");
            string? title = Console.ReadLine() ?? "";

            Console.Write("Enter Author: ");
            string? author = Console.ReadLine() ?? "";

            Console.Write("Enter Genre: ");
            string? genre = Console.ReadLine() ?? "";

            Console.Write("Enter Publication Year: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                util.AddBook(title, author, genre, year);
                Console.WriteLine($"✓ Book '{title}' added successfully!\n");
            }
            else
            {
                Console.WriteLine("❌ Invalid year format.\n");
            }
        }

        private static void DisplayBooksByGenre(ILibraryUtility util)
        {
            Console.WriteLine("\n========== Books Grouped by Genre (Alphabetically) ==========\n");
            var groupedBooks = util.GroupBooksByGenre();

            if (groupedBooks.Count == 0)
            {
                Console.WriteLine("📭 No books in library.\n");
                return;
            }

            foreach (var genre in groupedBooks)
            {
                Console.WriteLine($"📚 {genre.Key.ToUpper()} ({genre.Value.Count} books)");
                foreach (var book in genre.Value)
                {
                    Console.WriteLine($"   • {book.Title} by {book.Author} ({book.PublicationYear})");
                }
                Console.WriteLine();
            }
        }

        private static void SearchBooksByAuthor(ILibraryUtility util)
        {
            Console.WriteLine("\n========== Search Books by Author ==========");
            Console.Write("Enter Author Name: ");
            string? author = Console.ReadLine() ?? "";

            var booksByAuthor = util.GetBooksByAuthor(author);

            if (booksByAuthor.Count == 0)
            {
                Console.WriteLine($"❌ No books found by author '{author}'.\n");
                return;
            }

            Console.WriteLine($"\n📖 Books by {author} ({booksByAuthor.Count} books):");
            foreach (var book in booksByAuthor)
            {
                Console.WriteLine($"   • {book.Title} ({book.Genre}) - {book.PublicationYear}");
            }
            Console.WriteLine();
        }

        private static void DisplayStatistics(ILibraryUtility util)
        {
            Console.WriteLine("\n========== Library Statistics ==========\n");
            int totalBooks = util.GetTotalBooksCount();
            var groupedBooks = util.GroupBooksByGenre();

            Console.WriteLine($"📊 Total Books: {totalBooks}");
            Console.WriteLine($"📊 Total Genres: {groupedBooks.Count}");
            Console.WriteLine("\n📊 Books per Genre:");

            foreach (var genre in groupedBooks)
            {
                int percentage = totalBooks > 0 ? (genre.Value.Count * 100) / totalBooks : 0;
                Console.WriteLine($"   • {genre.Key}: {genre.Value.Count} books ({percentage}%)");
            }
            Console.WriteLine();
        }

        private static void DisplayAllBooks()
        {
            Console.WriteLine("\n========== All Books in Library ==========\n");

            if (BookDetails.Count == 0)
            {
                Console.WriteLine("📭 No books in library.\n");
                return;
            }

            foreach (var entry in BookDetails)
            {
                var book = entry.Value;
                Console.WriteLine($"ID: {entry.Key} | Title: {book.Title}");
                Console.WriteLine($"   Author: {book.Author} | Genre: {book.Genre} | Year: {book.PublicationYear}");
                Console.WriteLine();
            }
        }

        private static void InitializeSampleData(ILibraryUtility util)
        {
            // Add sample data on startup
            util.AddBook("The Great Gatsby", "F. Scott Fitzgerald", "Fiction", 1925);
            util.AddBook("1984", "George Orwell", "Fiction", 1949);
            util.AddBook("The Catcher in the Rye", "J.D. Salinger", "Fiction", 1951);

            util.AddBook("A Brief History of Time", "Stephen Hawking", "Non-Fiction", 1988);
            util.AddBook("Sapiens", "Yuval Noah Harari", "Non-Fiction", 2011);
            util.AddBook("Thinking, Fast and Slow", "Daniel Kahneman", "Non-Fiction", 2011);

            util.AddBook("Murder on the Orient Express", "Agatha Christie", "Mystery", 1934);
            util.AddBook("The Girl with the Dragon Tattoo", "Stieg Larsson", "Mystery", 2005);
            util.AddBook("The Da Vinci Code", "Dan Brown", "Mystery", 2003);

            Console.WriteLine("✓ Sample data loaded (9 books)");
        }
    }
}