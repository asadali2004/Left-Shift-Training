# Question 11: Online Food Delivery

## Scenario
A food delivery service needs to manage restaurants, menus, and orders.

## Requirements

### In class Restaurant:
- `int RestaurantId`
- `string Name`
- `string CuisineType`
- `string Location`
- `double DeliveryCharge`

### In class FoodItem:
- `int ItemId`
- `string Name`
- `string Category`
- `double Price`
- `int RestaurantId`

### In class Order:
- `int OrderId`
- `int CustomerId`
- `List<FoodItem> Items`
- `DateTime OrderTime`
- `string Status`
- `double TotalAmount`

### In class DeliveryManager:

#### Method 1
```csharp
public void AddRestaurant(string name, string cuisine, string location, double charge)
```

#### Method 2
```csharp
public void AddFoodItem(int restaurantId, string name, string category, double price)
```

#### Method 3
```csharp
public Dictionary<string, List<Restaurant>> GroupRestaurantsByCuisine()
```

#### Method 4
```csharp
public bool PlaceOrder(int customerId, List<int> itemIds)
```

#### Method 5
```csharp
public List<Order> GetPendingOrders()
```

## Sample Use Cases:
1. Add restaurants with different cuisines
2. Manage restaurant menus
3. Place food orders
4. Group restaurants by cuisine type
5. Track order status
csharp
// In class Restaurant:
// - int RestaurantId
// - string Name
// - string CuisineType
// - string Location
// - double DeliveryCharge

// In class FoodItem:
// - int ItemId
// - string Name
// - string Category
// - double Price
// - int RestaurantId

// In class Order:
// - int OrderId
// - int CustomerId
// - List<FoodItem> Items
// - DateTime OrderTime
// - string Status
// - double TotalAmount

// In class DeliveryManager:
public void AddRestaurant(string name, string cuisine, 
                         string location, double charge)
public void AddFoodItem(int restaurantId, string name, 
                       string category, double price)
public Dictionary<string, List<Restaurant>> GroupRestaurantsByCuisine()
public bool PlaceOrder(int customerId, List<int> itemIds)
public List<Order> GetPendingOrders()
Use Cases:
•	Add restaurants with different cuisines
•	Manage restaurant menus
•	Place food orders
•	Group restaurants by cuisine type
•	Track order status
