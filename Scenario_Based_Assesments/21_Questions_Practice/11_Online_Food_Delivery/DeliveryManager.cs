using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineFoodDelivery
{
    public class DeliveryManager
    {
        // Store all data
        public Dictionary<int, Restaurant> Restaurants = new Dictionary<int, Restaurant>();
        public Dictionary<int, FoodItem> FoodItems = new Dictionary<int, FoodItem>();
        public List<Order> Orders = new List<Order>();
        private int nextRestaurantId = 1;
        private int nextItemId = 1;
        private int nextOrderId = 1;
        
        // Add restaurant
        public void AddRestaurant(string name, string cuisine, string location, double charge)
        {
            int restaurantId = nextRestaurantId++;
            Restaurants.Add(restaurantId, new Restaurant
            {
                RestaurantId = restaurantId,
                Name = name,
                CuisineType = cuisine,
                Location = location,
                DeliveryCharge = charge
            });
        }
        
        // Add food item
        public void AddFoodItem(int restaurantId, string name, string category, double price)
        {
            if (Restaurants.ContainsKey(restaurantId))
            {
                int itemId = nextItemId++;
                FoodItems.Add(itemId, new FoodItem
                {
                    ItemId = itemId,
                    Name = name,
                    Category = category,
                    Price = price,
                    RestaurantId = restaurantId
                });
            }
        }
        
        // Group restaurants by cuisine
        public Dictionary<string, List<Restaurant>> GroupRestaurantsByCuisine()
        {
            return Restaurants.Values.GroupBy(r => r.CuisineType).ToDictionary(g => g.Key, g => g.ToList());
        }
        
        // Place order
        public bool PlaceOrder(int customerId, List<int> itemIds)
        {
            if (itemIds.Count > 0)
            {
                var items = new List<FoodItem>();
                double totalAmount = 0;
                int restaurantId = -1;
                
                foreach (var itemId in itemIds)
                {
                    if (!FoodItems.ContainsKey(itemId))
                        return false;
                    
                    var item = FoodItems[itemId];
                    if (restaurantId == -1)
                        restaurantId = item.RestaurantId;
                    else if (restaurantId != item.RestaurantId)
                        return false;
                    
                    items.Add(item);
                    totalAmount += item.Price;
                }
                
                if (Restaurants.ContainsKey(restaurantId))
                {
                    totalAmount += Restaurants[restaurantId].DeliveryCharge;
                    
                    Orders.Add(new Order
                    {
                        OrderId = nextOrderId++,
                        CustomerId = customerId,
                        Items = items,
                        OrderTime = DateTime.Now,
                        Status = "Pending",
                        TotalAmount = totalAmount
                    });
                    return true;
                }
            }
            return false;
        }
        
        // Get pending orders
        public List<Order> GetPendingOrders()
        {
            return Orders.Where(o => o.Status == "Pending").ToList();
        }
    }
}
