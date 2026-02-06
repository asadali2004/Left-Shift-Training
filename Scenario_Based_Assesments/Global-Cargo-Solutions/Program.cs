using GlobalCargoSolutions.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Create an instance of ShipmentDetails to manage shipment data
        ShipmentDetails shipmentDetails = new ShipmentDetails();

        // Prompt user to enter shipment code
        System.Console.WriteLine("Enter you ShipmentCode: ");
        shipmentDetails.ShipmentCode = Console.ReadLine()!;
        
        // Validate the shipment code before proceeding
        if (shipmentDetails.ValidateShipmentCode())
        {
            // If valid, collect transport mode from user
            System.Console.WriteLine("Enter TransportMode(Air/Land/Sea): ");
            shipmentDetails.TransportMode = Console.ReadLine()!;
            
            // Collect shipment weight in kilograms
            System.Console.WriteLine("Enter Weight: ");
            shipmentDetails.Weight = double.Parse(Console.ReadLine()!);
            
            // Collect storage duration in days
            System.Console.WriteLine("Enter StorageDays: ");
            shipmentDetails.StorageDays = int.Parse(Console.ReadLine()!);

            // Calculate and display the total shipping cost
            double TotalCost = shipmentDetails.CalculateTotalCost();
            System.Console.WriteLine($"The total shipping cost is {TotalCost}");
        }
        else
        {
            // Display error message if shipment code is invalid
            System.Console.WriteLine("Invalid shipment code");
        }

    }
}