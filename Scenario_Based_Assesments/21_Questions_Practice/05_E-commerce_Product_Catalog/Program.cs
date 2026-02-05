using System;
using System.Linq;

namespace EcommerceProductCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();
            
            // Add some hardcoded test data
            manager.AddProduct("Laptop", "Electronics", 899.99, 15);
            manager.AddProduct("Smartphone", "Electronics", 699.99, 25);
            manager.AddProduct("Headphones", "Electronics", 149.99, 50);
            manager.AddProduct("T-Shirt", "Clothing", 29.99, 100);
            manager.AddProduct("Jeans", "Clothing", 59.99, 75);
            manager.AddProduct("Jacket", "Clothing", 129.99, 30);
            manager.AddProduct("C# Programming", "Books", 49.99, 40);
            manager.AddProduct("Python Basics", "Books", 39.99, 35);
            manager.AddProduct("Web Development", "Books", 44.99, 20);
            manager.AddProduct("Keyboard", "Electronics", 79.99, 45);
            
            while (true)
            {
                Console.WriteLine("\n=== E-commerce Product Catalog ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. View Products by Category");
                Console.WriteLine("4. Update Stock");
                Console.WriteLine("5. Find Products Below Price");
                Console.WriteLine("6. View Category Stock Summary");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Product
                    Console.WriteLine("\n--- Add Product ---");
                    Console.Write("Enter Product Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Category (Electronics/Clothing/Books): ");
                    string category = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    double price = double.Parse(Console.ReadLine());
                    Console.Write("Enter Stock Quantity: ");
                    int stock = int.Parse(Console.ReadLine());
                    
                    manager.AddProduct(name, category, price, stock);
                    Console.WriteLine("Product added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Products
                    Console.WriteLine("\n--- All Products ---");
                    if (manager.Products.Count == 0)
                    {
                        Console.WriteLine("No products found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-8} {1,-25} {2,-15} {3,-10} {4,-10}", "Code", "Name", "Category", "Price", "Stock");
                        Console.WriteLine(new string('-', 75));
                        foreach (var product in manager.Products.Values)
                        {
                            Console.WriteLine("{0,-8} {1,-25} {2,-15} ${3,-9:F2} {4,-10}", 
                                product.ProductCode, product.ProductName, product.Category, product.Price, product.StockQuantity);
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Products by Category
                    Console.WriteLine("\n--- Products by Category ---");
                    if (manager.Products.Count == 0)
                    {
                        Console.WriteLine("No products found!");
                    }
                    else
                    {
                        var grouped = manager.GroupProductsByCategory();
                        foreach (var category in grouped)
                        {
                            Console.WriteLine($"\n{category.Key} ({category.Value.Count} products):");
                            foreach (var product in category.Value)
                            {
                                Console.WriteLine($"  {product.ProductCode} - {product.ProductName} - ${product.Price:F2} - Stock: {product.StockQuantity}");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // Update Stock
                    Console.WriteLine("\n--- Update Stock ---");
                    if (manager.Products.Count == 0)
                    {
                        Console.WriteLine("No products found!");
                    }
                    else
                    {
                        Console.Write("Enter Product Code: ");
                        string code = Console.ReadLine();
                        Console.Write("Enter Quantity to Deduct: ");
                        int qty = int.Parse(Console.ReadLine());
                        
                        if (manager.UpdateStock(code, qty))
                        {
                            Console.WriteLine("Stock updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed! Product not found or insufficient stock.");
                        }
                    }
                }
                else if (choice == "5")
                {
                    // Find Products Below Price
                    Console.WriteLine("\n--- Find Products Below Price ---");
                    if (manager.Products.Count == 0)
                    {
                        Console.WriteLine("No products found!");
                    }
                    else
                    {
                        Console.Write("Enter Maximum Price: ");
                        double maxPrice = double.Parse(Console.ReadLine());
                        var products = manager.GetProductsBelowPrice(maxPrice);
                        
                        if (products.Count > 0)
                        {
                            Console.WriteLine($"\nFound {products.Count} products below ${maxPrice:F2}:");
                            Console.WriteLine("{0,-8} {1,-25} {2,-15} {3,-10}", "Code", "Name", "Category", "Price");
                            Console.WriteLine(new string('-', 65));
                            foreach (var product in products)
                            {
                                Console.WriteLine("{0,-8} {1,-25} {2,-15} ${3,-9:F2}", 
                                    product.ProductCode, product.ProductName, product.Category, product.Price);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No products found below this price!");
                        }
                    }
                }
                else if (choice == "6")
                {
                    // View Category Stock Summary
                    Console.WriteLine("\n--- Category Stock Summary ---");
                    if (manager.Products.Count == 0)
                    {
                        Console.WriteLine("No products found!");
                    }
                    else
                    {
                        var summary = manager.GetCategoryStockSummary();
                        Console.WriteLine("{0,-20} {1,-15}", "Category", "Total Stock");
                        Console.WriteLine(new string('-', 40));
                        foreach (var item in summary)
                        {
                            Console.WriteLine("{0,-20} {1,-15}", item.Key, item.Value);
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
