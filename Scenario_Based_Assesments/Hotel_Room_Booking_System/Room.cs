namespace Hotel_Room_Boooking_System
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public bool isAvailable { get; set; }
        
        public Room(int roomNo, string roomType, double price, bool isAvailable)
        {
            RoomNumber = roomNo;
            RoomType = roomType;
            PricePerNight = price;
            this.isAvailable = isAvailable;
        }
    }
}