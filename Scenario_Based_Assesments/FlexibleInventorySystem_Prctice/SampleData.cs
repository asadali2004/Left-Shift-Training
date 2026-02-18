using FlexibleInventorySystem_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice
{
    // Sample data for testing your implementation
    public static class SampleData
    {
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
        {
            new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 10,
                Category = "Electronics",
                DateAdded = DateTime.Now.AddMonths(-3),
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V",
                IsRefurbished = false
            },
            new ElectronicProduct
            {
                Id = "E002",
                Name = "Smartphone",
                Price = 599.99m,
                Quantity = 25,
                Category = "Electronics",
                DateAdded = DateTime.Now.AddMonths(-2),
                Brand = "Samsung",
                WarrantyMonths = 12,
                Voltage = "5V",
                IsRefurbished = false
            },
            new GroceryProduct
            {
                Id = "G001",
                Name = "Milk",
                Price = 3.49m,
                Quantity = 50,
                Category = "Groceries",
                DateAdded = DateTime.Now.AddDays(-5),
                ExpiryDate = DateTime.Now.AddDays(7),
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            },
            new GroceryProduct
            {
                Id = "G002",
                Name = "Bread",
                Price = 2.99m,
                Quantity = 30,
                Category = "Groceries",
                DateAdded = DateTime.Now.AddDays(-2),
                ExpiryDate = DateTime.Now.AddDays(3),
                IsPerishable = true,
                Weight = 0.5,
                StorageTemperature = "Room temperature"
            },
            new ClothingProduct
            {
                Id = "C001",
                Name = "T-Shirt",
                Price = 19.99m,
                Quantity = 100,
                Category = "Clothing",
                DateAdded = DateTime.Now.AddMonths(-1),
                Size = "L",
                Color = "Blue",
                Material = "Cotton",
                Gender = "Unisex",
                Season = "Summer"
            },
            new ClothingProduct
            {
                Id = "C002",
                Name = "Winter Jacket",
                Price = 79.99m,
                Quantity = 20,
                Category = "Clothing",
                DateAdded = DateTime.Now.AddMonths(-2),
                Size = "M",
                Color = "Black",
                Material = "Polyester",
                Gender = "Men",
                Season = "Winter"
            }
        };
        }
    }
}
