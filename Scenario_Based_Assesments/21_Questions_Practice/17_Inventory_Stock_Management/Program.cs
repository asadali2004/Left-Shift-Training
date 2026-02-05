using System;
using System.Linq;

namespace _17_Inventory_Stock_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();

            // ✅ Hardcoded Products
            manager.AddProduct("P101", "Laptop", "Electronics",
                "Dell", 75000, 20, 5);

            manager.AddProduct("P102", "Office Chair", "Furniture",
                "Ikea", 5000, 15, 3);

            manager.AddProduct("P103", "Mouse", "Electronics",
                "Logitech", 800, 50, 10);

            // Simulate stock movements
            manager.UpdateStock("P101", "Out", 3, "Sale");
            manager.UpdateStock("P103", "Out", 45, "Sale"); // triggers low stock

            while (true)
            {
                Console.WriteLine("\n=== Inventory Stock Management ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Update Stock");
                Console.WriteLine("3. Group Products By Category");
                Console.WriteLine("4. View Low Stock Items");
                Console.WriteLine("5. Inventory Value By Category");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var p in manager.Products.Values)
                    {
                        Console.WriteLine($"{p.ProductCode} | {p.ProductName} | Stock: {p.CurrentStock} | Price: ₹{p.UnitPrice}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Product Code: ");
                    string code = Console.ReadLine();

                    Console.Write("Movement Type (In/Out): ");
                    string type = Console.ReadLine();

                    Console.Write("Quantity: ");
                    int qty = int.Parse(Console.ReadLine());

                    Console.Write("Reason: ");
                    string reason = Console.ReadLine();

                    if (manager.UpdateStock(code, type, qty, reason))
                        Console.WriteLine("Stock updated!");
                    else
                        Console.WriteLine("Stock update failed!");
                }

                else if (choice == "3")
                {
                    var grouped = manager.GroupProductsByCategory();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nCategory: {g.Key}");

                        foreach (var p in g.Value)
                            Console.WriteLine($"  {p.ProductName} (Stock: {p.CurrentStock})");
                    }
                }

                else if (choice == "4")
                {
                    var low = manager.GetLowStockProducts();

                    if (!low.Any())
                        Console.WriteLine("No low stock items!");
                    else
                    {
                        Console.WriteLine("\n⚠ LOW STOCK ALERT:");

                        foreach (var p in low)
                        {
                            Console.WriteLine($"{p.ProductName} | Stock: {p.CurrentStock} | Min: {p.MinimumStockLevel}");
                        }
                    }
                }

                else if (choice == "5")
                {
                    var values = manager.GetStockValueByCategory();

                    foreach (var v in values)
                    {
                        Console.WriteLine($"{v.Key} -> Total Value: ₹{v.Value}");
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Exiting Inventory System!");
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
