using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantMenuManagement
{
    public class MenuManager
    {
        // Store all menu items
        public List<MenuItem> MenuItems = new List<MenuItem>();
        private int nextId = 1;
        
        // Add menu item with price validation
        public void AddMenuItem(string name, string category, double price, bool isVeg)
        {
            if (price > 0)
            {
                MenuItems.Add(new MenuItem
                {
                    ItemId = nextId++,
                    ItemName = name,
                    Category = category,
                    Price = price,
                    IsVegetarian = isVeg
                });
            }
        }

        // Group items by category
        public Dictionary<string, List<MenuItem>> GroupItemsByCategory()
        {
            return MenuItems.GroupBy(m => m.Category).ToDictionary(g => g.Key, g => g.ToList());
        }

        // Get all vegetarian items
        public List<MenuItem> GetVegetarianItems()
        {
            return MenuItems.Where(m => m.IsVegetarian).ToList();
        }

        // Calculate average price by category
        public double CalculateAveragePriceByCategory(string category)
        {
            var items = MenuItems.Where(m => m.Category == category).ToList();
            return items.Count > 0 ? items.Average(m => m.Price) : 0;
        }
    }
}
