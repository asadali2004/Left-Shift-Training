using System;
using System.Collections.Generic;

namespace _19_Courier_Delivery_Tracking
{
    // Represents a package
    public class Package
    {
        public string TrackingNumber { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string DestinationAddress { get; set; }
        public double Weight { get; set; }
        public string PackageType { get; set; } // Document / Parcel / Fragile
        public double ShippingCost { get; set; }
    }

    // Represents delivery status
    public class DeliveryStatus
    {
        public string TrackingNumber { get; set; }
        public List<string> Checkpoints { get; set; }
            = new List<string>();

        public string CurrentStatus { get; set; } // Dispatched / InTransit / Delivered
        public DateTime EstimatedDelivery { get; set; }
        public DateTime ActualDelivery { get; set; }
    }
}
