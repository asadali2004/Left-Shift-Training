using System;
using System.Linq;

namespace CarRental
{
    class Program
    {
        static void Main(string[] args)
        {
            RentalManager manager = new RentalManager();
            
            // Add some hardcoded test data
            manager.AddCar("ABC123", "Toyota", "Camry", "Sedan", 45.99);
            manager.AddCar("XYZ789", "Honda", "Accord", "Sedan", 48.99);
            manager.AddCar("DEF456", "Ford", "Explorer", "SUV", 75.99);
            manager.AddCar("GHI321", "Chevrolet", "Tahoe", "SUV", 85.99);
            manager.AddCar("JKL654", "Dodge", "Grand Caravan", "Van", 65.99);
            manager.AddCar("MNO987", "Chrysler", "Pacifica", "Van", 68.99);
            manager.AddCar("PQR147", "Nissan", "Altima", "Sedan", 42.99);
            manager.AddCar("STU258", "Jeep", "Wrangler", "SUV", 79.99);
            
            // Pre-book some rentals
            manager.RentCar("ABC123", "John Smith", DateTime.Now, 5);
            manager.RentCar("DEF456", "Sarah Johnson", DateTime.Now.AddDays(-2), 7);
            manager.RentCar("JKL654", "Mike Brown", DateTime.Now.AddDays(1), 3);
            
            while (true)
            {
                Console.WriteLine("\n=== Car Rental Agency ===");
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. View All Cars");
                Console.WriteLine("3. View Cars by Type");
                Console.WriteLine("4. Rent Car");
                Console.WriteLine("5. View Active Rentals");
                Console.WriteLine("6. View Total Revenue");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Car
                    Console.WriteLine("\n--- Add Car ---");
                    Console.Write("Enter License Plate: ");
                    string license = Console.ReadLine();
                    Console.Write("Enter Make: ");
                    string make = Console.ReadLine();
                    Console.Write("Enter Model: ");
                    string model = Console.ReadLine();
                    Console.Write("Enter Type (Sedan/SUV/Van): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter Daily Rate: ");
                    double rate = double.Parse(Console.ReadLine());
                    
                    manager.AddCar(license, make, model, type, rate);
                    Console.WriteLine("Car added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Cars
                    Console.WriteLine("\n--- All Cars ---");
                    if (manager.Cars.Count == 0)
                    {
                        Console.WriteLine("No cars found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-12} {1,-12} {2,-15} {3,-8} {4,-12} {5,-10}", "License", "Make", "Model", "Type", "Daily Rate", "Status");
                        Console.WriteLine(new string('-', 75));
                        foreach (var car in manager.Cars.Values)
                        {
                            string status = car.IsAvailable ? "Available" : "Rented";
                            Console.WriteLine("{0,-12} {1,-12} {2,-15} {3,-8} ${4,-11:F2} {5,-10}", 
                                car.LicensePlate, car.Make, car.Model, car.CarType, car.DailyRate, status);
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Cars by Type
                    Console.WriteLine("\n--- Cars by Type ---");
                    if (manager.Cars.Count == 0)
                    {
                        Console.WriteLine("No cars found!");
                    }
                    else
                    {
                        var grouped = manager.GroupCarsByType();
                        foreach (var type in grouped)
                        {
                            Console.WriteLine($"\n{type.Key} ({type.Value.Count} cars):");
                            foreach (var car in type.Value)
                            {
                                string status = car.IsAvailable ? "Available" : "Rented";
                                Console.WriteLine($"  {car.LicensePlate} - {car.Make} {car.Model} - ${car.DailyRate:F2}/day - {status}");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // Rent Car
                    Console.WriteLine("\n--- Rent Car ---");
                    if (manager.Cars.Count == 0)
                    {
                        Console.WriteLine("No cars available!");
                    }
                    else
                    {
                        var availableCars = manager.Cars.Values.Where(c => c.IsAvailable).ToList();
                        if (availableCars.Count == 0)
                        {
                            Console.WriteLine("No available cars for rent!");
                        }
                        else
                        {
                            Console.WriteLine("\nAvailable Cars:");
                            foreach (var car in availableCars)
                            {
                                Console.WriteLine($"{car.LicensePlate} - {car.Make} {car.Model} ({car.CarType}) - ${car.DailyRate:F2}/day");
                            }
                            
                            Console.Write("\nEnter License Plate: ");
                            string license = Console.ReadLine();
                            Console.Write("Enter Customer Name: ");
                            string customer = Console.ReadLine();
                            Console.Write("Enter Start Date (yyyy-mm-dd) or press Enter for today: ");
                            string dateInput = Console.ReadLine();
                            DateTime startDate = string.IsNullOrEmpty(dateInput) ? DateTime.Now : DateTime.Parse(dateInput);
                            Console.Write("Enter Number of Days: ");
                            int days = int.Parse(Console.ReadLine());
                            
                            if (manager.RentCar(license, customer, startDate, days))
                            {
                                double cost = manager.Cars[license].DailyRate * days;
                                Console.WriteLine($"\nRental successful! Total: ${cost:F2}");
                            }
                            else
                            {
                                Console.WriteLine("Rental failed! Car not available.");
                            }
                        }
                    }
                }
                else if (choice == "5")
                {
                    // View Active Rentals
                    Console.WriteLine("\n--- Active Rentals ---");
                    var activeRentals = manager.GetActiveRentals();
                    if (activeRentals.Count == 0)
                    {
                        Console.WriteLine("No active rentals!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-12} {2,-20} {3,-12} {4,-12} {5,-10}", "Rental#", "License", "Customer", "Start", "End", "Cost");
                        Console.WriteLine(new string('-', 80));
                        foreach (var rental in activeRentals)
                        {
                            Console.WriteLine("{0,-8} {1,-12} {2,-20} {3,-12:d} {4,-12:d} ${5,-9:F2}", 
                                rental.RentalId, rental.LicensePlate, rental.CustomerName, 
                                rental.StartDate, rental.EndDate, rental.TotalCost);
                        }
                    }
                }
                else if (choice == "6")
                {
                    // View Total Revenue
                    Console.WriteLine("\n--- Total Revenue ---");
                    double revenue = manager.CalculateTotalRentalRevenue();
                    int totalRentals = manager.Rentals.Count;
                    int activeRentals = manager.GetActiveRentals().Count;
                    Console.WriteLine($"Total Rentals: {totalRentals}");
                    Console.WriteLine($"Active Rentals: {activeRentals}");
                    Console.WriteLine($"Total Revenue: ${revenue:F2}");
                    if (totalRentals > 0)
                    {
                        Console.WriteLine($"Average Rental Value: ${revenue / totalRentals:F2}");
                    }
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
