# Question 2: Hotel Room Booking System

## 📘 Scenario
A hotel needs to manage room bookings and categorize rooms by type.

## 🛠️ Requirements

### In class Room, implement the following properties:
- `int RoomNumber`
- `string RoomType` (Single/Double/Suite)
- `double PricePerNight`
- `bool IsAvailable`

### In class HotelManager, implement the following methods:

#### Method 1
```csharp
public void AddRoom(int roomNumber, string type, double price)
```
- Adds room if room number doesn't exist

#### Method 2
```csharp
public Dictionary<string, List<Room>> GroupRoomsByType()
```
- Groups available rooms by type

#### Method 3
```csharp
public bool BookRoom(int roomNumber, int nights)
```
- Books room if available, calculates total cost

#### Method 4
```csharp
public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
```
- Returns available rooms within price range

## Sample Use Cases
1. Add different room types with prices
2. Display available rooms grouped by type
3. Book a room for specific nights
4. Find rooms within budget
