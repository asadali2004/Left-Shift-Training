using System.Text.RegularExpressions;

namespace GlobalCargoSolutions.Models
{
    // Base class representing a shipment with core properties
    public class Shipment
    {
        // Unique identifier for the shipment (format: GC#XXXX)
        public string ShipmentCode { get; set; } = string.Empty;
        
        // Mode of transport (Sea, Air, or Land)
        public string TransportMode { get; set; } = string.Empty;
        
        // Weight of the shipment in kilograms
        public double Weight { get; set; }
        
        // Number of days the shipment will be stored
        public int StorageDays { get; set; }
    }
    
    // Derived class that extends Shipment with validation and cost calculation
    public class ShipmentDetails : Shipment
    {
        // Validates if the shipment code matches the pattern GC# followed by 4 digits
        public bool ValidateShipmentCode()
        {
            // Check if code matches the pattern: GC# + exactly 4 digits
            if (Regex.IsMatch(ShipmentCode, @"^GC#[0-9]{4}$"))
            {
                return true;
            }
            return false;
        }
        
        // Calculates total shipping cost based on weight, transport mode, and storage days
        public double CalculateTotalCost()
        {
            // Initialize rate per kilogram
            int RatePerKg = 0;
            
            // Determine rate based on transport mode
            if(TransportMode == "Sea")
            {
                RatePerKg = 15; // Sea transport rate: $15/kg
            }
            else if(TransportMode == "Air")
            {
                RatePerKg = 50; // Air transport rate: $50/kg
            }
            else if(TransportMode == "Land")
            {
                RatePerKg = 25; // Land transport rate: $25/kg
            }
            
            // Calculate total cost: (Weight × RatePerKg) + √StorageDays
            double result = (Weight * RatePerKg) + Math.Sqrt(StorageDays);

            // Round result to 2 decimal places
            return Math.Round(result, 2);
        }
    }
}