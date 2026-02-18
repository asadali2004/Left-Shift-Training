using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexibleInventorySystem_Practice.Models;

namespace FlexibleInventorySystem_Practice.Utilities
{
    /// <summary>
    /// Validation helper class for products
    /// </summary>
    public static class ProductValidator
        {
        /// <summary>
        /// Validates product data
        /// Checks: ID not null/empty, Name not null/empty, Price > 0, Quantity >= 0
        /// </summary>
        public static bool ValidateProduct(Product product, out string? errorMessage)
            {
                errorMessage = null;

                if (product == null)
                {
                    errorMessage = "Product cannot be null.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Id))
                {
                    errorMessage = "Product ID cannot be empty.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    errorMessage = "Product Name cannot be empty.";
                    return false;
                }

                if (product.Price <= 0)
                {
                    errorMessage = "Product Price must be greater than 0.";
                    return false;
                }

                if (product.Quantity < 0)
                {
                    errorMessage = "Product Quantity cannot be negative.";
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Validates electronic product specific rules
            /// </summary>
            public static bool ValidateElectronicProduct(ElectronicProduct product, out string? errorMessage)
            {
                errorMessage = null;

                // First validate base product
                if (!ValidateProduct(product, out errorMessage))
                    return false;

                if (string.IsNullOrWhiteSpace(product.Brand))
                {
                    errorMessage = "Brand cannot be empty for electronic products.";
                    return false;
                }

                if (product.WarrantyMonths < 0)
                {
                    errorMessage = "Warranty months cannot be negative.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Voltage))
                {
                    errorMessage = "Voltage cannot be empty for electronic products.";
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Validates grocery product specific rules
            /// </summary>
            public static bool ValidateGroceryProduct(GroceryProduct product, out string? errorMessage)
            {
                errorMessage = null;

                // First validate base product
                if (!ValidateProduct(product, out errorMessage))
                    return false;

                if (product.ExpiryDate < DateTime.Now && product.ExpiryDate != default(DateTime))
                {
                    errorMessage = "Expiry date cannot be in the past.";
                    return false;
                }

                if (product.Weight <= 0)
                {
                    errorMessage = "Weight must be greater than 0.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.StorageTemperature))
                {
                    errorMessage = "Storage temperature cannot be empty.";
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Validates clothing product specific rules
            /// </summary>
            public static bool ValidateClothingProduct(ClothingProduct product, out string? errorMessage)
            {
                errorMessage = null;

                // First validate base product
                if (!ValidateProduct(product, out errorMessage))
                    return false;

                if (string.IsNullOrWhiteSpace(product.Size))
                {
                    errorMessage = "Size cannot be empty.";
                    return false;
                }

                if (!product.IsValidSize())
                {
                    errorMessage = "Size must be one of: XS, S, M, L, XL, XXL.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Color))
                {
                    errorMessage = "Color cannot be empty.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Material))
                {
                    errorMessage = "Material cannot be empty.";
                    return false;
                }

                return true;
            }
        }
}
