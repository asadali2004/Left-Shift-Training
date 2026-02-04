namespace BikeRental
{
    public class BikeUtility
    {
        public void AddBikeDetails(string model, string brand, int pricePerDay)
        {
            Program.bikeDetails.Add(Program.bikeDetails.Count + 1, new Bike(model, brand, pricePerDay));
        }

        public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
        {
            var result = Program.bikeDetails.Values
                                            .GroupBy(e => e.Brand)
                                            .ToDictionary(e => e.Key, e => e.ToList());
            return new SortedDictionary<string, List<Bike>>(result);
        }
    }
}