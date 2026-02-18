using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice.Models
{
    /// <summary>
    /// Grocery product class extending Product base class
    /// </summary>
    public class GroceryProduct : Product
    {
        /// <summary>
        /// Expiration/expiry date of the product
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Indicates if product is perishable
        /// </summary>
        public bool IsPerishable { get; set; }

        /// <summary>
        /// Weight of the product in kilograms
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Storage temperature requirement (e.g., "Room temperature", "Refrigerated", "Frozen")
        /// </summary>
        public string? StorageTemperature { get; set; }

        /// <summary>
        /// Returns formatted details for grocery product
        /// </summary>
        public override string GetProductDetails()
        {
            int daysLeft = DaysUntilExpiry();
            string expiryStatus = IsExpired() ? "EXPIRED" : $"Expires in {daysLeft} days";
            return $"Weight: {Weight} kg, Storage: {StorageTemperature}, Perishable: {IsPerishable}, {expiryStatus}, Expiry Date: {ExpiryDate:MM/dd/yyyy}";
        }

        /// <summary>
        /// Checks if product is expired
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        /// <summary>
        /// Calculates days until expiry (negative if expired)
        /// </summary>
        public int DaysUntilExpiry()
        {
            return (int)(ExpiryDate - DateTime.Now).TotalDays;
        }

        /// <summary>
        /// Calculates value with discount for near-expiry items (20% discount if within 3 days)
        /// </summary>
        public override decimal CalculateValue()
        {
            decimal baseValue = base.CalculateValue();
            
            // Apply 20% discount if within 3 days of expiry
            if (!IsExpired() && DaysUntilExpiry() <= 3 && DaysUntilExpiry() >= 0)
            {
                baseValue *= 0.80m; // 20% discount
            }
            
            // Don't count expired items in inventory value
            if (IsExpired())
            {
                return 0m;
            }

            return baseValue;
        }
    }
}