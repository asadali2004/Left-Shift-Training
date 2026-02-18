// using System;                                                     // Console
// using System.Collections.Generic;                                    // List

// namespace ItTechGenie.M1.IComparable.Q2
// {
//     public class Product : IComparable<Product>                      // comparable for Sort()
//     {
//         public string Sku { get; }                                   // key (may contain spaces)
//         public string Name { get; }                                  // display name
//         public decimal Price { get; }                                // price

//         public Product(string sku, string name, decimal price)        // constructor
//         {
//             Sku = sku;                                               // assign
//             Name = name;                                             // assign
//             Price = price;                                           // assign
//         }

//         // ✅ TODO: Student must implement only this method
//         public int CompareTo(Product? other)
//         {
//             // TODO:
//             // - handle null
//             // - compare Sku.Trim() using StringComparison.Ordinal
//             // - then Price
//             // - then Name (ignore case)
//             if (other == null)
//             {
//                 return 1;
//             }
//             int result = Sku.Trim().CompareTo(other.Sku.Trim(), StringComparison.Ordinal);
//             if (result != 0) return result;

//             result = Price.CompareTo(other.Price);
//             if (result != 0) return result;

//             return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
//         }

//         public override string ToString() => $"{Sku} | {Name} | ₹{Price}";
//     }

//     internal class Program
//     {
//         static void Main()
//         {
//             var list = new List<Product>
//             {
//                 new Product(" SKU-β77  ", "Headphones ✅", 7999m),
//                 new Product("SKU-α12", "Laptop Stand", 1299.50m),
//                 new Product("SKU-α12", "Laptop stand", 1299.50m),
//                 new Product("SKU-!@#", "Cable 2m", 499m),
//             };

//             list.Sort();                                             // uses CompareTo
//             list.ForEach(p => Console.WriteLine(p));
//         }
//     }
// }