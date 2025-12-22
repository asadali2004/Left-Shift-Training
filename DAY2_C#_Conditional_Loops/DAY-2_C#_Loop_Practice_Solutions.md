## Practice on Loops

**PQ1: Fibonacci Series**
Fibonacci Series: Display the first N terms of the Fibonacci sequence.
```csharp
/// <summary>
/// This is Programm for N fabonacci Series 
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter your Number for Fabonacci Series:");
            int Number = int.Parse(Console.ReadLine()!);
            int firstNumber = 0, secondNumber = 1, nextNumber;
            Console.WriteLine("Fabonacci Series:");
            #endregion

            #region Calculation
            for (int i = 0; i < Number; i++)
            {
                if (i <= 1)
                {
                    nextNumber = i;
                }
                else
                {
                    nextNumber = firstNumber + secondNumber;
                    firstNumber = secondNumber;
                    secondNumber = nextNumber;
                }
                Console.WriteLine(nextNumber);
                
            }
            #endregion
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```
---

---

**PQ2: Prime Number**
Check if a number is prime using a for loop and break.

```csharp
using System;
/// <summary>
// Check if a number is prime using a for loop and break.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter a number to check if it is prime:");
            int number = int.Parse(Console.ReadLine());
            bool isPrime = true;

            #endregion

            #region Prime Check Logic
            if (number <= 1)
            {
                isPrime = false;
            }
            else
            {
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }
            Console.WriteLine(isPrime ? "The number is prime." : "The number is not prime.");
            
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

**PQ3: Armstrong Number**
Check if a number equals the sum of its digits raised to the power of number of digits.

```csharp
using System;
/// <summary>
//Check if a number equals the sum of its digits raised to the power of number of digits.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter a number:");
            int number = int.Parse(Console.ReadLine());


            #endregion

            #region Logic
            int originalNumber = number;
            int sum = 0;
            int numberOfDigits = number.ToString().Length;
            while (number > 0)
            {
                int digit = number % 10;
                sum += (int)Math.Pow(digit, numberOfDigits);
                number /= 10;
            }
            if (sum == originalNumber)
            {
                Console.WriteLine($"{originalNumber} is equal to the sum of its digits raised to the power of number of digits.");
            }
            else
            {
                Console.WriteLine($"{originalNumber} is not equal to the sum of its digits raised to the power of number of digits.");
            }
            
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

**PQ4: Reverse & Palindrome**
Reverse an integer and check if it is a palindrome using while.

```csharp
using System;
/// <summary>
// Reverse an integer and check if it is a palindrome using while.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter an integer:");
            int number = Convert.ToInt32(Console.ReadLine());
            int reversedNumber = 0;

            #endregion

            #region Reversing the Integer
            int tempNumber = number;
            while (tempNumber != 0)
            {
                int digit = tempNumber % 10;
                reversedNumber = reversedNumber * 10 + digit;
                tempNumber /= 10;
            }
            
            Console.WriteLine("Reversed Number: " + reversedNumber);
            Console.WriteLine("Is Palindrome: " + (number == reversedNumber));
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

**PQ5: GCD and LCM**
Find the Greatest Common Divisor and Least Common Multiple of two numbers.

```csharp
using System;
/// <summary>
// Find the Greatest Common Divisor and Least Common Multiple of two numbers.

/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter first number:");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            int num2 = int.Parse(Console.ReadLine());

            #endregion

            #region GCD Calculation
            int a = num1;
            int b = num2;
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            int gcd = a;
            int lcm = (num1 * num2) / gcd;
            Console.WriteLine("Greatest Common Divisor (GCD): " + gcd);
            Console.WriteLine("Least Common Multiple (LCM): " + lcm);   
        
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

**PQ6: Pascal's Triangle**
Use nested loops to print Pascal's triangle up to N rows.

```csharp
```

---

**PQ7: Binary to Decimal**
Convert a binary number string to decimal without using built-in library functions.

```csharp
```

---

**PQ8: Diamond Pattern**
Print a diamond shape using `*` characters with nested loops.

```csharp
```

---

**PQ9: Factorial (Large Numbers)**
Calculate N! and handle potential overflow for larger integers.

```csharp
```

---

**PQ10: Guessing Game**
Use do-while to let a user guess a secret number until they get it right.

```csharp
```

---

**PQ11: Sum of Digits (Digital Root)**
Repeatedly sum digits of a number until the result is a single digit.

```csharp
```

---

**PQ12: Continue Usage**
Print numbers from 1 to 50, but skip all multiples of 3 using `continue`.

```csharp
```

---

**PQ13: Menu System**
Use do-while and switch to create a persistent console menu.

```csharp
```

---

**PQ14: Strong Number**
Check if the sum of the factorial of digits is equal to the number itself.

```csharp
```

---

**PQ15: Search with Goto**
Implement a deep-nested loop search that uses `goto` to exit all levels instantly upon finding a result.

```csharp
```

---
