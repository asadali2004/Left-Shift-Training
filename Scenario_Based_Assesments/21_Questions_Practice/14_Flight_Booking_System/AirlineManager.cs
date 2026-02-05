using System;
using System.Collections.Generic;
using System.Linq;

namespace _14_Flight_Booking_System
{
    public class AirlineManager
    {
        public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();

        private int nextBookingId = 1;

        // Add new flight
        public void AddFlight(string number, string origin, string destination,
                             DateTime depart, DateTime arrive, int seats, double price)
        {
            Flights[number] = new Flight
            {
                FlightNumber = number,
                Origin = origin,
                Destination = destination,
                DepartureTime = depart,
                ArrivalTime = arrive,
                TotalSeats = seats,
                AvailableSeats = seats,
                TicketPrice = price
            };
        }

        // Book flight
        public bool BookFlight(string flightNumber, string passenger,
                               int seats, string seatClass)
        {
            if (!Flights.ContainsKey(flightNumber) || seats <= 0)
                return false;

            var flight = Flights[flightNumber];

            if (flight.AvailableSeats < seats)
                return false;

            // Business class costs 50% more
            double multiplier = seatClass.Equals("Business", StringComparison.OrdinalIgnoreCase) ? 1.5 : 1.0;

            double totalFare = seats * flight.TicketPrice * multiplier;

            Booking booking = new Booking
            {
                BookingId = "B" + nextBookingId++,
                FlightNumber = flightNumber,
                PassengerName = passenger,
                SeatsBooked = seats,
                SeatClass = seatClass,
                TotalFare = totalFare
            };

            flight.AvailableSeats -= seats;
            flight.Bookings.Add(booking);

            return true;
        }

        // Group by destination
        public Dictionary<string, List<Flight>> GroupFlightsByDestination()
        {
            return Flights.Values
                          .GroupBy(f => f.Destination)
                          .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Search flights
        public List<Flight> SearchFlights(string origin, string destination,
                                          DateTime date)
        {
            return Flights.Values
                .Where(f =>
                    f.Origin.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                    f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                    f.DepartureTime.Date == date.Date)
                .ToList();
        }

        // Calculate revenue
        public double CalculateTotalRevenue(string flightNumber)
        {
            if (!Flights.ContainsKey(flightNumber))
                return 0;

            return Flights[flightNumber]
                    .Bookings
                    .Sum(b => b.TotalFare);
        }
    }
}
