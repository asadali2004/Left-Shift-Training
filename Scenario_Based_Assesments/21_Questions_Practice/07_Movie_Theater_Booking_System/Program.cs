using System;
using System.Linq;

namespace MovieTheaterBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            TheaterManager manager = new TheaterManager();
            
            // Add some hardcoded test data
            manager.AddScreening("Avengers", DateTime.Today.AddHours(14), "Screen 1", 150, 12.99);
            manager.AddScreening("Avengers", DateTime.Today.AddHours(18), "Screen 1", 150, 12.99);
            manager.AddScreening("Avengers", DateTime.Today.AddHours(21), "Screen 2", 200, 15.99);
            manager.AddScreening("Inception", DateTime.Today.AddHours(15), "Screen 3", 100, 10.99);
            manager.AddScreening("Inception", DateTime.Today.AddHours(20), "Screen 3", 100, 10.99);
            manager.AddScreening("The Matrix", DateTime.Today.AddHours(16), "Screen 2", 180, 11.99);
            manager.AddScreening("The Matrix", DateTime.Today.AddHours(19), "Screen 4", 120, 11.99);
            manager.AddScreening("Interstellar", DateTime.Today.AddHours(17), "Screen 4", 130, 13.99);
            
            // Book some tickets for testing
            manager.BookTickets("Avengers", DateTime.Today.AddHours(14), 50);
            manager.BookTickets("Inception", DateTime.Today.AddHours(15), 30);
            manager.BookTickets("The Matrix", DateTime.Today.AddHours(16), 40);
            
            while (true)
            {
                Console.WriteLine("\n=== Movie Theater Booking System ===");
                Console.WriteLine("1. Add Screening");
                Console.WriteLine("2. View All Screenings");
                Console.WriteLine("3. Book Tickets");
                Console.WriteLine("4. View Screenings by Movie");
                Console.WriteLine("5. View Available Screenings for Group");
                Console.WriteLine("6. View Total Revenue");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Screening
                    Console.WriteLine("\n--- Add Screening ---");
                    Console.Write("Enter Movie Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Show Time (hours from now): ");
                    int hours = int.Parse(Console.ReadLine());
                    DateTime showTime = DateTime.Now.AddHours(hours);
                    Console.Write("Enter Screen Number: ");
                    string screen = Console.ReadLine();
                    Console.Write("Enter Total Seats: ");
                    int seats = int.Parse(Console.ReadLine());
                    Console.Write("Enter Ticket Price: ");
                    double price = double.Parse(Console.ReadLine());
                    
                    manager.AddScreening(title, showTime, screen, seats, price);
                    Console.WriteLine("Screening added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Screenings
                    Console.WriteLine("\n--- All Screenings ---");
                    if (manager.Screenings.Count == 0)
                    {
                        Console.WriteLine("No screenings found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-12} {4,-10}", "Movie", "Show Time", "Screen", "Available", "Price");
                        Console.WriteLine(new string('-', 80));
                        foreach (var screening in manager.Screenings)
                        {
                            int available = screening.TotalSeats - screening.BookedSeats;
                            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-12} ${4,-9:F2}", 
                                screening.MovieTitle, screening.ShowTime.ToString("g"), screening.ScreenNumber, 
                                $"{available}/{screening.TotalSeats}", screening.TicketPrice);
                        }
                    }
                }
                else if (choice == "3")
                {
                    // Book Tickets
                    Console.WriteLine("\n--- Book Tickets ---");
                    if (manager.Screenings.Count == 0)
                    {
                        Console.WriteLine("No screenings available!");
                    }
                    else
                    {
                        Console.Write("Enter Movie Title: ");
                        string title = Console.ReadLine();
                        
                        var movieScreenings = manager.Screenings.Where(s => s.MovieTitle.ToLower() == title.ToLower()).ToList();
                        if (movieScreenings.Count == 0)
                        {
                            Console.WriteLine("Movie not found!");
                        }
                        else
                        {
                            Console.WriteLine("\nAvailable Show Times:");
                            for (int i = 0; i < movieScreenings.Count; i++)
                            {
                                int available = movieScreenings[i].TotalSeats - movieScreenings[i].BookedSeats;
                                Console.WriteLine($"{i + 1}. {movieScreenings[i].ShowTime:g} - {movieScreenings[i].ScreenNumber} - Available: {available}");
                            }
                            
                            Console.Write("\nSelect show time (number): ");
                            int showIndex = int.Parse(Console.ReadLine()) - 1;
                            
                            if (showIndex >= 0 && showIndex < movieScreenings.Count)
                            {
                                Console.Write("Enter number of tickets: ");
                                int tickets = int.Parse(Console.ReadLine());
                                
                                if (manager.BookTickets(movieScreenings[showIndex].MovieTitle, movieScreenings[showIndex].ShowTime, tickets))
                                {
                                    double total = tickets * movieScreenings[showIndex].TicketPrice;
                                    Console.WriteLine($"\nBooking successful! Total: ${total:F2}");
                                }
                                else
                                {
                                    Console.WriteLine("Booking failed! Not enough seats available.");
                                }
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // View Screenings by Movie
                    Console.WriteLine("\n--- Screenings by Movie ---");
                    if (manager.Screenings.Count == 0)
                    {
                        Console.WriteLine("No screenings found!");
                    }
                    else
                    {
                        var grouped = manager.GroupScreeningsByMovie();
                        foreach (var movie in grouped)
                        {
                            Console.WriteLine($"\n{movie.Key} ({movie.Value.Count} screenings):");
                            foreach (var screening in movie.Value)
                            {
                                int available = screening.TotalSeats - screening.BookedSeats;
                                Console.WriteLine($"  {screening.ShowTime:g} - {screening.ScreenNumber} - Available: {available}/{screening.TotalSeats} - ${screening.TicketPrice:F2}");
                            }
                        }
                    }
                }
                else if (choice == "5")
                {
                    // View Available Screenings for Group
                    Console.WriteLine("\n--- Available Screenings for Group ---");
                    Console.Write("Enter minimum seats required: ");
                    int minSeats = int.Parse(Console.ReadLine());
                    
                    var availableScreenings = manager.GetAvailableScreenings(minSeats);
                    if (availableScreenings.Count > 0)
                    {
                        Console.WriteLine($"\nFound {availableScreenings.Count} screenings with at least {minSeats} seats:");
                        Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-12}", "Movie", "Show Time", "Screen", "Available");
                        Console.WriteLine(new string('-', 70));
                        foreach (var screening in availableScreenings)
                        {
                            int available = screening.TotalSeats - screening.BookedSeats;
                            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-12}", 
                                screening.MovieTitle, screening.ShowTime.ToString("g"), screening.ScreenNumber, available);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No screenings available with required seats!");
                    }
                }
                else if (choice == "6")
                {
                    // View Total Revenue
                    Console.WriteLine("\n--- Total Revenue ---");
                    double revenue = manager.CalculateTotalRevenue();
                    int totalBooked = manager.Screenings.Sum(s => s.BookedSeats);
                    Console.WriteLine($"Total Tickets Booked: {totalBooked}");
                    Console.WriteLine($"Total Revenue: ${revenue:F2}");
                }
                else if (choice == "7")
                {
                    Console.WriteLine("Thank you!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}
