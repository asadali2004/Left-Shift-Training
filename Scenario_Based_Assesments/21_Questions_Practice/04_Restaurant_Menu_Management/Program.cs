using System;
using System.Linq;

namespace RestaurantMenuManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManager manager = new MenuManager();
            
            // Add some hardcoded test data
            manager.AddMenuItem("Spring Rolls", "Appetizer", 8.99, true);
            manager.AddMenuItem("Chicken Wings", "Appetizer", 12.99, false);
            manager.AddMenuItem("Caesar Salad", "Appetizer", 9.99, false);
            manager.AddMenuItem("Grilled Chicken", "Main Course", 18.99, false);
            manager.AddMenuItem("Vegetable Pasta", "Main Course", 15.99, true);
            manager.AddMenuItem("Beef Steak", "Main Course", 25.99, false);
            manager.AddMenuItem("Paneer Tikka", "Main Course", 16.99, true);
            manager.AddMenuItem("Chocolate Cake", "Dessert", 7.99, true);
            manager.AddMenuItem("Ice Cream", "Dessert", 5.99, true);
            manager.AddMenuItem("Tiramisu", "Dessert", 8.99, true);
            
            while (true)
            {
                Console.WriteLine("\n=== Restaurant Menu Management ===");
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. View All Menu Items");
                Console.WriteLine("3. View Menu by Category");
                Console.WriteLine("4. View Vegetarian Menu");
                Console.WriteLine("5. Calculate Average Price by Category");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Menu Item
                    Console.WriteLine("\n--- Add Menu Item ---");
                    Console.Write("Enter Item Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Category (Appetizer/Main Course/Dessert): ");
                    string category = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    double price = double.Parse(Console.ReadLine());
                    Console.Write("Is Vegetarian (y/n): ");
                    bool isVeg = Console.ReadLine().ToLower() == "y";
                    
                    manager.AddMenuItem(name, category, price, isVeg);
                    Console.WriteLine("Menu item added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Menu Items
                    Console.WriteLine("\n--- All Menu Items ---");
                    if (manager.MenuItems.Count == 0)
                    {
                        Console.WriteLine("No items found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-10} {4,-10}", "ID", "Name", "Category", "Price", "Veg");
                        Console.WriteLine(new string('-', 70));
                        foreach (var item in manager.MenuItems)
                        {
                            Console.WriteLine("{0,-5} {1,-25} {2,-15} ${3,-9:F2} {4,-10}", 
                                item.ItemId, item.ItemName, item.Category, item.Price, item.IsVegetarian ? "Yes" : "No");
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Menu by Category
                    Console.WriteLine("\n--- Menu by Category ---");
                    if (manager.MenuItems.Count == 0)
                    {
                        Console.WriteLine("No items found!");
                    }
                    else
                    {
                        var grouped = manager.GroupItemsByCategory();
                        foreach (var category in grouped)
                        {
                            Console.WriteLine($"\n{category.Key} ({category.Value.Count} items):");
                            foreach (var item in category.Value)
                            {
                                string veg = item.IsVegetarian ? "[V]" : "";
                                Console.WriteLine($"  {item.ItemId}. {item.ItemName} - ${item.Price:F2} {veg}");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // View Vegetarian Menu
                    Console.WriteLine("\n--- Vegetarian Menu ---");
                    var vegItems = manager.GetVegetarianItems();
                    if (vegItems.Count == 0)
                    {
                        Console.WriteLine("No vegetarian items found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-10}", "ID", "Name", "Category", "Price");
                        Console.WriteLine(new string('-', 60));
                        foreach (var item in vegItems)
                        {
                            Console.WriteLine("{0,-5} {1,-25} {2,-15} ${3,-9:F2}", 
                                item.ItemId, item.ItemName, item.Category, item.Price);
                        }
                    }
                }
                else if (choice == "5")
                {
                    // Calculate Average Price
                    Console.WriteLine("\n--- Calculate Average Price ---");
                    if (manager.MenuItems.Count == 0)
                    {
                        Console.WriteLine("No items found!");
                    }
                    else
                    {
                        var categories = manager.MenuItems.Select(m => m.Category).Distinct();
                        Console.WriteLine("Available Categories: " + string.Join(", ", categories));
                        Console.Write("\nEnter Category: ");
                        string category = Console.ReadLine();
                        double avg = manager.CalculateAveragePriceByCategory(category);
                        if (avg > 0)
                        {
                            Console.WriteLine($"Average Price for {category}: ${avg:F2}");
                        }
                        else
                        {
                            Console.WriteLine("Category not found!");
                        }
                    }
                }
                else if (choice == "6")
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
