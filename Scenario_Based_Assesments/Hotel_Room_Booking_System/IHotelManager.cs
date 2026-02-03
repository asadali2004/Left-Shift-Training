namespace Hotel_Room_Boooking_System
{
    public interface IHotelManager
    {
        void AddRoom(int roomNumber, string type, double price);

        Dictionary<string, List<Room>> GroupRoomsByType();

        bool BookRoom(int roomNumber, int nights);

        List<Room> GetAvailableRoomsByPriceRange(double min, double max);
    }
}