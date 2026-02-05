# Question 14: Flight Booking System

## Scenario
An airline needs to manage flights, bookings, and passenger information.

---

## Requirements

### Class: `Flight`
- `string FlightNumber`
- `string Origin`
- `string Destination`
- `DateTime DepartureTime`
- `DateTime ArrivalTime`
- `int TotalSeats`
- `int AvailableSeats`
- `double TicketPrice`

---

### Class: `Booking`
- `string BookingId`
- `string FlightNumber`
- `string PassengerName`
- `int SeatsBooked`
- `double TotalFare`
- `string SeatClass` (Economy / Business)

---

### Class: `AirlineManager`

```csharp
public void AddFlight(string number, string origin, string destination,
                     DateTime depart, DateTime arrive, int seats, double price);
public bool BookFlight(string flightNumber, string passenger, 
                       int seats, string seatClass);
public Dictionary<string, List<Flight>> GroupFlightsByDestination();
public List<Flight> SearchFlights(string origin, string destination, 
                                  DateTime date);
public double CalculateTotalRevenue(string flightNumber);
````

---

## Use Cases

* Schedule flights
* Book tickets
* Search flights by route
* Group flights by destination
* Calculate flight revenue

