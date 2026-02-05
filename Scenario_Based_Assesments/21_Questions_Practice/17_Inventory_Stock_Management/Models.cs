using System;
using System.Collections.Generic;

namespace _17_Inventory_Stock_Management
{
    // Represents a product in warehouse
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public double UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStockLevel { get; set; }

        // Track stock movements
        public List<StockMovement> Movements { get; set; }
            = new List<StockMovement>();
    }

    // Represents stock movement (In/Out)
    public class StockMovement
    {
        public int MovementId { get; set; }
        public string ProductCode { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } // In / Out
        public int Quantity { get; set; }
        public string Reason { get; set; } // Purchase / Sale / Return
    }
}
