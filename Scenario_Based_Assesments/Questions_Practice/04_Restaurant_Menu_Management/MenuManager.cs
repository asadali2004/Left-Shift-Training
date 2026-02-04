using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantMenuManagement
{
    public class MenuManager
    {
        // TODO: Add collection to store menu items
        
        public void AddMenuItem(string name, string category, double price, bool isVeg)
        {
            // TODO: Adds item with validation for price > 0
        }

        public Dictionary<string, List<MenuItem>> GroupItemsByCategory()
        {
            // TODO: Groups items by category
            return new Dictionary<string, List<MenuItem>>();
        }

        public List<MenuItem> GetVegetarianItems()
        {
            // TODO: Returns all vegetarian items
            return new List<MenuItem>();
        }

        public double CalculateAveragePriceByCategory(string category)
        {
            // TODO: Returns average price of items in category
            return 0;
        }
    }
}
