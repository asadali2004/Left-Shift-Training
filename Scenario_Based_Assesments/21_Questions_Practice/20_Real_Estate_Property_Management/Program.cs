using System;
using System.Collections.Generic;
using System.Linq;

namespace _20_Real_Estate_Property_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            RealEstateManager manager = new RealEstateManager();

            // ✅ Hardcoded Properties
            manager.AddProperty("Mumbai Central", "Apartment", 2, 950, 12000000, "Mr. Sharma");
            manager.AddProperty("Bangalore North", "Villa", 4, 2500, 35000000, "Mrs. Iyer");
            manager.AddProperty("Delhi West", "House", 3, 1800, 22000000, "Mr. Gupta");

            // ✅ Hardcoded Clients
            manager.AddClient("Asad", "9999999999", "Buyer",
                25000000, new List<string> { "3BHK", "Parking" });

            manager.AddClient("Rohit", "8888888888", "Buyer",
                40000000, new List<string> { "Villa", "Garden" });

            // Simulate viewing
            manager.ScheduleViewing("PR1001", 1, DateTime.Today.AddDays(2));

            while (true)
            {
                Console.WriteLine("\n=== Real Estate Property Management ===");
                Console.WriteLine("1. View Properties");
                Console.WriteLine("2. Add Client");
                Console.WriteLine("3. Schedule Viewing");
                Console.WriteLine("4. Group Properties By Type");
                Console.WriteLine("5. Search Properties By Budget");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var p in manager.Properties.Values)
                    {
                        Console.WriteLine($"{p.PropertyId} | {p.PropertyType} | ₹{p.Price} | {p.Status}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Contact: ");
                    string contact = Console.ReadLine();

                    Console.Write("Type (Buyer/Renter): ");
                    string type = Console.ReadLine();

                    Console.Write("Budget: ");
                    double budget = double.Parse(Console.ReadLine());

                    Console.Write("Requirements (comma separated): ");
                    var req = Console.ReadLine()
                        .Split(',')
                        .Select(r => r.Trim())
                        .ToList();

                    manager.AddClient(name, contact, type, budget, req);

                    Console.WriteLine("Client added!");
                }

                else if (choice == "3")
                {
                    Console.Write("Property ID: ");
                    string pid = Console.ReadLine();

                    Console.Write("Client ID: ");
                    int cid = int.Parse(Console.ReadLine());

                    Console.Write("Viewing Date (yyyy-mm-dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    if (manager.ScheduleViewing(pid, cid, date))
                        Console.WriteLine("Viewing scheduled!");
                    else
                        Console.WriteLine("Failed to schedule viewing!");
                }

                else if (choice == "4")
                {
                    var grouped = manager.GroupPropertiesByType();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nType: {g.Key}");

                        foreach (var p in g.Value)
                            Console.WriteLine($"  {p.PropertyId} - ₹{p.Price}");
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Min Price: ");
                    double min = double.Parse(Console.ReadLine());

                    Console.Write("Max Price: ");
                    double max = double.Parse(Console.ReadLine());

                    var results = manager.GetPropertiesInBudget(min, max);

                    if (!results.Any())
                        Console.WriteLine("No properties found!");
                    else
                    {
                        foreach (var p in results)
                            Console.WriteLine($"{p.PropertyId} | {p.Address} | ₹{p.Price}");
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Exiting Real Estate System!");
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
