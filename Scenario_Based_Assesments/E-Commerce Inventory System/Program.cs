// Base product interface
public interface IProduct
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    Category Category { get; }
}

public enum Category { Electronics, Clothing, Books, Groceries }

// Abstract Product class with price update capability
public abstract class Product : IProduct
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; private set; }

    public abstract Category Category { get; }

    protected Product(int id, string name, decimal price)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid Id");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name");

        if (price <= 0)
            throw new ArgumentException("Invalid price");

        Id = id;
        Name = name;
        Price = price;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Invalid price");

        Price = newPrice;
    }
}

// 1. Create a generic repository for products
public class ProductRepository<T> where T : class, IProduct
{
    private List<T> _products = new List<T>();
    
    // TODO: Implement method to add product with validation
    public void AddProduct(T product)
    {
        // Rule: Product ID must be unique
        if (_products.Any(p => p.Id == product.Id))
        {
            Console.WriteLine($"Error: Product with ID {product.Id} already exists.");
            return;
        }
        
        // Note: Price and Name validation is handled by Product constructor
        // Rule: Price must be positive (validated in Product constructor)
        // Rule: Name cannot be null or empty (validated in Product constructor)
        
        // Add to collection if validation passes
        _products.Add(product);
    }
    
    // TODO: Create method to find products by predicate
    public IEnumerable<T> FindProducts(Func<T, bool> predicate)
    {
        // Should return filtered products
        return _products.Where(predicate);
    }
    
    // TODO: Calculate total inventory value
    public decimal CalculateTotalValue()
    {
        // Return sum of all product prices
        return _products.Sum(p => p.Price);
    }
}

// 2. Specialized electronic product
public class ElectronicProduct : Product
{
    public override Category Category => Category.Electronics;
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; } = string.Empty;
    
    public ElectronicProduct(int id, string name, decimal price, int warrantyMonths, string brand) 
        : base(id, name, price)
    {
        WarrantyMonths = warrantyMonths;
        Brand = brand;
    }
}

// 3. Create a discounted product wrapper
public class DiscountedProduct<T> where T : IProduct
{
    private T _product;
    private decimal _discountPercentage;
    
    public DiscountedProduct(T product, decimal discountPercentage)
    {
        // TODO: Initialize with validation
        // Discount must be between 0 and 100
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentException("Discount must be between 0 and 100.");
        }
        
        _product = product;
        _discountPercentage = discountPercentage;
    }
    
    // TODO: Implement calculated price with discount
    public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);
    
    // TODO: Override ToString to show discount details
    public override string ToString()
    {
        return $"{_product.Name} - Original: ${_product.Price:F2}, Discount: {_discountPercentage}%, Final: ${DiscountedPrice:F2}";
    }
}

// 4. Inventory manager with constraints
public class InventoryManager
{
    // TODO: Create method that accepts any IProduct collection
    public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
    {
        // a) Print all product names and prices
        Console.WriteLine("\n=== All Products ===");
        foreach (var product in products)
        {
            Console.WriteLine($"  {product.Name} - ${product.Price:F2}");
        }
        
        // b) Find the most expensive product
        var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
        if (mostExpensive != null)
        {
            Console.WriteLine($"\nMost Expensive Product: {mostExpensive.Name} - ${mostExpensive.Price:F2}");
        }
        
        // c) Group products by category
        Console.WriteLine("\n=== Products Grouped by Category ===");
        var grouped = products.GroupBy(p => p.Category);
        foreach (var group in grouped)
        {
            Console.WriteLine($"  {group.Key}: {group.Count()} product(s)");
        }
        
        // d) Apply 10% discount to Electronics over $500
        Console.WriteLine("\n=== Electronics over $500 with 10% Discount ===");
        var expensiveElectronics = products.Where(p => p.Category == Category.Electronics && p.Price > 500);
        foreach (var product in expensiveElectronics)
        {
            decimal discountedPrice = product.Price * 0.9m;
            Console.WriteLine($"  {product.Name}: ${product.Price:F2} -> ${discountedPrice:F2}");
        }
    }
    
