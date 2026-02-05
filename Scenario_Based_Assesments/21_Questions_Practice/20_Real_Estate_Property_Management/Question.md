
# Question 20: Real Estate Property Management

## Scenario
A real estate agency needs to manage properties, clients, and viewings.

---

## Requirements

### Class: `Property`
- `string PropertyId`
- `string Address`
- `string PropertyType` (Apartment / House / Villa)
- `int Bedrooms`
- `double AreaSqFt`
- `double Price`
- `string Status` (Available / Sold / Rented)
- `string Owner`

---

### Class: `Client`
- `int ClientId`
- `string Name`
- `string Contact`
- `string ClientType` (Buyer / Renter)
- `double Budget`
- `List<string> Requirements`

---

### Class: `Viewing`
- `int ViewingId`
- `string PropertyId`
- `int ClientId`
- `DateTime ViewingDate`
- `string Feedback`

---

### Class: `RealEstateManager`

```csharp
public void AddProperty(string address, string type, int bedrooms,
                       double area, double price, string owner);
public void AddClient(string name, string contact, string type, 
                      double budget, List<string> requirements);
public bool ScheduleViewing(string propertyId, int clientId, DateTime date);
public Dictionary<string, List<Property>> GroupPropertiesByType();
public List<Property> GetPropertiesInBudget(double minPrice, double maxPrice);
````

---

## Use Cases

* List properties for sale/rent
* Register clients with preferences
* Schedule property viewings
* Group properties by type
* Find properties within budget

