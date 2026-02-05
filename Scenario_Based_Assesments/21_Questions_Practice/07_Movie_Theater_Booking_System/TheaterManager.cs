using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTheaterBooking
{
    public class TheaterManager
    {
        // TODO: Add collection to store screenings
        
        public void AddScreening(string title, DateTime time, string screen, int seats, double price)
        {
            // TODO: Adds new screening
        }

        public bool BookTickets(string movieTitle, DateTime showTime, int tickets)
        {
            // TODO: Books tickets if available seats
            return false;
        }

        public Dictionary<string, List<MovieScreening>> GroupScreeningsByMovie()
        {
            // TODO: Groups screenings by movie title
            return new Dictionary<string, List<MovieScreening>>();
        }

        public double CalculateTotalRevenue()
        {
            // TODO: Calculates total revenue from all bookings
            return 0;
        }

        public List<MovieScreening> GetAvailableScreenings(int minSeats)
        {
            // TODO: Returns screenings with at least minSeats available
            return new List<MovieScreening>();
        }
    }
}
