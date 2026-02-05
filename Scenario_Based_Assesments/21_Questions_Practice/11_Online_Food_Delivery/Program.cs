using System;
using System.Linq;

namespace OnlineFoodDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            DeliveryManager manager = new DeliveryManager();
            
            // Add some hardcoded test data
            manager.AddRestaurant("Pizza Palace", "Italian", "Downtown", 2.99);
            manager.AddRestaurant("Burger Barn", "American", "Midtown", 1.99);
            manager.AddRestaurant("Sushi Haven", "Japanese", "Uptown", 3.99);
            manager.AddRestaurant("Taco Fiesta", "Mexican", "Downtown", 1.50);
            manager.AddRestaurant("Curry House", "Indian", "East Side", 2.50);
            
            // Add food items
            manager.AddFoodItem(1, "Margherita Pizza", "Pizza", 12.99);
            manager.AddFoodItem(1, "Pepperoni Pizza", "Pizza", 14.99);
            manager.AddFoodItem(1, "Garlic Bread", "Appetizer", 4.99);
            manager.AddFoodItem(2, "Cheeseburger", "Burger", 9.99);
            manager.AddFoodItem(2, "Fries", "Side", 3.99);
            manager.AddFoodItem(2, "Milkshake", "Beverage", 3.50);
            manager.AddFoodItem(3, "California Roll", "Sushi", 8.99);
            manager.AddFoodItem(3, "Tempura", "Appetizer", 7.99);
            manager.AddFoodItem(4, "Tacos", "Main", 7.99);
            manager.AddFoodItem(4, "Quesadilla", "Main", 8.99);
            manager.AddFoodItem(5, "Chicken Tikka", "Main", 11.99);
            manager.AddFoodItem(5, "Biryani", "Main", 10.99);
            
            // Pre-order some items
            manager.PlaceOrder(101, new System.Collections.Generic.List<int> { 1, 3 });
            manager.PlaceOrder(102, new System.Collections.Generic.List<int> { 4, 5, 6 });
            manager.PlaceOrder(103, new System.Collections.Generic.List<int> { 7 });
            
            while (true)
            {
                Console.WriteLine("\n=== Online Food Delivery System ===");
                Console.WriteLine("1. View All Restaurants");
                Console.WriteLine("2. View Restaurants by Cuisine");
                Console.WriteLine("3. View Restaurant Menu");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. View Pending Orders");
                Console.WriteLine("6. View All Orders");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // View All Restaurants
                    Console.WriteLine("\n--- All Restaurants ---");
                    if (manager.Restaurants.Count == 0)
                    {
                        Console.WriteLine("No restaurants found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-10}", "ID", "Name", "Cuisine", "Location", "Delivery");
                        Console.WriteLine(new string('-', 70));
                        foreach (var restaurant in manager.Restaurants.Values)
                        {
                            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} ${4,-9:F2}", 
                                restaurant.RestaurantId, restaurant.Name, restaurant.CuisineType, 
                                restaurant.Location, restaurant.DeliveryCharge);
                        }
                    }
                }
                else if (choice == "2")
                {
                    // View Restaurants by Cuisine
                    Console.WriteLine("\n--- Restaurants by Cuisine ---");
                    if (manager.Restaurants.Count == 0)
                    {
                        Console.WriteLine("No restaurants found!");
                    }
                    else
                    {
                        var grouped = manager.GroupRestaurantsByCuisine();
                        foreach (var cuisine in grouped)
                        {
                            Console.WriteLine($"\n{cuisine.Key} ({cuisine.Value.Count} restaurants):");
                            foreach (var restaurant in cuisine.Value)
                            {
                                Console.WriteLine($"  {restaurant.RestaurantId}. {restaurant.Name} - {restaurant.Location} - Delivery: ${restaurant.DeliveryCharge:F2}");
                            }
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Restaurant Menu
                    Console.WriteLine("\n--- View Restaurant Menu ---");
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = int.Parse(Console.ReadLine());
                    
                    if (manager.Restaurants.ContainsKey(restaurantId))
                    {
                        var restaurant = manager.Restaurants[restaurantId];
                        var items = manager.FoodItems.Values.Where(i => i.RestaurantId == restaurantId).ToList();
                        
                        if (items.Count == 0)
                        {
                            Console.WriteLine("No items available!");
                        }
                        else
                        {
                            Console.WriteLine($"\n{restaurant.Name} Menu:");
                            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-10}", "ID", "Item", "Category", "Price");
                            Console.WriteLine(new string('-', 60));
                            foreach (var item in items)
                            {
                                Console.WriteLine("{0,-5} {1,-25} {2,-15} ${3,-9:F2}", 
                                    item.ItemId, item.Name, item.Category, item.Price);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Restaurant not found!");
                    }
                }
                else if (choice == "4")
                {
                    // Place Order
                    Console.WriteLine("\n--- Place Order ---");
                    Console.Write("Enter Customer ID: ");
                    int customerId = int.Parse(Console.ReadLine());
                    
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = int.Parse(Console.ReadLine());
                    
                    if (manager.Restaurants.ContainsKey(restaurantId))
                    {
                        var items = manager.FoodItems.Values.Where(i => i.RestaurantId == restaurantId).ToList();
                        if (items.Count > 0)
                        {
                            Console.WriteLine("\nAvailable Items:");
                            foreach (var item in items)
                            {
                                Console.WriteLine($"{item.ItemId}. {item.Name} - ${item.Price:F2}");
                            }
                            
                            Console.Write("\nEnter Item IDs (comma separated): ");
                            var itemInput = Console.ReadLine();
                            var itemIds = itemInput.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                            
                            if (manager.PlaceOrder(customerId, itemIds))
                            {
                                var order = manager.Orders.Last();
                                Console.WriteLine($"\nOrder placed successfully! Order ID: {order.OrderId}, Total: ${order.TotalAmount:F2}");
                            }
                            else
                            {
                                Console.WriteLine("Failed to place order!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Restaurant not found!");
                    }
                }
                else if (choice == "5")
                {
                    // View Pending Orders
                    Console.WriteLine("\n--- Pending Orders ---");
                    var pendingOrders = manager.GetPendingOrders();
                    if (pendingOrders.Count == 0)
                    {
                        Console.WriteLine("No pending orders!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-10} {2,-25} {3,-20} {4,-10}", "Order#", "Customer", "Items", "Order Time", "Total");
                        Console.WriteLine(new string('-', 80));
                        foreach (var order in pendingOrders)
                        {
                            var restaurantName = manager.Restaurants[order.Items[0].RestaurantId].Name;
                            string itemNames = string.Join(", ", order.Items.Select(i => i.Name));
                            Console.WriteLine("{0,-8} {1,-10} {2,-25} {3,-20:g} ${4,-9:F2}", 
                                order.OrderId, order.CustomerId, itemNames.Length > 25 ? itemNames.Substring(0, 22) + "..." : itemNames, 
                                order.OrderTime, order.TotalAmount);
                        }
                    }
                }
                else if (choice == "6")
                {
                    // View All Orders
                    Console.WriteLine("\n--- All Orders ---");
                    if (manager.Orders.Count == 0)
                    {
                        Console.WriteLine("No orders found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-10} {2,-20} {3,-10}", "Order#", "Customer", "Order Time", "Total");
                        Console.WriteLine(new string('-', 55));
                        foreach (var order in manager.Orders)
                        {
                            Console.WriteLine("{0,-8} {1,-10} {2,-20:g} ${3,-9:F2}", 
                                order.OrderId, order.CustomerId, order.OrderTime, order.TotalAmount);
                        }
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
