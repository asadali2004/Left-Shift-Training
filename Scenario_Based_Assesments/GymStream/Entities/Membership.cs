namespace GymStream.Entities
{
    // Represents a gym membership enrollment
    public class Membership
    {
        // Subscription plan (Basic, Premium, Elite)
        public string? Tier{ get; set; }
        // Duration of membership
        public int DurationInMonths { get; set; }

        // Standard monthly price before discount
        public double BasePricePerMonth{ get; set; }

    }
}
