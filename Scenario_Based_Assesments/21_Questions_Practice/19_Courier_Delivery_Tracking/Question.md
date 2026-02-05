# Question 19: Courier Delivery Tracking

## Scenario
A courier service needs to manage packages, deliveries, and tracking.

---

## Requirements

### Class: `Package`
- `string TrackingNumber`
- `string SenderName`
- `string ReceiverName`
- `string DestinationAddress`
- `double Weight`
- `string PackageType` (Document / Parcel / Fragile)
- `double ShippingCost`

---

### Class: `DeliveryStatus`
- `string TrackingNumber`
- `List<string> Checkpoints`
- `string CurrentStatus` (Dispatched / InTransit / Delivered)
- `DateTime EstimatedDelivery`
- `DateTime ActualDelivery`

---

### Class: `CourierManager`

```csharp
public void AddPackage(string sender, string receiver, string address,
                      double weight, string type, double cost);
public bool UpdateStatus(string trackingNumber, string status, 
                         string checkpoint);
public Dictionary<string, List<Package>> GroupPackagesByType();
public List<Package> GetPackagesByDestination(string city);
public List<Package> GetDelayedPackages();
````

---

## Use Cases

* Register packages for delivery
* Update delivery status
* Group packages by type
* Track packages by destination
* Identify delayed deliveries