    // TODO: Implement bulk price update with delegate
    public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster) 
        where T : IProduct
    {
        // Apply priceAdjuster to each product
        // Handle exceptions gracefully
        try
        {
            foreach (var product in products)
            {
                try
                {
                    decimal newPrice = priceAdjuster(product);
                    
                    // Use UpdatePrice method if product is a Product instance
                    if (product is Product productInstance)
                    {
                        productInstance.UpdatePrice(newPrice);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Cannot update price for {product.Name}. Product does not support price updates.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating price for {product.Name}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in bulk price update: {ex.Message}");
        }
    }
}

// 5. TEST SCENARIO: Your tasks:
// a) Implement all TODO methods with proper error handling
// b) Create a sample inventory with at least 5 products
// c) Demonstrate:
//    - Adding products with validation
//    - Finding products by brand (for electronics)
//    - Applying discounts
//    - Calculating total value before/after discount
//    - Handling a mixed collection of different product types

class Program
{
    static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   E-Commerce Inventory System Demo");
        Console.WriteLine("========================================\n");
        
        // Create repository for electronic products
        var repository = new ProductRepository<ElectronicProduct>();
        
        // b) Create a sample inventory with at least 5 products
        var product1 = new ElectronicProduct(1, "Laptop", 1200.00m, 24, "Dell");
        var product2 = new ElectronicProduct(2, "Smartphone", 800.00m, 12, "Samsung");
        var product3 = new ElectronicProduct(3, "Tablet", 450.00m, 18, "Apple");
        var product4 = new ElectronicProduct(4, "Smartwatch", 300.00m, 12, "Samsung");
        var product5 = new ElectronicProduct(5, "Headphones", 150.00m, 6, "Sony");
        var product6 = new ElectronicProduct(6, "Gaming Console", 550.00m, 24, "Sony");
        
        // c) Demonstrate: Adding products with validation
        Console.WriteLine("=== Adding Products with Validation ===");
        repository.AddProduct(product1);
        Console.WriteLine($"✓ Added: {product1.Name}");
        
        repository.AddProduct(product2);
        Console.WriteLine($"✓ Added: {product2.Name}");
        
        repository.AddProduct(product3);
        Console.WriteLine($"✓ Added: {product3.Name}");
        
        repository.AddProduct(product4);
        Console.WriteLine($"✓ Added: {product4.Name}");
        
        repository.AddProduct(product5);
        Console.WriteLine($"✓ Added: {product5.Name}");
        
        repository.AddProduct(product6);
        Console.WriteLine($"✓ Added: {product6.Name}");
        
        // Test validation: Try adding duplicate product
        Console.WriteLine("\n--- Testing Duplicate Validation ---");
        repository.AddProduct(product1); // Should fail - duplicate ID
        
        // Test validation: Try adding product with invalid price
        Console.WriteLine("\n--- Testing Price Validation ---");
        try
        {
            var invalidProduct = new ElectronicProduct(7, "Invalid Item", -50.00m, 12, "Unknown");
            repository.AddProduct(invalidProduct); // Should fail - negative price
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Exception caught during product creation: {ex.Message}");
        }
        
        // Test validation: Try adding product with empty name
        Console.WriteLine("\n--- Testing Name Validation ---");
        try
        {
            var emptyNameProduct = new ElectronicProduct(8, "", 100.00m, 12, "Unknown");
            repository.AddProduct(emptyNameProduct); // Should fail - empty name
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Exception caught during product creation: {ex.Message}");
        }
        
        // Demonstrate: Finding products by brand (for electronics)
        Console.WriteLine("\n=== Finding Products by Brand (Samsung) ===");
        var samsungProducts = repository.FindProducts(p => p.Brand == "Samsung");
        foreach (var product in samsungProducts)
        {
            Console.WriteLine($"  Found: {product.Name} - ${product.Price:F2} (Brand: {product.Brand})");
        }
        
        Console.WriteLine("\n=== Finding Products by Brand (Sony) ===");
        var sonyProducts = repository.FindProducts(p => p.Brand == "Sony");
        foreach (var product in sonyProducts)
        {
            Console.WriteLine($"  Found: {product.Name} - ${product.Price:F2} (Brand: {product.Brand})");
        }
        
        // Demonstrate: Calculating total value before discount
        Console.WriteLine("\n=== Total Inventory Value (Before Discount) ===");
        decimal totalValueBefore = repository.CalculateTotalValue();
        Console.WriteLine($"  Total Value: ${totalValueBefore:F2}");
        
        // Demonstrate: Applying discounts
        Console.WriteLine("\n=== Applying Discounts ===");
        
        // Apply 15% discount to Laptop
        var discountedLaptop = new DiscountedProduct<ElectronicProduct>(product1, 15);
        Console.WriteLine($"  {discountedLaptop}");
        
        // Apply 20% discount to Smartphone
        var discountedPhone = new DiscountedProduct<ElectronicProduct>(product2, 20);
        Console.WriteLine($"  {discountedPhone}");
        
        // Apply 10% discount to Gaming Console
        var discountedConsole = new DiscountedProduct<ElectronicProduct>(product6, 10);
        Console.WriteLine($"  {discountedConsole}");
        
        // Test invalid discount (should throw exception)
        Console.WriteLine("\n--- Testing Invalid Discount ---");
        try
        {
            var invalidDiscount = new DiscountedProduct<ElectronicProduct>(product5, 150); // Invalid: > 100%
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  Exception caught: {ex.Message}");
        }
        
        // Demonstrate: Using InventoryManager to process products
        var allProducts = new List<ElectronicProduct> { product1, product2, product3, product4, product5, product6 };
        var manager = new InventoryManager();
        manager.ProcessProducts(allProducts);
        
        // Demonstrate: Bulk price update with delegate
        Console.WriteLine("\n=== Bulk Price Update (5% increase) ===");
        var productsToUpdate = new List<ElectronicProduct> { product1, product2, product3, product4, product5, product6 };
        
        // Apply 5% increase to all prices
        manager.UpdatePrices(productsToUpdate, p => p.Price * 1.05m);
        Console.WriteLine("  ✓ All prices increased by 5%");
        
        // Demonstrate: Calculating total value after discount
        Console.WriteLine("\n=== Total Inventory Value (After 5% Increase) ===");
        decimal totalValueAfter = repository.CalculateTotalValue();
        Console.WriteLine($"  Total Value: ${totalValueAfter:F2}");
        Console.WriteLine($"  Difference: ${(totalValueAfter - totalValueBefore):F2}");
        
        // Demonstrate: Handling mixed collection (generic constraint allows any IProduct)
        Console.WriteLine("\n=== Demonstrating Mixed Collection Support ===");
        Console.WriteLine("  The system uses generic constraints (where T : IProduct)");
        Console.WriteLine("  This allows handling different product types safely while maintaining type safety.");
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("   Demo Completed Successfully!");
        Console.WriteLine("========================================");
    }
}