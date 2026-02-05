using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental
{
    public class RentalManager
    {
        // Store all cars and rentals
        public Dictionary<string, RentalCar> Cars = new Dictionary<string, RentalCar>();
        public List<Rental> Rentals = new List<Rental>();
        private int nextRentalId = 1;
        
        // Add car to fleet
        public void AddCar(string license, string make, string model, string type, double rate)
        {
            Cars.Add(license, new RentalCar
            {
                LicensePlate = license,
                Make = make,
                Model = model,
                CarType = type,
                IsAvailable = true,
                DailyRate = rate
            });
        }
        
        // Rent car if available
        public bool RentCar(string license, string customer, DateTime start, int days)
        {
            if (Cars.ContainsKey(license) && Cars[license].IsAvailable)
            {
                Cars[license].IsAvailable = false;
                double totalCost = Cars[license].DailyRate * days;
                
                Rentals.Add(new Rental
                {
                    RentalId = nextRentalId++,
                    LicensePlate = license,
                    CustomerName = customer,
                    StartDate = start,
                    EndDate = start.AddDays(days),
                    TotalCost = totalCost
                });
                return true;
            }
            return false;
        }
        
        // Group cars by type
        public Dictionary<string, List<RentalCar>> GroupCarsByType()
        {
            return Cars.Values.GroupBy(c => c.CarType).ToDictionary(g => g.Key, g => g.ToList());
        }
        
        // Get active rentals
        public List<Rental> GetActiveRentals()
        {
            return Rentals.Where(r => r.EndDate >= DateTime.Now).ToList();
        }
        
        // Calculate total revenue
        public double CalculateTotalRentalRevenue()
        {
            return Rentals.Sum(r => r.TotalCost);
        }
    }
}
