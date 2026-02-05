using System;

namespace MovieTheaterBooking
{
    public class MovieScreening
    {
        public string MovieTitle { get; set; }
        public DateTime ShowTime { get; set; }
        public string ScreenNumber { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public double TicketPrice { get; set; }
    }
}
