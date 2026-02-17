using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine(Apply(5, 3, (a, b) => a + b));    // Expected: 8
        Console.WriteLine(Apply(5, 3, (a, b) => a * b));    // Expected: 15
        Console.WriteLine(Apply("Hi", "Tech", (a, b) => a + " " + b)); // Expected: Hi Tech
    }

    // ✅ TODO: Students implement only this function
    public static T Apply<T>(T x, T y, Func<T, T, T> op)
    {
        // TODO: call op and return the result
        return op(x, y);
    }
}