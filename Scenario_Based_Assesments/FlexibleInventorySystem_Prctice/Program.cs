using System;
using FlexibleInventorySystem_Practice.Services;
using FlexibleInventorySystem_Practice.Models;


namespace FlexibleInventorySystem_Practice
{
    /// <summary>
    /// Main console application providing user interface for inventory management.
    /// Implements menu-driven console interface with options for adding, removing,
    /// updating products and generating reports.
    /// </summary>
    class Program
    {
        private static InventoryManager _inventory = new InventoryManager();

        static void Main(string[] args)
        {
            // Display menu and handle user input
            // Options include:
            // 1. Add Product
            // 2. Remove Product
            // 3. Update Quantity
            // 4. Find Product
            // 5. View All Products
            // 6. Generate Reports
            // 7. Check Low Stock
            // 8. Exit

            // Initialize sample data
            InitializeSampleData();

            try
            {
                while (true)
                {
                    DisplayMenu();
                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
                            AddProductMenu();
                            break;
                        case "2":
                            RemoveProductMenu();
                            break;
                        case "3":
                            UpdateQuantityMenu();
                            break;
                        case "4":
                            FindProductMenu();
                            break;
                        case "5":
                            ViewAllProductsMenu();
                            break;
                        case "6":
                            GenerateReportsMenu();
                            break;
                        case "7":
                            CheckLowStockMenu();
                            break;
                        case "8":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void InitializeSampleData()
        {
            // Add sample clothing products
            _inventory.AddProduct(new ClothingProduct
            {
                Id = "CLT001",
                Name = "Blue Jeans",
                Price = 1200,
                Quantity = 15,
                Category = "Clothing",
                DateAdded = DateTime.Now,
                Size = "M",
                Color = "Blue",
                Material = "Denim",
                Gender = "Men",
                Season = "All-season"
            });

            _inventory.AddProduct(new ClothingProduct
            {
                Id = "CLT002",
                Name = "Summer Dress",
                Price = 1500,
                Quantity = 8,
                Category = "Clothing",
                DateAdded = DateTime.Now,
                Size = "S",
                Color = "Red",
                Material = "Cotton",
                Gender = "Women",
                Season = "Summer"
            });

            // Add sample electronic products
            _inventory.AddProduct(new ElectronicProduct
            {
                Id = "ELC001",
                Name = "Laptop",
                Price = 75000,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V",
                IsRefurbished = false
            });

            _inventory.AddProduct(new ElectronicProduct
            {
                Id = "ELC002",
                Name = "Wireless Mouse",
                Price = 800,
                Quantity = 25,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Logitech",
                WarrantyMonths = 12,
                Voltage = "5V",
                IsRefurbished = false
            });

            // Add sample grocery products
            _inventory.AddProduct(new GroceryProduct
            {
                Id = "GRC001",
                Name = "Organic Apples",
                Price = 80,
                Quantity = 50,
                Category = "Groceries",
                DateAdded = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(15),
                IsPerishable = true,
                Weight = 1.5,
                StorageTemperature = "Room temperature"
            });

            _inventory.AddProduct(new GroceryProduct
            {
                Id = "GRC002",
                Name = "Frozen Vegetables Mix",
                Price = 150,
                Quantity = 20,
                Category = "Groceries",
                DateAdded = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(180),
                IsPerishable = true,
                Weight = 0.5,
                StorageTemperature = "Frozen"
            });

            Console.Clear();
            Console.WriteLine("Sample data loaded successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("========== FLEXIBLE INVENTORY SYSTEM ==========");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Find Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. Check Low Stock");
            Console.WriteLine("8. Exit");
            Console.WriteLine("==============================================");
            Console.WriteLine("Note: Run 'dotnet test' to execute unit tests");
            Console.Write("Choose an option: ");
        }

        static void AddProductMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("========== ADD PRODUCT ==========");
                Console.WriteLine("Select Product Type:");
                Console.WriteLine("1. Clothing Product");
                Console.WriteLine("2. Electronic Product");
                Console.WriteLine("3. Grocery Product");
                Console.Write("Enter choice: ");

                string type = Console.ReadLine() ?? "";

                Console.Write("Enter Product ID: ");
                string id = Console.ReadLine() ?? "";

                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Enter Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid price. Product not added.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid quantity. Product not added.");
                    Console.ReadKey();
                    return;
                }

                Product? product = null;

                switch (type)
                {
                    case "1":
                        product = CreateClothingProduct(id, name, price, quantity);
                        break;
                    case "2":
                        product = CreateElectronicProduct(id, name, price, quantity);
                        break;
                    case "3":
                        product = CreateGroceryProduct(id, name, price, quantity);
                        break;
                    default:
                        Console.WriteLine("Invalid product type.");
                        Console.ReadKey();
                        return;
                }

                if (product != null && _inventory.AddProduct(product))
                {
                    Console.WriteLine("Product added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add product.");
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                Console.ReadKey();
            }
        }

        static ClothingProduct CreateClothingProduct(string? id, string? name, decimal price, int quantity)
        {
            Console.Write("Enter Size (XS, S, M, L, XL, XXL): ");
            string? size = Console.ReadLine();

            Console.Write("Enter Color: ");
            string? color = Console.ReadLine();

            Console.Write("Enter Material: ");
            string? material = Console.ReadLine();

            Console.Write("Enter Gender (Men/Women/Unisex): ");
            string? gender = Console.ReadLine();

            Console.Write("Enter Season (Summer/Winter/All-season): ");
            string? season = Console.ReadLine();

            return new ClothingProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Clothing",
                DateAdded = DateTime.Now,
                Size = size,
                Color = color,
                Material = material,
                Gender = gender,
                Season = season
            };
        }

