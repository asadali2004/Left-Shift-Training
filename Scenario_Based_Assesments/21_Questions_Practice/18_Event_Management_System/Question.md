````md
# Question 18: Event Management System

## Scenario
An event organizing company needs to manage events, tickets, and attendees.

---

## Requirements

### Class: `Event`
- `int EventId`
- `string EventName`
- `string EventType` (Concert / Conference / Workshop)
- `DateTime EventDate`
- `string Venue`
- `int TotalCapacity`
- `int TicketsSold`
- `double TicketPrice`

---

### Class: `Attendee`
- `int AttendeeId`
- `string Name`
- `string Email`
- `string Phone`
- `List<int> RegisteredEvents`

---

### Class: `Ticket`
- `string TicketNumber`
- `int EventId`
- `int AttendeeId`
- `DateTime PurchaseDate`
- `string SeatNumber`

---

### Class: `EventManager`

```csharp
public void CreateEvent(string name, string type, DateTime date,
                       string venue, int capacity, double price);
public bool BookTicket(int eventId, int attendeeId, string seatNumber);
public Dictionary<string, List<Event>> GroupEventsByType();
public List<Event> GetUpcomingEvents(int days);
public double CalculateEventRevenue(int eventId);
````

---

## Use Cases

* Create different types of events
* Book tickets for events
* Group events by type
* View upcoming events
* Calculate event revenue


