using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProductCatalog
{
    public class InventoryManager
    {
        // TODO: Add collection to store products
        
        public void AddProduct(string name, string category, double price, int stock)
        {
            // TODO: Auto-generate ProductCode (P001, P002...)
        }

        public SortedDictionary<string, List<Product>> GroupProductsByCategory()
        {
            // TODO: Groups products by category
            return new SortedDictionary<string, List<Product>>();
        }

        public bool UpdateStock(string productCode, int quantity)
        {
            // TODO: Updates stock, returns false if insufficient stock
            return false;
        }

        public List<Product> GetProductsBelowPrice(double maxPrice)
        {
            // TODO: Returns products below specified price
            return new List<Product>();
        }

        public Dictionary<string, int> GetCategoryStockSummary()
        {
            // TODO: Returns total stock quantity per category
            return new Dictionary<string, int>();
        }
    }
}