        static ElectronicProduct CreateElectronicProduct(string? id, string? name, decimal price, int quantity)
        {
            Console.Write("Enter Brand: ");
            string? brand = Console.ReadLine();

            Console.Write("Enter Warranty (months): ");
            if (!int.TryParse(Console.ReadLine(), out int warranty))
            {
                warranty = 12;
            }

            Console.Write("Enter Voltage: ");
            string? voltage = Console.ReadLine();

            Console.Write("Is Refurbished? (y/n): ");
            bool isRefurbished = Console.ReadLine()?.ToLower() == "y";

            return new ElectronicProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = brand,
                WarrantyMonths = warranty,
                Voltage = voltage,
                IsRefurbished = isRefurbished
            };
        }

        static GroceryProduct CreateGroceryProduct(string? id, string? name, decimal price, int quantity)
        {
            Console.Write("Enter Expiry Date (MM/dd/yyyy): ");
            DateTime expiryDate = DateTime.Now.AddMonths(6);
            if (DateTime.TryParse(Console.ReadLine(), out DateTime parsedDate))
            {
                expiryDate = parsedDate;
            }

            Console.Write("Is Perishable? (y/n): ");
            bool isPerishable = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Enter Weight (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                weight = 1.0;
            }

            Console.Write("Enter Storage Temperature (Room temperature/Refrigerated/Frozen): ");
            string? storage = Console.ReadLine();

            return new GroceryProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Grocery",
                DateAdded = DateTime.Now,
                ExpiryDate = expiryDate,
                IsPerishable = isPerishable,
                Weight = weight,
                StorageTemperature = storage
            };
        }

        static void RemoveProductMenu()
        {
            Console.Clear();
            Console.WriteLine("========== REMOVE PRODUCT ==========");
            Console.Write("Enter Product ID to remove: ");
            string? productId = Console.ReadLine();

            if (!string.IsNullOrEmpty(productId) && _inventory.RemoveProduct(productId))
            {
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found or removal failed.");
            }
            Console.ReadKey();
        }

        static void UpdateQuantityMenu()
        {
            Console.Clear();
            Console.WriteLine("========== UPDATE QUANTITY ==========");
            Console.Write("Enter Product ID: ");
            string? productId = Console.ReadLine();

            Console.Write("Enter new Quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                if (!string.IsNullOrEmpty(productId) && _inventory.UpdateQuantity(productId, quantity))
                {
                    Console.WriteLine("Quantity updated successfully!");
                }
                else
                {
                    Console.WriteLine("Product not found or update failed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid quantity.");
            }
            Console.ReadKey();
        }

        static void FindProductMenu()
        {
            Console.Clear();
            Console.WriteLine("========== FIND PRODUCT ==========");
            Console.Write("Enter Product ID: ");
            string? productId = Console.ReadLine();

            if (string.IsNullOrEmpty(productId))
            {
                Console.WriteLine("Product ID cannot be empty.");
                Console.ReadKey();
                return;
            }

            Product? product = _inventory.FindProduct(productId);
            if (product != null)
            {
                Console.WriteLine("\nProduct Found:");
                Console.WriteLine(product.ToString());
                Console.WriteLine("Details: " + product.GetProductDetails());
                Console.WriteLine("Inventory Value: $" + product.CalculateValue().ToString("F2"));
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
            Console.ReadKey();
        }

        static void ViewAllProductsMenu()
        {
            Console.Clear();
            Console.WriteLine("========== ALL PRODUCTS ==========");
            var allProducts = _inventory.SearchProducts(_ => true);

            if (allProducts.Any())
            {
                foreach (var product in allProducts)
                {
                    Console.WriteLine(product.ToString());
                }
            }
            else
            {
                Console.WriteLine("No products in inventory.");
            }
            Console.ReadKey();
        }

        static void GenerateReportsMenu()
        {
            Console.Clear();
            Console.WriteLine("========== GENERATE REPORTS ==========");
            Console.WriteLine("1. Inventory Report");
            Console.WriteLine("2. Category Summary");
            Console.WriteLine("3. Value Report");
            Console.WriteLine("4. Expiry Report");
            Console.Write("Choose report type: ");

            string reportChoice = Console.ReadLine() ?? "";

            Console.Clear();
            switch (reportChoice)
            {
                case "1":
                    Console.WriteLine(_inventory.GenerateInventoryReport());
                    break;
                case "2":
                    Console.WriteLine(_inventory.GenerateCategorySummary());
                    break;
                case "3":
                    Console.WriteLine(_inventory.GenerateValueReport());
                    break;
                case "4":
                    Console.Write("Enter expiry threshold (days): ");
                    if (int.TryParse(Console.ReadLine(), out int days))
                    {
                        Console.WriteLine(_inventory.GenerateExpiryReport(days));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid report type.");
                    break;
            }
            Console.ReadKey();
        }

        static void CheckLowStockMenu()
        {
            Console.Clear();
            Console.WriteLine("========== CHECK LOW STOCK ==========");
            Console.Write("Enter stock threshold: ");

            if (int.TryParse(Console.ReadLine(), out int threshold))
            {
                var lowStockProducts = _inventory.GetLowStockProducts(threshold);

                if (lowStockProducts.Any())
                {
                    Console.WriteLine("\nLow Stock Products:");
                    foreach (var product in lowStockProducts)
                    {
                        Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine("No products below the threshold.");
                }
            }
            else
            {
                Console.WriteLine("Invalid threshold.");
            }
            Console.ReadKey();
        }
    }
}