# Coding Activity: Your Name Is Mine

## Problem Statement

Archer devised a strategy to evaluate compatibility between two people based on their names.

Two names are said to be **made for each other** if:
- One name is a **subsequence** of the other.
- The order of characters must be maintained.
- Names should contain **only alphabets and spaces**.

### Conditions

1. If a name contains numbers or special characters, it is invalid.
2. If both names are invalid, display:
```
Both <name1> and <name2> are invalid names
```
3. If only one name is invalid, display:
```
<name> is an invalid name
```
4. If one name is a subsequence of the other:
```
<name1> and <name2> are made for each other
```
5. Otherwise:
```
Both <name1> and <name2> are not made for each other
```

> **Note:** Compatibility value calculation is intentionally skipped in this implementation.

---

## Solution (C# Implementation)

```csharp
using System;
using System.Text.RegularExpressions;

namespace myNameIsYours
{
 public class MyNameIsYours
 {
     public static void Main(string[] args)
     {
         // Read man and woman names
         Console.WriteLine("Enter Man Name:");
         string man = Console.ReadLine();

         Console.WriteLine("Enter Woman Name:");
         string woman = Console.ReadLine();

         // Display match result
         Console.WriteLine(checkMatch(man, woman));
     }

     public static string checkMatch(string man, string woman)
     {
         string result = "";

         // Validate names (only alphabets and spaces allowed)
         bool isManValid = Regex.IsMatch(man, @"^[A-Za-z ]+$");
         bool isWomanValid = Regex.IsMatch(woman, @"^[A-Za-z ]+$");

         // Validation checks
         if (!isManValid && !isWomanValid)
         {
             return $"Both {man} and {woman} are invalid names";
         }
         else if (!isManValid)
         {
             return $"{man} is an invalid name";
         }
         else if (!isWomanValid)
         {
             return $"{woman} is an invalid name";
         }

         // Check subsequence condition in both directions
         if (validMatch(man, woman) || validMatch(woman, man))
         {
             result = $"{man} and {woman} are made for each other";
         }
         else
         {
             result = $"Both {man} and {woman} are not made for each other";
         }

         return result;
     }

     // Checks whether 'man' is a subsequence of 'woman'
     public static bool validMatch(string man, string woman)
     {
         int i = 0;
         int j = 0;

         // Two-pointer approach for subsequence check
         while (i < man.Length && j < woman.Length)
         {
             if (man[i] == woman[j])
             {
                 i++;
             }
             j++;
         }

         return i == man.Length;
     }

     // Compatibility value calculation intentionally skipped
     // public static int findCompatibility(string man, string woman)
     // {
     // }
 }
}
```

---

## Example

### Input

```
Enter Man Name:
john
Enter Woman Name:
johanna
```

### Output

```
john and johanna are made for each other
```

### Explanation

* "john" is a subsequence of "johanna"
* Character order is preserved

---

## Key Concepts Used

* Regular Expressions for input validation
* Subsequence checking using two-pointer technique
* Conditional logic
* Clean separation of methods
* Object-Oriented design

---
