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

#### Method 1
```csharp
public void AddCar(string license, string make, string model, string type, double rate)
```
- Adds car to inventory

#### Method 2
```csharp
public bool RentCar(string license, string customer, DateTime start, int days)
```
- Creates rental if car available

#### Method 3
```csharp
public Dictionary<string, List<RentalCar>> GroupCarsByType()
```
- Groups available cars by type

#### Method 4
```csharp
public List<Rental> GetActiveRentals()
```
- Returns current rentals

#### Method 5
```csharp
public double CalculateTotalRentalRevenue()
```
- Sum of all rental costs

## Sample Use Cases:
1. Add cars to inventory
2. Process rentals
3. Check car availability by type
4. Track active rentals
5. Calculate revenue
