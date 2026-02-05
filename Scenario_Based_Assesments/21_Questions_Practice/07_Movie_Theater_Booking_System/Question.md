# Question 7: Movie Theater Booking System

## Scenario
A cinema needs to manage movie screenings and seat bookings.

## Requirements

### In class MovieScreening:
- `string MovieTitle`
- `DateTime ShowTime`
- `string ScreenNumber`
- `int TotalSeats`
- `int BookedSeats`
- `double TicketPrice`

### In class TheaterManager:

#### Method 1
```csharp
public void AddScreening(string title, DateTime time, string screen, int seats, double price)
```
- Adds new screening

#### Method 2
```csharp
public bool BookTickets(string movieTitle, DateTime showTime, int tickets)
```
- Books tickets if available seats

#### Method 3
```csharp
public Dictionary<string, List<MovieScreening>> GroupScreeningsByMovie()
```
- Groups screenings by movie title

#### Method 4
```csharp
public double CalculateTotalRevenue()
```
- Calculates total revenue from all bookings

#### Method 5
```csharp
public List<MovieScreening> GetAvailableScreenings(int minSeats)
```
- Returns screenings with at least minSeats available

## Sample Use Cases:
- Add multiple screenings for different movies
- Book tickets for specific show
- View all screenings of a particular movie
- Check available screenings for group booking
- Track revenue
