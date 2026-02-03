namespace Hotel_Room_Boooking_System
{
    public class HotelManager
    {
        public static Dictionary<int, Room> RoomsDetail = new Dictionary<int, Room>();

        // Adds room if room number doesn't exist
        public void AddRoom(int roomNumber, string type, double price)
        {
            RoomsDetail.Add(roomNumber, new Room(roomNumber, type, price, true));
        }

        // Groups available rooms by type
        public Dictionary<string, List<Room>> GroupRoomsByType()
        {
            Dictionary<string, List<Room>> result = new Dictionary<string, List<Room>>();
            result = RoomsDetail.Values
                    .GroupBy(r => r.RoomType)
                    .ToDictionary(r => r.Key, r => r.Where(r => r.isAvailable).ToList());
            return result;
        }

        // Books room if available, calculates total cost
        public bool BookRoom(int roomNumber, int nights)
        {
            double totalCost = 0;
            bool result = false;
                if (RoomsDetail[roomNumber].isAvailable == true)
                {
                totalCost = RoomsDetail[roomNumber].PricePerNight * nights;
                Console.WriteLine($"Total Cost will be: {totalCost}");
                RoomsDetail[roomNumber].isAvailable = false;
                result = true;
                }
            return result;
        }

        // Returns available rooms within price range
        public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
        {
            var result = RoomsDetail.Values.Where(r => r.isAvailable && r.PricePerNight >= min && r.PricePerNight <= max).ToList();
            return result;
        }
    }
}