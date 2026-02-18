using System;                                                     // Console
using System.Collections.Generic;                                    // IEnumerable, List

namespace ItTechGenie.M1.IComparable.Q3
{
    public static class RangeUtil
    {
        // ✅ TODO: Student must implement only this method
        public static (T min, T max) GetMinMax<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            // TODO:
            // - validate items not null
            // - iterate once
            // - initialize min/max from first element
            // - use CompareTo to update
            bool isFirst = true;
            T min = default!;
            T max = default!;

            foreach (var item in items)
            {
                if (isFirst)
                {
                    min = item;
                    max = item;
                    isFirst = false;
                    continue;
                }

                if (item.CompareTo(min) < 0)
                    min = item;

                if (item.CompareTo(max) > 0)
                    max = item;
            }

            if (isFirst)
                throw new InvalidOperationException("Sequence contains no elements.");

            return (min, max);
        }
    }

    internal class Program
    {
        static void Main()
        {
            var ints = new List<int> { 5, 1, 9, 2, 2 };
            var mm1 = RangeUtil.GetMinMax(ints);
            Console.WriteLine($"Int Min={mm1.min}, Max={mm1.max}");

            var dates = new List<DateTime> { DateTime.Parse("2026-02-18"), DateTime.Parse("2026-01-01"), DateTime.Parse("2026-12-31") };
            var mm2 = RangeUtil.GetMinMax(dates);
            Console.WriteLine($"Date Min={mm2.min:yyyy-MM-dd}, Max={mm2.max:yyyy-MM-dd}");
        }
    }
}




// Another Way 

// public static (T min, T max) GetMinMax<T>(IEnumerable<T> items) where T : IComparable<T>
// {
//     if (items == null)
//         throw new ArgumentNullException(nameof(items));

//     using var enumerator = items.GetEnumerator();

//     if (!enumerator.MoveNext())
//         throw new InvalidOperationException("Sequence contains no elements.");

//     T min = enumerator.Current;
//     T max = enumerator.Current;

//     while (enumerator.MoveNext())
//     {
//         var current = enumerator.Current;

//         if (current.CompareTo(min) < 0)
//             min = current;

//         if (current.CompareTo(max) > 0)
//             max = current;
//     }

//     return (min, max);
// }



