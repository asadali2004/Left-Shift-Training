using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice.Models
{
    /// <summary>
    /// Abstract base class for all products in the inventory system
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Unique identifier for the product
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Date when product was added to inventory
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Abstract method to get product-specific details
        /// </summary>
        public abstract string GetProductDetails();

        /// <summary>
        /// Calculates total inventory value (Price * Quantity)
        /// Can be overridden by derived classes for special calculations
        /// </summary>
        public virtual decimal CalculateValue()
        {
            return Price * Quantity;
        }

        /// <summary>
        /// Returns formatted product summary
        /// </summary>
        public override string ToString()
        {
            return $"[{Id}] {Name} - Price: ${Price:F2}, Quantity: {Quantity}, Category: {Category}";
        }
    }
}