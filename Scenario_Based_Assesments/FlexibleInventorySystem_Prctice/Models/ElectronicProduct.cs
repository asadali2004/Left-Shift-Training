using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice.Models
{
    /// <summary>
    /// Electronic product class extending Product base class
    /// </summary>
    public class ElectronicProduct : Product
    {
        /// <summary>
        /// Brand of the electronic product
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// Warranty period in months
        /// </summary>
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Operating voltage (e.g., "110-240V", "220V")
        /// </summary>
        public string? Voltage { get; set; }

        /// <summary>
        /// Indicates if product is refurbished
        /// </summary>
        public bool IsRefurbished { get; set; }

        /// <summary>
        /// Returns formatted details for electronic product
        /// Format: "Brand: {Brand}, Model: {Name}, Warranty: {WarrantyMonths} months"
        /// </summary>
        public override string GetProductDetails()
        {
            string refurbished = IsRefurbished ? " (Refurbished)" : "";
            return $"Brand: {Brand}, Model: {Name}, Warranty: {WarrantyMonths} months, Voltage: {Voltage}{refurbished}";
        }

        /// <summary>
        /// Calculates warranty expiration date
        /// </summary>
        public DateTime GetWarrantyExpiryDate()
        {
            return DateAdded.AddMonths(WarrantyMonths);
        }

        /// <summary>
        /// Checks if warranty is still valid
        /// </summary>
        public bool IsWarrantyValid()
        {
            return DateTime.Now <= GetWarrantyExpiryDate();
        }
    }
}