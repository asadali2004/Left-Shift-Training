# Shop Gadget Warranty Validator

## Problem Statement

TechStore is a gadget shop that sells electronic items such as laptops, smartphones, televisions, and accessories.  
Each gadget has a warranty period ranging from **6 to 36 months**.

To automate stock updates, the shop needs a validation system that checks:
- Gadget ID
- Warranty Period

The input is provided in the format:

```
gadgetID:gadgetType:warrantyPeriod
```

Example:
```
T283:Television:10
```

---

## Validation Rules

### Gadget ID
- Must start with **one uppercase letter**
- Followed by **exactly three digits**

Example:
```
J367
```

If invalid, throw:
```
Invalid gadget ID
```

---

### Warranty Period
- Must be between **6 and 36 months (inclusive)**

If invalid, throw:
```
Invalid warranty period
```

---

## Output Rules

- If validation succeeds:
```
Warranty accepted, stock updated
```

- If validation fails:
```
Display the exception message
```

---

## Sample Input

```
Enter the number of gadget entries
2
Enter gadget 1 details
T283:Television:10
Enter gadget 2 details
L123:Laptop:24
```

---

## Sample Output

```
Warranty accepted, stock updated
Warranty accepted, stock updated
```

---

## Solution (C# Implementation)

```csharp
using System;
using System.Text.RegularExpressions;

namespace ShopValidator
{
    public class EntryValidator
    {
        // Validates gadget ID (Uppercase letter followed by 3 digits)
        public bool validateGadgetID(string gadgetId)
        {
            if (Regex.IsMatch(gadgetId, @"^[A-Z][0-9]{3}$"))
            {
                return true;
            }

            throw new InvalidGadgetException("Invalid gadget ID");
        }

        // Validates warranty period between 6 and 36 months
        public bool validateWarrantyPeriod(int period)
        {
            if (period >= 6 && period <= 36)
            {
                return true;
            }

            throw new InvalidGadgetException("Invalid warranty period");
        }
    }

    // Custom exception class
    public class InvalidGadgetException : Exception
    {
        public InvalidGadgetException(string message) : base(message)
        {
        }
    }

    public class UserInterface
    {
        public static void Main(string[] args)
        {
            EntryValidator validator = new EntryValidator();

            Console.WriteLine("Enter the number of gadget entries");
            int numOfEntries = int.Parse(Console.ReadLine()!);

            for (int i = 1; i <= numOfEntries; i++)
            {
                Console.WriteLine($"Enter gadget {i} details");
                string entry = Console.ReadLine()!;

                try
                {
                    string[] entryArray = entry.Split(':');

                    // Validate input format
                    if (entryArray.Length != 3)
                        throw new InvalidGadgetException("Invalid gadget ID");

                    validator.validateGadgetID(entryArray[0]);
                    validator.validateWarrantyPeriod(int.Parse(entryArray[2]));

                    Console.WriteLine("Warranty accepted, stock updated");
                }
                catch (InvalidGadgetException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid gadget ID");
                }
            }
        }
    }
}
```

---

## Key Concepts Used

* Custom Exception Handling
* Regular Expressions
* String Parsing
* Input Validation
* Object-Oriented Programming

---

## Conclusion

This implementation validates gadget details accurately, follows the given specifications, and safely handles invalid inputs using custom exceptions.

✔ Specification compliant
✔ Exam-ready
✔ Robust and readable
