using System;
using System.Collections.Generic;
using System.Linq;

namespace _18_Event_Management_System
{
    public class EventManager
    {
        public Dictionary<int, Event> Events = new Dictionary<int, Event>();
        public Dictionary<int, Attendee> Attendees = new Dictionary<int, Attendee>();

        private int nextEventId = 1;
        private int nextAttendeeId = 1;
        private int nextTicketNumber = 1001;

        // Create event
        public void CreateEvent(string name, string type, DateTime date,
                                string venue, int capacity, double price)
        {
            int id = nextEventId++;

            Events.Add(id, new Event
            {
                EventId = id,
                EventName = name,
                EventType = type,
                EventDate = date,
                Venue = venue,
                TotalCapacity = capacity,
                TicketPrice = price,
                TicketsSold = 0
            });
        }

        // Add attendee (helper)
        public void AddAttendee(string name, string email, string phone)
        {
            int id = nextAttendeeId++;

            Attendees.Add(id, new Attendee
            {
                AttendeeId = id,
                Name = name,
                Email = email,
                Phone = phone
            });
        }

        // Book ticket
        public bool BookTicket(int eventId, int attendeeId, string seatNumber)
        {
            if (!Events.ContainsKey(eventId) || !Attendees.ContainsKey(attendeeId))
                return false;

            var ev = Events[eventId];

            if (ev.TicketsSold >= ev.TotalCapacity)
                return false;

            // Prevent duplicate seat
            if (ev.Tickets.Any(t => t.SeatNumber == seatNumber))
                return false;

            Ticket ticket = new Ticket
            {
                TicketNumber = "T" + nextTicketNumber++,
                EventId = eventId,
                AttendeeId = attendeeId,
                PurchaseDate = DateTime.Now,
                SeatNumber = seatNumber
            };

            ev.Tickets.Add(ticket);
            ev.TicketsSold++;

            Attendees[attendeeId].RegisteredEvents.Add(eventId);

            return true;
        }

        // Group events by type
        public Dictionary<string, List<Event>> GroupEventsByType()
        {
            return Events.Values
                .GroupBy(e => e.EventType)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Upcoming events
        public List<Event> GetUpcomingEvents(int days)
        {
            DateTime limit = DateTime.Now.AddDays(days);

            return Events.Values
                .Where(e => e.EventDate >= DateTime.Now && e.EventDate <= limit)
                .OrderBy(e => e.EventDate)
                .ToList();
        }

        // Revenue
        public double CalculateEventRevenue(int eventId)
        {
            if (!Events.ContainsKey(eventId))
                return 0;

            var ev = Events[eventId];

            return ev.TicketsSold * ev.TicketPrice;
        }
    }
}
