using System; using System.Collections.Generic;
namespace OnlineFoodDelivery
{
    public class Restaurant { public int RestaurantId { get; set; } public string Name { get; set; } public string CuisineType { get; set; } public string Location { get; set; } public double DeliveryCharge { get; set; } }
    public class FoodItem { public int ItemId { get; set; } public string Name { get; set; } public string Category { get; set; } public double Price { get; set; } public int RestaurantId { get; set; } }
    public class Order { public int OrderId { get; set; } public int CustomerId { get; set; } public List<FoodItem> Items { get; set; } public DateTime OrderTime { get; set; } public string Status { get; set; } public double TotalAmount { get; set; } }
}
