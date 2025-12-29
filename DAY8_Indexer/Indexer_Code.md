# Indexer in C# - Code Example

## Overview
This example demonstrates how to implement and use **indexers** in C#. Indexers allow objects to be indexed like arrays, providing a natural syntax for accessing elements within a class using the bracket notation `[]`.

## Key Concepts
- **Indexer**: A special property that enables array-like access to class members
- **Syntax**: Uses the `this` keyword with square brackets `this[int index]`
- **Benefits**: Provides intuitive array-like access to internal collections without exposing the underlying data structure

## Learning Objectives
1. Understand how to declare an indexer using the `this` keyword
2. Implement get and set accessors for the indexer
3. Use indexers to access and modify internal data in a class

---

## Code Implementation

```csharp
namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate the use of indexers in C#.
    /// Learning Outcome: Understand how to implement and use indexers in a class.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Mydata mydata = new Mydata(); // Creating an instance of Mydata class
            mydata[0] = "C";  // Using indexer to set values
            mydata[1] = "C++";
            mydata[2] = "C#";

            Console.WriteLine("First Value is: " + mydata[0]); // Using indexer to get values
            Console.WriteLine("Second Value is: " + mydata[1]);
            Console.WriteLine("Third Value is: " + mydata[2]);
        }
    }


    /// <summary>
    /// Represents a collection of three string values accessible by index.
    /// </summary>
    /// <remarks>Use the indexer to get or set individual string values by their zero-based index. The valid
    /// index range is 0 to 2, inclusive.</remarks>
    class Mydata
    {
        private string[] values = new string[3]; // Internal array to hold string values

        public string this[int index] // Indexer to get/set values by index
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }

    }
}
```

## Expected Output
```
First Value is: C
Second Value is: C++
Third Value is: C#
```

## How It Works
1. The `Mydata` class contains a private array `values` with 3 elements
2. The indexer `public string this[int index]` provides controlled access to this array
3. When you write `mydata[0] = "C"`, the set accessor is called
4. When you write `mydata[0]`, the get accessor returns the value at that index
5. This provides a clean, array-like interface without exposing the internal array directly




