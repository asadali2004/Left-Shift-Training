using System;
using System.Collections.Generic;
using LMS;

namespace LibraryManagementSystem
{
    class Program
    {
        // Static dictionary to store all books with unique ID as key
        public static Dictionary<int, Book> BookDetails = new Dictionary<int, Book>();

        static void Main(string[] args)
        {
            // Create library utility instance for managing books
            LibraryUtility library = new LibraryUtility();
            bool exit = false;

            // Add some initial sample books to the library
            InitializeSampleBooks(library);

            // Main menu loop
            while (!exit)
            {
                Console.WriteLine("\n=== Library Management System ===");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. View All Books Grouped by Genre");
                Console.WriteLine("3. Search Books by Author");
                Console.WriteLine("4. View Library Statistics");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewBook(library);
                        break;
                    case "2":
                        ViewBooksByGenre(library);
                        break;
                    case "3":
                        SearchBooksByAuthor(library);
                        break;
                    case "4":
                        ViewStatistics(library);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("\nThank you for using Library Management System!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice! Please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        // Initializes the library with sample books
        static void InitializeSampleBooks(LibraryUtility library)
        {
            library.AddBook("The Great Gatsby", "F. Scott Fitzgerald", "Fiction", 1925);
            library.AddBook("Sapiens", "Yuval Noah Harari", "Non-Fiction", 2011);
            library.AddBook("Murder on the Orient Express", "Agatha Christie", "Mystery", 1934);
            library.AddBook("1984", "George Orwell", "Fiction", 1949);
            library.AddBook("Educated", "Tara Westover", "Non-Fiction", 2018);
            library.AddBook("The Girl with the Dragon Tattoo", "Stieg Larsson", "Mystery", 2005);
        }

        // Menu option 1: Add a new book to the library
        static void AddNewBook(LibraryUtility library)
        {
            Console.WriteLine("\n--- Add New Book ---");
            
            // Get book title from user
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            
            // Get author name from user
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            
            // Get genre from user
            Console.Write("Enter Genre: ");
            string genre = Console.ReadLine();
            
            // Get publication year from user with validation
            Console.Write("Enter Publication Year: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                library.AddBook(title, author, genre, year);
                Console.WriteLine("\nBook added successfully!");
            }
            else
            {
                Console.WriteLine("\nInvalid year! Book not added.");
            }
        }

        // Menu option 2: Display all books grouped by their genre
        static void ViewBooksByGenre(LibraryUtility library)
        {
            Console.WriteLine("\n--- Books Grouped by Genre ---");
            
            // Get books grouped by genre
            var booksByGenre = library.GroupBooksByGenre();
            
            // Check if library has any books
            if (booksByGenre.Count == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }

            // Display books organized by genre
            foreach (var genre in booksByGenre)
            {
                Console.WriteLine($"\n{genre.Key}:");
                foreach (var book in genre.Value)
                {
                    Console.WriteLine($"  - {book.Title} by {book.Author} ({book.PublicationYear})");
                }
            }
        }

        // Menu option 3: Search and display books by a specific author
        static void SearchBooksByAuthor(LibraryUtility library)
        {
            Console.WriteLine("\n--- Search Books by Author ---");
            
            // Get author name to search for
            Console.Write("Enter Author Name: ");
            string author = Console.ReadLine();
            
            // Retrieve books by the specified author
            var booksByAuthor = library.GetBooksByAuthor(author);
            
            // Display results or no books found message
            if (booksByAuthor.Count > 0)
            {
                Console.WriteLine($"\nBooks by {author}:");
                foreach (var book in booksByAuthor)
                {
                    Console.WriteLine($"  - {book.Title} ({book.Genre}, {book.PublicationYear})");
                }
            }
            else
            {
                Console.WriteLine($"\nNo books found by '{author}'.");
            }
        }

        // Menu option 4: Display library statistics including total books and books per genre
        static void ViewStatistics(LibraryUtility library)
        {
            Console.WriteLine("\n--- Library Statistics ---");
            
            // Get total count of books
            int totalBooks = library.GetTotalBooksCount();
            Console.WriteLine($"Total books in library: {totalBooks}");
            
            // Check if library has any books
            if (totalBooks == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }
            
            // Display count of books per genre
            Console.WriteLine("\nBooks per genre:");
            var genres = library.GroupBooksByGenre();
            foreach (var genre in genres)
            {
                Console.WriteLine($"  - {genre.Key}: {genre.Value.Count} book(s)");
            }
        }
    }
}
