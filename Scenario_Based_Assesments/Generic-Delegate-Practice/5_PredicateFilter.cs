using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Program
{
    public static void Main()
    {
        var nums = new List<int> { 2, 5, 8, 11, 14 };

        var evens = Filter(nums, n => n % 2 == 0);
        Console.WriteLine(string.Join(",", evens));         // Expected: 2,8,14

        var big = Filter(nums, n => n >= 10);
        Console.WriteLine(string.Join(",", big));           // Expected: 11,14
    }

    // ✅ TODO: Students implement only this function
    public static List<T> Filter<T>(List<T> items, Predicate<T> match)
    {
        // TODO: return a new list with matched items
        //using LINQ
        var result = items.Where(number => match(number)).ToList();
        return result.Count == 0?default:result;

        //Using Loop
        // List<T> result = new List<T>();
        // foreach (var item in items)
        // {
        //     if (match(item))
        //     {
        //         result.Add(item);
        //     }
        // }
        // return result;
    }
}