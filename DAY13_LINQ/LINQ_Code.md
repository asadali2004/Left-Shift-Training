# LINQ Code — Simple, Readable Examples

This file shows three beginner-friendly LINQ examples:
- Filter and transform an array of names (`LinqExample`)
- Shape process info into a simple class (`LinqExample2`)
- Shape process info into an anonymous type (`LinqExample3`)

The code is kept simple, with short comments to explain each step.

```csharp
using System;
using System.Data.Common;
using System.Linq;

namespace Learning_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            // Try any one of these:
            // LinqExample();
            // LinqExample2();
            LinqExample3();
        }

        // Example 1: basic LINQ with strings
        public static void LinqExample()
        {
            string[] names = { "Asad", "BoB", "Varav", "Abhi"};

            // Plain loop check (not LINQ): finds exact match "Bob" (case-sensitive)
            foreach (var item in names)
            {
                if (item == "Bob")
                {
                    System.Console.WriteLine("Bob is present");
                }
            }

            // LINQ: find all names equal to "Bob" (case-sensitive)
            var findName = from name in names where name == "Bob" select name;

            // LINQ: uppercase all names
            var findName2 = from Capname in names select Capname.ToUpper();

            // LINQ: sort names in descending order
            var findName3 = from Ordername in names orderby Ordername descending select Ordername;

            // Use the palindrome check on each found name
            foreach (var n in findName)
            {
                System.Console.WriteLine(IsPlindrom(n));
            }

        }

        // Palindrome helper: keeps original behavior (case-sensitive)
        public static string IsPlindrom(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Not Palindrom " + name;
            }

            var reversed = new string(name.Reverse().ToArray());

            if (name.Equals(reversed))
            {
                return "Palindrom " + name;
            }
            else
            {
                return "Not Palindrom " + name;
            }
        }

        // Example 2: project processes into a simple class
        public static void LinqExample2()
        {
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new MyProcess() { Name = p.ProcessName, Id = p.Id };

            foreach (var proc in procCollection)
            {
                System.Console.WriteLine($"Process Name = {proc.Name} Id = {proc.Id}");
            }
        }
        
        // Example 3: anonymous type (no class needed)
        public static void LinqExample3(){
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new { Name = p.ProcessName, Id = p.Id };

            foreach(var proc in procCollection)
            {
                System.Console.WriteLine($"Process Name = {proc.Name} Id = {proc.Id}");
            }
        }

        private class MyProcess
        {
            public MyProcess()
            {
            }

            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}
```

## Notes to Keep in Mind
- String equality (`==`, `Equals`) is case-sensitive by default. If you need case-insensitive matching (so "BoB" equals "Bob"), use `StringComparison.OrdinalIgnoreCase`.
  ```csharp
  var findName = from name in names
                 where string.Equals(name, "Bob", StringComparison.OrdinalIgnoreCase)
                 select name;
  ```
- Anonymous types are great for quick results you only use locally (like printing). If you need to pass data around or return it, use a class (like `MyProcess`).
- Most LINQ queries run when you iterate them (e.g., `foreach`, `ToList()`), not when you create them—this is called deferred execution.

## Optional: Method Syntax Version (same behavior)
```csharp
var findName = names.Where(n => n == "Bob");
var findName2 = names.Select(n => n.ToUpper());
var findName3 = names.OrderByDescending(n => n);
```
using System;
using System.Data.Common;
using System.Linq;

namespace Learning_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            // LinqExample();
            // LinqExample2();
            LinqExample3();
        }

        public static void LinqExample()
        {
            string[] names = { "Asad", "BoB", "Varav", "Abhi"};

            foreach (var item in names)
            {
                if (item == "Bob")
                {
                    System.Console.WriteLine("Bob is present");
                }
            }
            var findName = from name in names where name == "Bob" select name;
            var findName2 = from Capname in names select Capname.ToUpper();
            var findName3 = from Ordername in names orderby Ordername descending select Ordername;

            foreach (var n in findName)
            {
                System.Console.WriteLine(IsPlindrom(n));
            }

        }
        public static string IsPlindrom(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Not Palindrom " + name;
            }

            var reversed = new string(name.Reverse().ToArray());

            if (name.Equals(reversed))
            {
                return "Palindrom " + name;
            }
            else
            {
                return "Not Palindrom " + name;
            }
        }

        public static void LinqExample2()
        {
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new MyProcess() { Name = p.ProcessName, Id = p.Id };

            foreach (var proc in procCollection)
            {
                System.Console.WriteLine($"Process Name = {proc.Name} Id = {proc.Id}");
            }
        }
        
        // Anonymous Data Type where we dont have to create a class and we can use it directly so its called anonymous data type
        public static void LinqExample3(){
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new { Name = p.ProcessName, Id = p.Id };

            foreach(var proc in procCollection)
            {
                System.Console.WriteLine($"Process Name = {proc.Name} Id = {proc.Id}");
            }
        }

        private class MyProcess
        {
            public MyProcess()
            {
            }

            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}