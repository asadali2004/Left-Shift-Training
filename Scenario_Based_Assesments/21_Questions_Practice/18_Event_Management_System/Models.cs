using System;
using System.Collections.Generic;

namespace _18_Event_Management_System
{
    // Represents an event
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; } // Concert / Conference / Workshop
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public int TotalCapacity { get; set; }
        public int TicketsSold { get; set; }
        public double TicketPrice { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    // Represents attendee
    public class Attendee
    {
        public int AttendeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<int> RegisteredEvents { get; set; }
            = new List<int>();
    }

    // Represents a ticket
    public class Ticket
    {
        public string TicketNumber { get; set; }
        public int EventId { get; set; }
        public int AttendeeId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string SeatNumber { get; set; }
    }
}
