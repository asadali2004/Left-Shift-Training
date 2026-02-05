using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTheaterBooking
{
    public class TheaterManager
    {
        // Store all screenings
        public List<MovieScreening> Screenings = new List<MovieScreening>();
        
        // Add new screening
        public void AddScreening(string title, DateTime time, string screen, int seats, double price)
        {
            Screenings.Add(new MovieScreening
            {
                MovieTitle = title,
                ShowTime = time,
                ScreenNumber = screen,
                TotalSeats = seats,
                BookedSeats = 0,
                TicketPrice = price
            });
        }

        // Book tickets if seats available
        public bool BookTickets(string movieTitle, DateTime showTime, int tickets)
        {
            var screening = Screenings.FirstOrDefault(s => s.MovieTitle == movieTitle && s.ShowTime == showTime);
            if (screening != null && (screening.TotalSeats - screening.BookedSeats) >= tickets)
            {
                screening.BookedSeats += tickets;
                return true;
            }
            return false;
        }

        // Group screenings by movie title
        public Dictionary<string, List<MovieScreening>> GroupScreeningsByMovie()
        {
            return Screenings.GroupBy(s => s.MovieTitle).ToDictionary(g => g.Key, g => g.ToList());
        }

        // Calculate total revenue from all bookings
        public double CalculateTotalRevenue()
        {
            return Screenings.Sum(s => s.BookedSeats * s.TicketPrice);
        }

        // Get screenings with at least minSeats available
        public List<MovieScreening> GetAvailableScreenings(int minSeats)
        {
            return Screenings.Where(s => (s.TotalSeats - s.BookedSeats) >= minSeats).ToList();
        }
    }
}
