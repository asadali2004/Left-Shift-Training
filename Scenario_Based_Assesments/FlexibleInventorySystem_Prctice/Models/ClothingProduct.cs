using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleInventorySystem_Practice.Models
{
    /// <summary>
    /// Clothing product class extending Product base class
    /// </summary>
    public class ClothingProduct : Product
        {
            /// <summary>
            /// Product size (XS, S, M, L, XL, XXL)
            /// </summary>
            public string? Size { get; set; }

            /// <summary>
            /// Product color
            /// </summary>
            public string? Color { get; set; }

            /// <summary>
            /// Product material
            /// </summary>
            public string? Material { get; set; }

            /// <summary>
            /// Target gender (Men, Women, Unisex)
            /// </summary>
            public string? Gender { get; set; }

            /// <summary>
            /// Season (Summer, Winter, All-season)
            /// </summary>
            public string? Season { get; set; }

            /// <summary>
            /// Returns formatted details for clothing product
            /// </summary>
            public override string GetProductDetails()
            {
                return $"Size: {Size}, Color: {Color}, Material: {Material}, Gender: {Gender}, Season: {Season}";
            }

            /// <summary>
            /// Validates if the size is within acceptable range
            /// </summary>
            public bool IsValidSize()
            {
                string[] validSizes = { "XS", "S", "M", "L", "XL", "XXL" };
                return validSizes.Contains(Size?.ToUpper());
            }

            /// <summary>
            /// Calculates value with seasonal discount (15% off for off-season items)
            /// </summary>
            public override decimal CalculateValue()
            {
                decimal baseValue = base.CalculateValue();
                
                // Apply 15% discount for off-season items
                int currentMonth = DateTime.Now.Month;
                if (((Season ?? "").ToLower() == "summer" && (currentMonth < 6 || currentMonth > 8)) ||
                    ((Season ?? "").ToLower() == "winter" && (currentMonth < 12 && currentMonth > 2)))
                {
                    baseValue *= 0.85m; // 15% discount
                }

                return baseValue;
            }
        }
}