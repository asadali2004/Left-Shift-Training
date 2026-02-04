namespace BikeRental
{
    public class Program
    {
        public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();

        public static void Main(string[] args)
        {
            BikeUtility util = new BikeUtility();
            int choice;
            bool run = true;
            
            while (run)
            {
                Console.WriteLine("1. Add Bike Details");
                Console.WriteLine("2. Group Bikes By Brand");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter your choice");
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the model");
                        string model = Console.ReadLine()!;
                        Console.WriteLine("Enter the brand");
                        string brand = Console.ReadLine()!;
                        Console.WriteLine("Enter the price per day");
                        int price = int.Parse(Console.ReadLine()!);
                        util.AddBikeDetails(model, brand, price);
                        Console.WriteLine("Bike details added successfully");
                        break;
                    case 2:
                        var GroupedBike = util.GroupBikesByBrand();
                        foreach (var item in GroupedBike)
                        {
                            Console.WriteLine(item.Key);
                            foreach(var i in item.Value)
                            {
                                Console.WriteLine(i.Model);
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        run = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid Input");
                        break;
                }
                
            }
        }
    }
}