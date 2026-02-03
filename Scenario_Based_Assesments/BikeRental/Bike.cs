namespace BikeRental
{
    public class Bike
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int PricePerDay { get; set; }
        
        public Bike(string model, string brand, int price)
        {
            Model = model;
            Brand = brand;
            PricePerDay = price;
        }
    }
}