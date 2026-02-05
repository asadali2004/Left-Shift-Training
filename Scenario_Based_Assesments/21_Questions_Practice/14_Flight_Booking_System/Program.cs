using System;
using System.Linq;

namespace _14_Flight_Booking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            AirlineManager manager = new AirlineManager();

            // ✅ Hardcoded Flights
            manager.AddFlight("AI101", "Delhi", "Mumbai",
                DateTime.Today.AddHours(10),
                DateTime.Today.AddHours(12), 150, 5000);

            manager.AddFlight("AI202", "Delhi", "Bangalore",
                DateTime.Today.AddHours(14),
                DateTime.Today.AddHours(17), 180, 6500);

            manager.AddFlight("AI303", "Mumbai", "Chennai",
                DateTime.Today.AddHours(9),
                DateTime.Today.AddHours(11), 120, 4500);

            // Simulate bookings
            manager.BookFlight("AI101", "Asad", 2, "Economy");
            manager.BookFlight("AI101", "Rohit", 1, "Business");

            while (true)
            {
                Console.WriteLine("\n=== Flight Booking System ===");
                Console.WriteLine("1. View All Flights");
                Console.WriteLine("2. Search Flights");
                Console.WriteLine("3. Book Flight");
                Console.WriteLine("4. Group Flights By Destination");
                Console.WriteLine("5. Calculate Flight Revenue");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n--- Flights ---");

                    foreach (var f in manager.Flights.Values)
                    {
                        Console.WriteLine($"{f.FlightNumber} | {f.Origin} -> {f.Destination} | " +
                                          $"Seats: {f.AvailableSeats}/{f.TotalSeats} | ₹{f.TicketPrice}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Origin: ");
                    string origin = Console.ReadLine();

                    Console.Write("Destination: ");
                    string dest = Console.ReadLine();

                    Console.Write("Date (yyyy-mm-dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    var results = manager.SearchFlights(origin, dest, date);

                    if (!results.Any())
                        Console.WriteLine("No flights found!");
                    else
                    {
                        foreach (var f in results)
                        {
                            Console.WriteLine($"{f.FlightNumber} | Departure: {f.DepartureTime:t} | ₹{f.TicketPrice}");
                        }
                    }
                }

                else if (choice == "3")
                {
                    Console.Write("Flight Number: ");
                    string number = Console.ReadLine();

                    Console.Write("Passenger Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Seats: ");
                    int seats = int.Parse(Console.ReadLine());

                    Console.Write("Seat Class (Economy/Business): ");
                    string seatClass = Console.ReadLine();

                    if (manager.BookFlight(number, name, seats, seatClass))
                        Console.WriteLine("Booking successful!");
                    else
                        Console.WriteLine("Booking failed!");
                }

                else if (choice == "4")
                {
                    var grouped = manager.GroupFlightsByDestination();

                    foreach (var dest in grouped)
                    {
                        Console.WriteLine($"\nDestination: {dest.Key}");

                        foreach (var f in dest.Value)
                            Console.WriteLine($"  {f.FlightNumber} from {f.Origin}");
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Flight Number: ");
                    string number = Console.ReadLine();

                    double revenue = manager.CalculateTotalRevenue(number);

                    Console.WriteLine($"Total Revenue: ₹{revenue}");
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Thank you for using Airline System!");
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
