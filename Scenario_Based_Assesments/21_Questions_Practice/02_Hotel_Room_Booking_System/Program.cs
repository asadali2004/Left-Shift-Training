using System;
using System.Linq;

namespace Hotel_Room_Boooking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create hotel manager instance for managing rooms
            HotelManager hotel = new HotelManager();
            bool exit = false;

            // Initialize hotel with sample rooms
            InitializeSampleRooms(hotel);

            // Main menu loop
            while (!exit)
            {
                Console.WriteLine("\n=== Hotel Room Booking System ===");
                Console.WriteLine("1. Add New Room");
                Console.WriteLine("2. View Available Rooms by Type");
                Console.WriteLine("3. Book a Room");
                Console.WriteLine("4. Find Rooms by Price Range");
                Console.WriteLine("5. View All Rooms");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter your choice (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewRoom(hotel);
                        break;
                    case "2":
                        ViewRoomsByType(hotel);
                        break;
                    case "3":
                        BookARoom(hotel);
                        break;
                    case "4":
                        FindRoomsByPriceRange(hotel);
                        break;
                    case "5":
                        ViewAllRooms();
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("\nThank you for using Hotel Room Booking System!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice! Please enter a number between 1 and 6.");
                        break;
                }
            }
        }

        // Initializes the hotel with sample rooms
        static void InitializeSampleRooms(HotelManager hotel)
        {
            hotel.AddRoom(101, "Single", 50.0);
            hotel.AddRoom(102, "Single", 55.0);
            hotel.AddRoom(201, "Double", 85.0);
            hotel.AddRoom(202, "Double", 90.0);
            hotel.AddRoom(301, "Suite", 150.0);
            hotel.AddRoom(302, "Suite", 175.0);
        }

        // Menu option 1: Add a new room to the hotel
        static void AddNewRoom(HotelManager hotel)
        {
            Console.WriteLine("\n--- Add New Room ---");
            
            // Get room number from user with validation
            Console.Write("Enter Room Number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("\nInvalid room number!");
                return;
            }
            
            // Check if room already exists
            if (HotelManager.RoomsDetail.ContainsKey(roomNumber))
            {
                Console.WriteLine("\nRoom number already exists!");
                return;
            }
            
            // Get room type from user
            Console.Write("Enter Room Type (Single/Double/Suite): ");
            string roomType = Console.ReadLine();
            
            // Get price per night with validation
            Console.Write("Enter Price Per Night: $");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("\nInvalid price!");
                return;
            }
            
            hotel.AddRoom(roomNumber, roomType, price);
            Console.WriteLine("\nRoom added successfully!");
        }

        // Menu option 2: Display available rooms grouped by type
        static void ViewRoomsByType(HotelManager hotel)
        {
            Console.WriteLine("\n--- Available Rooms by Type ---");
            
            // Get rooms grouped by type
            var roomsByType = hotel.GroupRoomsByType();
            
            // Check if there are any available rooms
            if (roomsByType.Count == 0)
            {
                Console.WriteLine("No available rooms.");
                return;
            }

            // Display rooms organized by type
            foreach (var type in roomsByType)
            {
                // Only display types that have available rooms
                if (type.Value.Count > 0)
                {
                    Console.WriteLine($"\n{type.Key}:");
                    foreach (var room in type.Value)
                    {
                        Console.WriteLine($"  Room {room.RoomNumber} - ${room.PricePerNight:F2} per night");
                    }
                }
            }
        }

        // Menu option 3: Book a room for specified number of nights
        static void BookARoom(HotelManager hotel)
        {
            Console.WriteLine("\n--- Book a Room ---");
            
            // Get room number to book
            Console.Write("Enter Room Number to Book: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("\nInvalid room number!");
                return;
            }
            
            // Check if room exists
            if (!HotelManager.RoomsDetail.ContainsKey(roomNumber))
            {
                Console.WriteLine("\nRoom not found!");
                return;
            }
            
            // Check if room is available
            if (!HotelManager.RoomsDetail[roomNumber].isAvailable)
            {
                Console.WriteLine("\nRoom is already booked!");
                return;
            }
            
            // Get number of nights
            Console.Write("Enter Number of Nights: ");
            if (!int.TryParse(Console.ReadLine(), out int nights) || nights <= 0)
            {
                Console.WriteLine("\nInvalid number of nights!");
                return;
            }
            
            // Attempt to book the room
            if (hotel.BookRoom(roomNumber, nights))
            {
                Console.WriteLine("\nRoom booked successfully!");
            }
            else
            {
                Console.WriteLine("\nFailed to book the room!");
            }
        }

        // Menu option 4: Find available rooms within a price range
        static void FindRoomsByPriceRange(HotelManager hotel)
        {
            Console.WriteLine("\n--- Find Rooms by Price Range ---");
            
            // Get minimum price
            Console.Write("Enter Minimum Price: $");
            if (!double.TryParse(Console.ReadLine(), out double minPrice))
            {
                Console.WriteLine("\nInvalid minimum price!");
                return;
            }
            
            // Get maximum price
            Console.Write("Enter Maximum Price: $");
            if (!double.TryParse(Console.ReadLine(), out double maxPrice))
            {
                Console.WriteLine("\nInvalid maximum price!");
                return;
            }
            
            // Validate price range
            if (minPrice > maxPrice)
            {
                Console.WriteLine("\nMinimum price cannot be greater than maximum price!");
                return;
            }
            
            // Get rooms within price range
            var rooms = hotel.GetAvailableRoomsByPriceRange(minPrice, maxPrice);
            
            // Display results
            if (rooms.Count > 0)
            {
                Console.WriteLine($"\nAvailable rooms between ${minPrice:F2} and ${maxPrice:F2}:");
                foreach (var room in rooms)
                {
                    Console.WriteLine($"  Room {room.RoomNumber} ({room.RoomType}) - ${room.PricePerNight:F2} per night");
                }
            }
            else
            {
                Console.WriteLine($"\nNo available rooms found in the price range ${minPrice:F2} - ${maxPrice:F2}.");
            }
        }

        // Menu option 5: Display all rooms with their status
        static void ViewAllRooms()
        {
            Console.WriteLine("\n--- All Rooms ---");
            
            // Check if hotel has any rooms
            if (HotelManager.RoomsDetail.Count == 0)
            {
                Console.WriteLine("No rooms in the hotel.");
                return;
            }
            
            // Display all rooms with their availability status
            Console.WriteLine($"\n{"Room",-8} {"Type",-10} {"Price/Night",-15} {"Status",-10}");
            Console.WriteLine(new string('-', 45));
            
            foreach (var room in HotelManager.RoomsDetail.Values.OrderBy(r => r.RoomNumber))
            {
                string status = room.isAvailable ? "Available" : "Booked";
                Console.WriteLine($"{room.RoomNumber,-8} {room.RoomType,-10} ${room.PricePerNight,-14:F2} {status,-10}");
            }
        }
    }
}
