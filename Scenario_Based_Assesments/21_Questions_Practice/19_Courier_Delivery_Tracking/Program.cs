using System;
using System.Linq;

namespace _19_Courier_Delivery_Tracking
{
    class Program
    {
        static void Main(string[] args)
        {
            CourierManager manager = new CourierManager();

            // ✅ Hardcoded Packages
            manager.AddPackage("Asad", "Rohit", "Mumbai, India",
                2.5, "Parcel", 450);

            manager.AddPackage("Anshul", "Yash", "Delhi, India",
                0.5, "Document", 150);

            manager.AddPackage("Neha", "Priya", "Bangalore, India",
                5, "Fragile", 800);

            // Simulate status updates
            manager.UpdateStatus("TRK1001", "InTransit", "Reached Mumbai Hub");

            while (true)
            {
                Console.WriteLine("\n=== Courier Delivery Tracking ===");
                Console.WriteLine("1. View Packages");
                Console.WriteLine("2. Update Delivery Status");
                Console.WriteLine("3. Group Packages By Type");
                Console.WriteLine("4. Search Packages By Destination");
                Console.WriteLine("5. View Delayed Packages");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var p in manager.Packages.Values)
                    {
                        var status = manager.Statuses[p.TrackingNumber];

                        Console.WriteLine($"{p.TrackingNumber} | {p.SenderName} -> {p.ReceiverName} | {status.CurrentStatus}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Tracking Number: ");
                    string tracking = Console.ReadLine();

                    Console.Write("New Status: ");
                    string status = Console.ReadLine();

                    Console.Write("Checkpoint: ");
                    string checkpoint = Console.ReadLine();

                    if (manager.UpdateStatus(tracking, status, checkpoint))
                        Console.WriteLine("Status updated!");
                    else
                        Console.WriteLine("Update failed!");
                }

                else if (choice == "3")
                {
                    var grouped = manager.GroupPackagesByType();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nType: {g.Key}");

                        foreach (var p in g.Value)
                            Console.WriteLine($"  {p.TrackingNumber} -> {p.ReceiverName}");
                    }
                }

                else if (choice == "4")
                {
                    Console.Write("Enter city: ");
                    string city = Console.ReadLine();

                    var results = manager.GetPackagesByDestination(city);

                    if (!results.Any())
                        Console.WriteLine("No packages found!");
                    else
                    {
                        foreach (var p in results)
                            Console.WriteLine($"{p.TrackingNumber} -> {p.DestinationAddress}");
                    }
                }

                else if (choice == "5")
                {
                    var delayed = manager.GetDelayedPackages();

                    if (!delayed.Any())
                        Console.WriteLine("No delayed packages!");
                    else
                    {
                        Console.WriteLine("\n⚠ Delayed Deliveries:");

                        foreach (var p in delayed)
                            Console.WriteLine($"{p.TrackingNumber} -> {p.DestinationAddress}");
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Exiting Courier System!");
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
