using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexibleInventorySystem_Practice;
using FlexibleInventorySystem_Practice.Models;
using FlexibleInventorySystem_Practice.Services;
using Xunit;

namespace FlexibleInventorySystem_Prctice.UnitTest
{
    /// <summary>
    /// xUnit test cases for InventoryManager
    /// Run with: dotnet test
    /// </summary>
    public class InventoryManagerTests
    {
        [Fact]
        public void AddProduct_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var manager = new InventoryManager();
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Test Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            // Act
            bool result = manager.AddProduct(product);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddProduct_NullProduct_ThrowsException()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => manager.AddProduct(null!));
        }

        [Fact]
        public void AddProduct_DuplicateId_ReturnsFalse()
        {
            // Arrange
            var manager = new InventoryManager();
            var product1 = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            var product2 = new ElectronicProduct
            {
                Id = "E001",
                Name = "Another Laptop",
                Price = 1299.99m,
                Quantity = 3,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "HP",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            manager.AddProduct(product1);

            // Act
            bool result = manager.AddProduct(product2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveProduct_ValidId_ReturnsTrue()
        {
            // Arrange
            var manager = new InventoryManager();
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            manager.AddProduct(product);

            // Act
            bool result = manager.RemoveProduct("E001");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveProduct_InvalidId_ReturnsFalse()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act
            bool result = manager.RemoveProduct("INVALID");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FindProduct_ValidId_ReturnsProduct()
        {
            // Arrange
            var manager = new InventoryManager();
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            manager.AddProduct(product);

            // Act
            var found = manager.FindProduct("E001");

            // Assert
            Assert.NotNull(found);
            Assert.Equal("E001", found.Id);
            Assert.Equal("Laptop", found.Name);
        }

        [Fact]
        public void FindProduct_InvalidId_ReturnsNull()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act
            var found = manager.FindProduct("INVALID");

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void UpdateQuantity_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var manager = new InventoryManager();
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            manager.AddProduct(product);

            // Act
            bool result = manager.UpdateQuantity("E001", 10);
            var updated = manager.FindProduct("E001");

            // Assert
            Assert.True(result);
            Assert.NotNull(updated);
            Assert.Equal(10, updated.Quantity);
        }

        [Fact]
        public void UpdateQuantity_InvalidId_ReturnsFalse()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act
            bool result = manager.UpdateQuantity("INVALID", 10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateQuantity_NegativeQuantity_ReturnsFalse()
        {
            // Arrange
            var manager = new InventoryManager();
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            manager.AddProduct(product);

            // Act
            bool result = manager.UpdateQuantity("E001", -5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetProductsByCategory_ReturnsCorrectProducts()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddProduct(new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            });

            manager.AddProduct(new GroceryProduct
            {
                Id = "G001",
                Name = "Milk",
                Price = 3.49m,
                Quantity = 50,
                Category = "Groceries",
                DateAdded = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            });

            // Act
            var electronics = manager.GetProductsByCategory("Electronics");

            // Assert
            Assert.Single(electronics);
            Assert.Equal("E001", electronics[0].Id);
        }

        [Fact]
        public void GetProductsByCategory_EmptyCategory_ReturnsEmptyList()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act
            var result = manager.GetProductsByCategory("");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetTotalInventoryValue_CalculatesCorrectly()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddProduct(new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 100m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            });

            manager.AddProduct(new GroceryProduct
            {
                Id = "G001",
                Name = "Milk",
                Price = 2m,
                Quantity = 50,
                Category = "Groceries",
                DateAdded = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            });

            // Act
            decimal totalValue = manager.GetTotalInventoryValue();

            // Assert
            // (100 * 5) + (2 * 50) = 500 + 100 = 600
            Assert.Equal(600m, totalValue);
        }

        [Fact]
        public void GetTotalInventoryValue_EmptyInventory_ReturnsZero()
        {
            // Arrange
            var manager = new InventoryManager();

            // Act
            decimal totalValue = manager.GetTotalInventoryValue();

            // Assert
            Assert.Equal(0m, totalValue);
        }

        [Fact]
        public void GetLowStockProducts_ReturnsProductsBelowThreshold()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddProduct(new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 2,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            });

            manager.AddProduct(new GroceryProduct
            {
                Id = "G001",
                Name = "Milk",
                Price = 3.49m,
                Quantity = 50,
                Category = "Groceries",
                DateAdded = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            });

            // Act
            var lowStock = manager.GetLowStockProducts(10);

            // Assert
            Assert.Single(lowStock);
            Assert.Equal("E001", lowStock[0].Id);
        }

        [Fact]
        public void GetLowStockProducts_NoLowStock_ReturnsEmptyList()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddProduct(new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 50,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            });

            // Act
            var lowStock = manager.GetLowStockProducts(10);

            // Assert
            Assert.Empty(lowStock);
        }

        [Fact]
        public void ElectronicProduct_GetProductDetails_ReturnsCorrectFormat()
        {
            // Arrange
            var product = new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 5,
                Category = "Electronics",
                DateAdded = DateTime.Now,
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V"
            };

            // Act
            string details = product.GetProductDetails();

            // Assert
            Assert.Contains("Dell", details);
            Assert.Contains("24 months", details);
            Assert.Contains("110-240V", details);
        }

        [Fact]
        public void GroceryProduct_IsExpired_ReturnsTrueForExpiredProduct()
        {
            // Arrange
            var product = new GroceryProduct
            {
                Id = "G001",
                Name = "Expired Milk",
                Price = 3.49m,
                Quantity = 10,
                Category = "Groceries",
                DateAdded = DateTime.Now.AddDays(-30),
                ExpiryDate = DateTime.Now.AddDays(-1), // Expired yesterday
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            };

            // Act
            bool isExpired = product.IsExpired();

            // Assert
            Assert.True(isExpired);
        }

        [Fact]
        public void GroceryProduct_CalculateValue_ReturnsZeroForExpiredProduct()
        {
            // Arrange
            var product = new GroceryProduct
            {
                Id = "G001",
                Name = "Expired Milk",
                Price = 10m,
                Quantity = 5,
                Category = "Groceries",
                DateAdded = DateTime.Now.AddDays(-30),
                ExpiryDate = DateTime.Now.AddDays(-1), // Expired
                IsPerishable = true,
                Weight = 1.0,
                StorageTemperature = "Refrigerated"
            };

            // Act
            decimal value = product.CalculateValue();

            // Assert
            Assert.Equal(0m, value);
        }
    }
}
