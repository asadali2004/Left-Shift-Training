namespace Hotel_Room_Boooking_System
{
    public class UserInterface
    {
        public static void Main(string[] args)
        {
            HotelManager mgr = new HotelManager();
            InitializeRooms(mgr);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n1. Add Room  2. View Rooms by Type  3. Book Room  4. Price Range  5. Exit");
                Console.Write("Choice: ");
                
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Room Number: ");
                        if (int.TryParse(Console.ReadLine(), out int roomNo))
                        {
                            Console.Write("Room Type: ");
                            string? type = Console.ReadLine();
                            Console.Write("Price per Night: ");
                            if (double.TryParse(Console.ReadLine(), out double price))
                                mgr.AddRoom(roomNo, type ?? "", price);
                            Console.WriteLine("✓ Room added!");
                        }
                        break;
                    case "2":
                        foreach (var type in mgr.GroupRoomsByType())
                            Console.WriteLine($"{type.Key}: {type.Value.Count} available");
                        break;
                    case "3":
                        Console.Write("Room Number: ");
                        if (int.TryParse(Console.ReadLine(), out int room))
                        {
                            Console.Write("Nights: ");
                            if (int.TryParse(Console.ReadLine(), out int nights))
                                Console.WriteLine(mgr.BookRoom(room, nights) ? "✓ Booked!" : "❌ Not available");
                        }
                        break;
                    case "4":
                        Console.Write("Min Price: ");
                        if (double.TryParse(Console.ReadLine(), out double min))
                        {
                            Console.Write("Max Price: ");
                            if (double.TryParse(Console.ReadLine(), out double max))
                                foreach (var r in mgr.GetAvailableRoomsByPriceRange(min, max))
                                    Console.WriteLine($"Room {r.RoomNumber} - {r.RoomType}: ${r.PricePerNight}");
                        }
                        break;
                    case "5":
                        running = false;
                        break;
                }
            }
        }

        static void InitializeRooms(HotelManager mgr)
        {
            mgr.AddRoom(101, "Single", 50);
            mgr.AddRoom(102, "Double", 80);
            mgr.AddRoom(103, "Suite", 150);
            mgr.AddRoom(104, "Single", 50);
            mgr.AddRoom(105, "Double", 80);
        }
    }
}