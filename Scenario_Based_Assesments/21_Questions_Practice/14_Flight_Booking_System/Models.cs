using System;
using System.Collections.Generic;

namespace _14_Flight_Booking_System
{
    // Represents a flight
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public double TicketPrice { get; set; }

        // Track bookings for revenue
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }

    // Represents a booking
    public class Booking
    {
        public string BookingId { get; set; }
        public string FlightNumber { get; set; }
        public string PassengerName { get; set; }
        public int SeatsBooked { get; set; }
        public double TotalFare { get; set; }
        public string SeatClass { get; set; } // Economy / Business
    }
}
