# Question 9: Car Rental Agency

## Scenario
A car rental company needs to manage its fleet and rentals.

## Requirements

### In class RentalCar:
- `string LicensePlate`
- `string Make`
- `string Model`
- `string CarType` (Sedan/SUV/Van)
- `bool IsAvailable`
- `double DailyRate`

### In class Rental:
- `int RentalId`
- `string LicensePlate`
- `string CustomerName`
- `DateTime StartDate`
- `DateTime EndDate`
- `double TotalCost`

### In class RentalManager:
- `void AddCar(string license, string make, string model, string type, double rate)`
- `bool RentCar(string license, string customer, DateTime start, int days)`
- `Dictionary<string, List<RentalCar>> GroupCarsByType()`
- `List<Rental> GetActiveRentals()`
- `double CalculateTotalRentalRevenue()`

## Sample Use Cases:
- Add cars to inventory
- Process rentals
- Check car availability by type
- Track active rentals
- Calculate revenue
