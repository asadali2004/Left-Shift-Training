using System;
using System.Linq;

namespace _18_Event_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            EventManager manager = new EventManager();

            // ✅ Hardcoded Events
            manager.CreateEvent("Coldplay Live", "Concert",
                DateTime.Today.AddDays(10), "Delhi Stadium", 5000, 3500);

            manager.CreateEvent("Tech Summit 2026", "Conference",
                DateTime.Today.AddDays(20), "Bangalore Convention Center", 2000, 2500);

            manager.CreateEvent("AI Workshop", "Workshop",
                DateTime.Today.AddDays(5), "Mumbai Hub", 300, 1500);

            // ✅ Hardcoded Attendees
            manager.AddAttendee("Asad", "asad@mail.com", "9999999999");
            manager.AddAttendee("Rohit", "rohit@mail.com", "8888888888");

            // Simulate booking
            manager.BookTicket(1, 1, "A1");
            manager.BookTicket(3, 2, "B5");

            while (true)
            {
                Console.WriteLine("\n=== Event Management System ===");
                Console.WriteLine("1. View Events");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Group Events By Type");
                Console.WriteLine("4. View Upcoming Events");
                Console.WriteLine("5. Calculate Event Revenue");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var e in manager.Events.Values)
                    {
                        Console.WriteLine($"{e.EventId} | {e.EventName} | {e.EventType} | Seats: {e.TicketsSold}/{e.TotalCapacity}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Event ID: ");
                    int eventId = int.Parse(Console.ReadLine());

                    Console.Write("Attendee ID: ");
                    int attendeeId = int.Parse(Console.ReadLine());

                    Console.Write("Seat Number: ");
                    string seat = Console.ReadLine();

                    if (manager.BookTicket(eventId, attendeeId, seat))
                        Console.WriteLine("Ticket booked successfully!");
                    else
                        Console.WriteLine("Booking failed!");
                }

                else if (choice == "3")
                {
                    var grouped = manager.GroupEventsByType();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nType: {g.Key}");

                        foreach (var e in g.Value)
                            Console.WriteLine($"  {e.EventName}");
                    }
                }

                else if (choice == "4")
                {
                    Console.Write("Enter days: ");
                    int days = int.Parse(Console.ReadLine());

                    var upcoming = manager.GetUpcomingEvents(days);

                    if (!upcoming.Any())
                        Console.WriteLine("No upcoming events!");
                    else
                    {
                        foreach (var e in upcoming)
                        {
                            Console.WriteLine($"{e.EventName} on {e.EventDate:d}");
                        }
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Event ID: ");
                    int id = int.Parse(Console.ReadLine());

                    double revenue = manager.CalculateEventRevenue(id);

                    Console.WriteLine($"Total Revenue: ₹{revenue}");
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Exiting Event System!");
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
