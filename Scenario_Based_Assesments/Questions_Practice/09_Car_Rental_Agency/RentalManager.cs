using System;
using System.Collections.Generic;

namespace CarRental
{
    public class RentalManager
    {
        // TODO: Add collections
        public void AddCar(string license, string make, string model, string type, double rate) { }
        public bool RentCar(string license, string customer, DateTime start, int days) { return false; }
        public Dictionary<string, List<RentalCar>> GroupCarsByType() { return new Dictionary<string, List<RentalCar>>(); }
        public List<Rental> GetActiveRentals() { return new List<Rental>(); }
        public double CalculateTotalRentalRevenue() { return 0; }
    }
}
