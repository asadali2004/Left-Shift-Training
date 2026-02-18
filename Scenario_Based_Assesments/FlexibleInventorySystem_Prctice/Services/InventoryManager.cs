using FlexibleInventorySystem_Practice.Interfaces;
using FlexibleInventorySystem_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice.Services
{  
    public class InventoryManager : IInventoryOperations, IReportGenerator
    {
        private readonly List<Product> _products;
        private readonly object _lockObject = new object();

        public InventoryManager()
        {
            _products = new List<Product>();
        }

        /// <summary>
        /// Adds a new product to inventory
        /// </summary>
        public bool AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            lock (_lockObject)
            {
                // Check if product already exists
                if (_products.Any(p => p.Id == product.Id))
                    return false;

                _products.Add(product);
                return true;
            }
        }

        /// <summary>
        /// Removes a product from inventory
        /// </summary>
        public bool RemoveProduct(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                return false;

            lock (_lockObject)
            {
                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    return false;

                return _products.Remove(product);
            }
        }

        /// <summary>
        /// Finds a product by ID
        /// </summary>
        public Product? FindProduct(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                return null;

            lock (_lockObject)
            {
                return _products.FirstOrDefault(p => p.Id == productId);
            }
        }

        /// <summary>
        /// Gets all products in a specific category
        /// </summary>
        public List<Product> GetProductsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return new List<Product>();

            lock (_lockObject)
            {
                return _products.Where(p => (p.Category ?? "").Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        /// <summary>
        /// Updates quantity of a product
        /// </summary>
        public bool UpdateQuantity(string productId, int newQuantity)
        {
            if (string.IsNullOrEmpty(productId) || newQuantity < 0)
                return false;

            lock (_lockObject)
            {
                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    return false;

                product.Quantity = newQuantity;
                return true;
            }
        }

        /// <summary>
        /// Calculates total inventory value
        /// </summary>
        public decimal GetTotalInventoryValue()
        {
            lock (_lockObject)
            {
                return _products.Sum(p => p.CalculateValue());
            }
        }

        /// <summary>
        /// Gets products with quantity below threshold
        /// </summary>
        public List<Product> GetLowStockProducts(int threshold)
        {
            lock (_lockObject)
            {
                return _products.Where(p => p.Quantity < threshold).ToList();
            }
        }

        /// <summary>
        /// Generates complete inventory report
        /// </summary>
        public string GenerateInventoryReport()
        {
            lock (_lockObject)
            {
                StringBuilder report = new StringBuilder();
                report.AppendLine("========== INVENTORY REPORT ==========");
                report.AppendLine($"Total Products: {_products.Count}");
                report.AppendLine($"Total Inventory Value: ${GetTotalInventoryValue():F2}");
                report.AppendLine("=====================================");
                
                if (_products.Count > 0)
                {
                    foreach (var product in _products)
                    {
                        report.AppendLine(product.ToString());
                        report.AppendLine($"  Details: {product.GetProductDetails()}");
                        report.AppendLine($"  Inventory Value: ${product.CalculateValue():F2}");
                    }
                }
                else
                {
                    report.AppendLine("No products in inventory.");
                }

                return report.ToString();
            }
        }

        /// <summary>
        /// Generates summary grouped by category
        /// </summary>
        public string GenerateCategorySummary()
        {
            lock (_lockObject)
            {
                StringBuilder report = new StringBuilder();
                report.AppendLine("========== CATEGORY SUMMARY ==========");

                var categories = _products.GroupBy(p => p.Category);

                if (categories.Any())
                {
                    foreach (var category in categories)
                    {
                        decimal categoryValue = category.Sum(p => p.CalculateValue());
                        int categoryQuantity = category.Sum(p => p.Quantity);
                        
                        report.AppendLine($"\nCategory: {category.Key}");
                        report.AppendLine($"  Products: {category.Count()}");
                        report.AppendLine($"  Total Quantity: {categoryQuantity}");
                        report.AppendLine($"  Category Value: ${categoryValue:F2}");
                    }
                }
                else
                {
                    report.AppendLine("No categories found.");
                }

                return report.ToString();
            }
        }

        /// <summary>
        /// Generates report of most/least valuable products
        /// </summary>
        public string GenerateValueReport()
        {
            lock (_lockObject)
            {
                StringBuilder report = new StringBuilder();
                report.AppendLine("========== VALUE REPORT ==========");

                if (_products.Count == 0)
                {
                    report.AppendLine("No products in inventory.");
                    return report.ToString();
                }

                var sortedByValue = _products.OrderByDescending(p => p.CalculateValue()).ToList();

                report.AppendLine("\nTop 5 Most Valuable Products:");
                foreach (var product in sortedByValue.Take(5))
                {
                    report.AppendLine($"  {product.ToString()} - Value: ${product.CalculateValue():F2}");
                }

                report.AppendLine("\nTop 5 Least Valuable Products:");
                foreach (var product in sortedByValue.Reverse<Product>().Take(5))
                {
                    report.AppendLine($"  {product.ToString()} - Value: ${product.CalculateValue():F2}");
                }

                return report.ToString();
            }
        }

        /// <summary>
        /// Generates report of expiring products (for groceries)
        /// </summary>
        public string GenerateExpiryReport(int daysThreshold)
        {
            lock (_lockObject)
            {
                StringBuilder report = new StringBuilder();
                report.AppendLine($"========== EXPIRY REPORT (Next {daysThreshold} days) ==========");

                var groceryProducts = _products.OfType<GroceryProduct>().ToList();

                if (groceryProducts.Count == 0)
                {
                    report.AppendLine("No grocery products in inventory.");
                    return report.ToString();
                }

                var expiringProducts = groceryProducts
                    .Where(p => p.DaysUntilExpiry() <= daysThreshold && p.DaysUntilExpiry() >= 0)
                    .OrderBy(p => p.DaysUntilExpiry())
                    .ToList();

                if (expiringProducts.Any())
                {
                    report.AppendLine($"Total expiring products: {expiringProducts.Count}");
                    report.AppendLine("-----------------------------------");

                    foreach (var product in expiringProducts)
                    {
                        report.AppendLine($"{product.ToString()}");
                        report.AppendLine($"  Days until expiry: {product.DaysUntilExpiry()}");
                        report.AppendLine($"  Expiry Date: {product.ExpiryDate:MM/dd/yyyy}");
                        report.AppendLine();
                    }
                }
                else
                {
                    report.AppendLine($"No products expiring within {daysThreshold} days.");
                }

                var expiredProducts = groceryProducts.Where(p => p.IsExpired()).ToList();
                if (expiredProducts.Any())
                {
                    report.AppendLine("\nEXPIRED PRODUCTS:");
                    foreach (var product in expiredProducts)
                    {
                        report.AppendLine($"  {product.ToString()} - EXPIRED on {product.ExpiryDate:MM/dd/yyyy}");
                    }
                }

                return report.ToString();
            }
        }

        /// <summary>
        /// Additional method for bonus features - search products by predicate
        /// </summary>
        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate)
        {
            lock (_lockObject)
            {
                return _products.Where(predicate).ToList();
            }
        }
    }    
}
