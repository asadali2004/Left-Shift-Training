using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProductCatalog
{
    public class InventoryManager
    {
        // Store all products
        public Dictionary<string, Product> Products = new Dictionary<string, Product>();
        private int nextId = 1;
        
        // Add product with auto-generated code
        public void AddProduct(string name, string category, double price, int stock)
        {
            string productCode = $"P{nextId++:D3}";
            Products.Add(productCode, new Product
            {
                ProductCode = productCode,
                ProductName = name,
                Category = category,
                Price = price,
                StockQuantity = stock
            });
        }

        // Group products by category
        public SortedDictionary<string, List<Product>> GroupProductsByCategory()
        {
            var result = Products.Values.GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.ToList());
            return new SortedDictionary<string, List<Product>>(result);
        }

        // Update stock quantity
        public bool UpdateStock(string productCode, int quantity)
        {
            if (Products.ContainsKey(productCode))
            {
                if (Products[productCode].StockQuantity >= quantity)
                {
                    Products[productCode].StockQuantity -= quantity;
                    return true;
                }
            }
            return false;
        }

        // Get products below specified price
        public List<Product> GetProductsBelowPrice(double maxPrice)
        {
            return Products.Values.Where(p => p.Price < maxPrice).ToList();
        }

        // Get total stock quantity per category
        public Dictionary<string, int> GetCategoryStockSummary()
        {
            return Products.Values.GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.Sum(p => p.StockQuantity));
        }
    }
}
