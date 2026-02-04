# GoAir Security Entry Validation System

## Problem Statement

In the fast-paced environment of **GOAIR**, maintaining security and controlled access to restricted areas is critical.  
To automate this process, a system is required to validate employee entry details.

Each entry contains:
- **Employee ID**
- **Entry Type**
- **Duration (in hours)**

The input is provided in the following format:

```
EmployeeID:EntryType:Duration
```

Example:
```
GOAIR/8924:Security:4
```

---

## Validation Rules

### Employee ID Validation
- Must be **exactly 10 characters**
- Format must be:
```
GOAIR/####
```
where `####` represents **4 digits**

Example of valid ID:
```
GOAIR/7385
```

---

### Duration Validation
- Duration must be between **1 and 5 hours (inclusive)**

---

## Output Rules

- If **both Employee ID and Duration are valid**, display:
```
Valid entry details
```
- If **any validation fails**, display:
```
Invalid entry details
```

---

## Sample Input

```
Enter the number of entries
3
Enter entry 1 details
GOAIR/8924:Security:4
Enter entry 2 details
GOAIR/1353:Inspection:2
Enter entry 3 details
GOAIR/1353:CheckIn:5
```

---

## Sample Output

```
Valid entry details
Valid entry details
Valid entry details
```

---

## Solution (C# Implementation)

```csharp
// GoAir Security Entry Validation System
// Validates employee IDs and entry duration for security personnel

using System;
using System.Text.RegularExpressions;

namespace GoAirSecurity
{
    /// <summary>
    /// Utility class for validating employee entry details
    /// </summary>
    public class EntryUtility
    {
        /// <summary>
        /// Validates employee ID format (GOAIR/####)
        /// </summary>
        public bool validateEmployeeId(string employeeId)
        {
            // Employee ID must be exactly 10 characters
            if (employeeId.Length == 10)
            {
                // Regex to check format GOAIR/ followed by 4 digits
                if (Regex.IsMatch(employeeId, @"^GOAIR/[0-9]{4}$"))
                {
                    return true;
                }
            }

            // Throw exception if validation fails
            throw new InvalidEntryException("Invalid employee ID");
        }

        /// <summary>
        /// Validates duration between 1 and 5 hours
        /// </summary>
        public bool validateDuration(int duration)
        {
            // Duration must be between 1 and 5 inclusive
            if (duration >= 1 && duration <= 5)
            {
                return true;
            }

            // Throw exception if validation fails
            throw new InvalidEntryException("Invalid duration");
        }
    }

    /// <summary>
    /// Custom exception class for invalid entry validation
    /// </summary>
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// User interface class to accept input and display output
    /// </summary>
    public class UserInterface
    {
        public static void Main(string[] args)
        {
            EntryUtility util = new EntryUtility();

            Console.WriteLine("Enter the number of entries");
            int n = int.Parse(Console.ReadLine()!);

            string[] entries = new string[n];

            // Input Phase
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter entry {i + 1} details");
                entries[i] = Console.ReadLine()!;
            }

            // Output Phase
            foreach (string entry in entries)
            {
                try
                {
                    string[] parts = entry.Split(':');

                    util.validateEmployeeId(parts[0]);
                    util.validateDuration(int.Parse(parts[2]));

                    Console.WriteLine("Valid entry details");
                }
                catch
                {
                    Console.WriteLine("Invalid entry details");
                }
            }
        }
    }
}
```

---

## Key Concepts Used

* Custom Exceptions
* Exception Handling (`try-catch`)
* Regular Expressions
* String Parsing
* Object-Oriented Design

---

## Conclusion

This solution strictly follows the problem specification:

* Validations are handled in a utility class
* Exceptions are propagated and handled in `Main`
* Output format exactly matches the sample output

✔ Exam-ready
✔ Assignment-safe
✔ Clean and readable