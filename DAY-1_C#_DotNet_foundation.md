// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
class Program
{
    static void Main()
    {
        // Console.WriteLine("Enter Your name: ");
        // string? name = Console.ReadLine();
        // Console.WriteLine("Hello, " + name + "!");

        Console.Write("Enter your number: ");
        int n = int.Parse(Console.ReadLine());

        if (n <= 1)
        {
            Console.WriteLine("Not a Prime");
            return;
        }

        if (n == 2 || n == 3)
        {
            Console.WriteLine("It's a Prime");
            return;
        }

        if (n % 2 == 0)
        {
            Console.WriteLine("Not a Prime");
            return;
        }

        for (int i = 3; i * i <= n; i += 2)
        {
            if (n % i == 0)
            {
                Console.WriteLine("Not a Prime");
                return;
            }
        }

        Console.WriteLine("It's a Prime");
    }
}
