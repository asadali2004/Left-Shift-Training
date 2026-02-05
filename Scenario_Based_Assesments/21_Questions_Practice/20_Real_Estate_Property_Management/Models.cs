using System;
using System.Collections.Generic;

namespace _20_Real_Estate_Property_Management
{
    // Represents a property
    public class Property
    {
        public string PropertyId { get; set; }
        public string Address { get; set; }
        public string PropertyType { get; set; } // Apartment / House / Villa
        public int Bedrooms { get; set; }
        public double AreaSqFt { get; set; }
        public double Price { get; set; }
        public string Status { get; set; } // Available / Sold / Rented
        public string Owner { get; set; }
    }

    // Represents a client
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string ClientType { get; set; } // Buyer / Renter
        public double Budget { get; set; }
        public List<string> Requirements { get; set; }
            = new List<string>();
    }

    // Represents a property viewing
    public class Viewing
    {
        public int ViewingId { get; set; }
        public string PropertyId { get; set; }
        public int ClientId { get; set; }
        public DateTime ViewingDate { get; set; }
        public string Feedback { get; set; }
    }
}
