namespace Hotel_Room_Boooking_System
{
    // Represents a hotel room with its details and availability status
    public class Room
    {
        // Unique identifier for the room
        public int RoomNumber { get; set; }
        
        // Type of room (Single/Double/Suite)
        public string RoomType { get; set; }
        
        // Cost per night for the room
        public double PricePerNight { get; set; }
        
        // Indicates whether the room is available for booking
        public bool isAvailable { get; set; }
        
        // Constructor to initialize a room with all required details
        public Room(int roomNo, string roomType, double price, bool isAvailable)
        {
            RoomNumber = roomNo;
            RoomType = roomType;
            PricePerNight = price;
            this.isAvailable = isAvailable;
        }
    }
}
