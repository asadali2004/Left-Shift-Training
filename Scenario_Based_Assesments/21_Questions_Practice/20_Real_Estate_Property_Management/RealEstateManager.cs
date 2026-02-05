using System;
using System.Collections.Generic;
using System.Linq;

namespace _20_Real_Estate_Property_Management
{
    public class RealEstateManager
    {
        public Dictionary<string, Property> Properties
            = new Dictionary<string, Property>();

        public Dictionary<int, Client> Clients
            = new Dictionary<int, Client>();

        public List<Viewing> Viewings = new List<Viewing>();

        private int nextPropertyId = 1001;
        private int nextClientId = 1;
        private int nextViewingId = 1;

        // Add property
        public void AddProperty(string address, string type, int bedrooms,
                                double area, double price, string owner)
        {
            string id = "PR" + nextPropertyId++;

            Properties.Add(id, new Property
            {
                PropertyId = id,
                Address = address,
                PropertyType = type,
                Bedrooms = bedrooms,
                AreaSqFt = area,
                Price = price,
                Owner = owner,
                Status = "Available"
            });
        }

        // Add client
        public void AddClient(string name, string contact, string type,
                              double budget, List<string> requirements)
        {
            int id = nextClientId++;

            Clients.Add(id, new Client
            {
                ClientId = id,
                Name = name,
                Contact = contact,
                ClientType = type,
                Budget = budget,
                Requirements = requirements
            });
        }

        // Schedule viewing
        public bool ScheduleViewing(string propertyId, int clientId, DateTime date)
        {
            if (!Properties.ContainsKey(propertyId) ||
                !Clients.ContainsKey(clientId))
                return false;

            if (Properties[propertyId].Status != "Available")
                return false;

            Viewings.Add(new Viewing
            {
                ViewingId = nextViewingId++,
                PropertyId = propertyId,
                ClientId = clientId,
                ViewingDate = date
            });

            return true;
        }

        // Group properties
        public Dictionary<string, List<Property>> GroupPropertiesByType()
        {
            return Properties.Values
                .GroupBy(p => p.PropertyType)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Budget filter
        public List<Property> GetPropertiesInBudget(double minPrice, double maxPrice)
        {
            return Properties.Values
                .Where(p => p.Price >= minPrice &&
                            p.Price <= maxPrice &&
                            p.Status == "Available")
                .ToList();
        }
    }
}
