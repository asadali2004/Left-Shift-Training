using System;
using System.Collections.Generic;

namespace CarRental
{
    public class RentalCar
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string CarType { get; set; }  // Sedan/SUV/Van
        public bool IsAvailable { get; set; }
        public double DailyRate { get; set; }
    }

    public class Rental
    {
        public int RentalId { get; set; }
        public string LicensePlate { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCost { get; set; }
    }
}
