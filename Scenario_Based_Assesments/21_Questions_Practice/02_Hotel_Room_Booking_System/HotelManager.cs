using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_Room_Boooking_System
{
    // Manages hotel room operations including adding, booking, and searching rooms
    public class HotelManager
    {
        // Static dictionary storing all rooms with room number as key
        public static Dictionary<int, Room> RoomsDetail = new Dictionary<int, Room>();

        // Adds a new room to the hotel if room number doesn't already exist
        public void AddRoom(int roomNumber, string type, double price)
        {
            // Check if room number already exists before adding
            if (!RoomsDetail.ContainsKey(roomNumber))
            {
                RoomsDetail.Add(roomNumber, new Room(roomNumber, type, price, true));
            }
        }

        // Groups available rooms by their type
        public Dictionary<string, List<Room>> GroupRoomsByType()
        {
            // Filter available rooms, group by type, and convert to dictionary
            Dictionary<string, List<Room>> result = new Dictionary<string, List<Room>>();
            result = RoomsDetail.Values
                    .GroupBy(r => r.RoomType)
                    .ToDictionary(r => r.Key, r => r.Where(r => r.isAvailable).ToList());
            return result;
        }

        // Books a room if available and calculates total cost
        public bool BookRoom(int roomNumber, int nights)
        {
            double totalCost = 0;
            bool result = false;
            
            // Check if room exists and is available
            if (RoomsDetail.ContainsKey(roomNumber) && RoomsDetail[roomNumber].isAvailable == true)
            {
                // Calculate total cost and mark room as unavailable
                totalCost = RoomsDetail[roomNumber].PricePerNight * nights;
                Console.WriteLine($"Total Cost will be: ${totalCost:F2}");
                RoomsDetail[roomNumber].isAvailable = false;
                result = true;
            }
            return result;
        }

        // Returns list of available rooms within specified price range
        public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
        {
            // Filter rooms that are available and within the price range
            var result = RoomsDetail.Values.Where(r => r.isAvailable && r.PricePerNight >= min && r.PricePerNight <= max).ToList();
            return result;
        }
    }
}
