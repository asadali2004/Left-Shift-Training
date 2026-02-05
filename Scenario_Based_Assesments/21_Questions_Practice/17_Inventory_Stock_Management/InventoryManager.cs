using System;
using System.Collections.Generic;
using System.Linq;

namespace _17_Inventory_Stock_Management
{
    public class InventoryManager
    {
        public Dictionary<string, Product> Products
            = new Dictionary<string, Product>();

        private int nextMovementId = 1;

        // Add new product
        public void AddProduct(string code, string name, string category,
                               string supplier, double price, int stock, int minLevel)
        {
            Products[code] = new Product
            {
                ProductCode = code,
                ProductName = name,
                Category = category,
                Supplier = supplier,
                UnitPrice = price,
                CurrentStock = stock,
                MinimumStockLevel = minLevel
            };
        }

        // Update stock
        public bool UpdateStock(string productCode, string movementType,
                                int quantity, string reason)
        {
            if (!Products.ContainsKey(productCode) || quantity <= 0)
                return false;

            var product = Products[productCode];

            if (movementType.Equals("Out", StringComparison.OrdinalIgnoreCase)
                && product.CurrentStock < quantity)
                return false;

            // Adjust stock
            if (movementType.Equals("In", StringComparison.OrdinalIgnoreCase))
                product.CurrentStock += quantity;
            else
                product.CurrentStock -= quantity;

            // Record movement
            product.Movements.Add(new StockMovement
            {
                MovementId = nextMovementId++,
                ProductCode = productCode,
                MovementDate = DateTime.Now,
                MovementType = movementType,
                Quantity = quantity,
                Reason = reason
            });

            return true;
        }

        // Group products by category
        public Dictionary<string, List<Product>> GroupProductsByCategory()
        {
            return Products.Values
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Low stock alert
        public List<Product> GetLowStockProducts()
        {
            return Products.Values
                .Where(p => p.CurrentStock <= p.MinimumStockLevel)
                .ToList();
        }

        // Inventory value by category
        public Dictionary<string, int> GetStockValueByCategory()
        {
            return Products.Values
                .GroupBy(p => p.Category)
                .ToDictionary(
                    g => g.Key,
                    g => (int)g.Sum(p => p.UnitPrice * p.CurrentStock)
                );
        }
    }
}
