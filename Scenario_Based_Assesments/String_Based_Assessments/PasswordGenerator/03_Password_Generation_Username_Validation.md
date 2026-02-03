# Coding Activity: Password Generation

## Problem Statement

“TechXam” is an online learning platform that generates passwords for students during registration based on a **valid username**.

The application must:
- Validate the username based on given rules
- Generate a password only if the username is valid

---

## Username Validation Rules

A username is valid only if **all** the following conditions are satisfied:

1. Username length must be **exactly 8 characters**
2. First **4 characters** must be **uppercase alphabets**
3. 5th character must be **'@'**
4. Last **3 characters** must be a **course ID between 101 and 115 (inclusive)**

If any rule fails, display:
```
<username> is an invalid username
```

---

## Password Generation Logic

If the username is valid, generate the password as:

```
TECH_ + (sum of ASCII values of first 4 characters after converting to lowercase)
+ (last 2 digits of course ID)
```

---

## Solution (C# Implementation)

```csharp
using System;
using System.Text.RegularExpressions;

namespace PassWordGenerator
{
    public class PassWordGenerator
    {
        public static void Main(string[] args)
        {
            // Read username from the user
            Console.WriteLine("Enter your UserName:");
            string username = Console.ReadLine();

            // Validate username and generate password
            Console.WriteLine(validateUsername(username));
        }

        public static string validateUsername(string username)
        {
            string result = "TECH_";

            // Check minimum length condition
            if (username.Length < 8)
            {
                return $"{username} is an invalid username";
            }

            // Regex validation:
            // 4 uppercase letters + '@' + course ID between 101 and 115
            bool isValid = Regex.IsMatch(username, @"^[A-Z]{4}@(1[0-1][0-5])$");

            if (!isValid)
            {
                return $"{username} is an invalid username";
            }

            // Convert first four characters to lowercase
            string firstFour = username.Substring(0, 4).ToLower();
            int sum = 0;

            // Calculate ASCII sum
            foreach (char ch in firstFour)
            {
                sum += (int)ch;
            }

            result += sum;

            // Extract last two digits of course ID
            string lastTwoDigits = username.Substring(6, 2);
            result += lastTwoDigits;

            return "Password: " + result;
        }
    }
}
```

---

## Example

### Input

```
Enter your UserName:
LAJA@112
```

### Output

```
Password: TECH_40812
```

### Explanation

* First 4 letters → `LAJA` → `laja`
* ASCII sum → 108 + 97 + 106 + 97 = **408**
* Last two digits of course ID → **12**
* Final password → `TECH_40812`

---

## Key Concepts Used

* Regular Expressions for validation
* ASCII value calculation
* String manipulation
* Conditional checks
* Object-Oriented Programming

---

