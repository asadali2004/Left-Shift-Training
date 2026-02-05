using System;
using System.Collections.Generic;
using System.Linq;

namespace _19_Courier_Delivery_Tracking
{
    public class CourierManager
    {
        public Dictionary<string, Package> Packages
            = new Dictionary<string, Package>();

        public Dictionary<string, DeliveryStatus> Statuses
            = new Dictionary<string, DeliveryStatus>();

        private int nextTrackingNumber = 1001;

        // Add package
        public void AddPackage(string sender, string receiver, string address,
                               double weight, string type, double cost)
        {
            string tracking = "TRK" + nextTrackingNumber++;

            Packages.Add(tracking, new Package
            {
                TrackingNumber = tracking,
                SenderName = sender,
                ReceiverName = receiver,
                DestinationAddress = address,
                Weight = weight,
                PackageType = type,
                ShippingCost = cost
            });

            Statuses.Add(tracking, new DeliveryStatus
            {
                TrackingNumber = tracking,
                CurrentStatus = "Dispatched",
                EstimatedDelivery = DateTime.Now.AddDays(5)
            });
        }

        // Update delivery status
        public bool UpdateStatus(string trackingNumber, string status,
                                 string checkpoint)
        {
            if (!Statuses.ContainsKey(trackingNumber))
                return false;

            var delivery = Statuses[trackingNumber];

            delivery.CurrentStatus = status;
            delivery.Checkpoints.Add($"{DateTime.Now:g} - {checkpoint}");

            if (status.Equals("Delivered", StringComparison.OrdinalIgnoreCase))
                delivery.ActualDelivery = DateTime.Now;

            return true;
        }

        // Group packages by type
        public Dictionary<string, List<Package>> GroupPackagesByType()
        {
            return Packages.Values
                .GroupBy(p => p.PackageType)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Packages by destination city
        public List<Package> GetPackagesByDestination(string city)
        {
            return Packages.Values
                .Where(p => p.DestinationAddress
                .Contains(city, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Delayed packages
        public List<Package> GetDelayedPackages()
        {
            return Statuses.Values
                .Where(s =>
                    s.CurrentStatus != "Delivered" &&
                    DateTime.Now > s.EstimatedDelivery)
                .Select(s => Packages[s.TrackingNumber])
                .ToList();
        }
    }
}
