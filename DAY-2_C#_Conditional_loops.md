```
# C# Notes & Practice Programs

## Note:
- **Main method** is the entry point in C#.
- **Comments** are important in industry-level programming (always provide a summary for methods and programs).
- **Namespace** of a project usually comes from the solution name declared.
- In **C#/.NET**, the names of **Classes, Functions, and Properties** start with a capital letter.
- **Instance variables and methods** can be removed from program memory by **Garbage Collection**, but **static variables** stay until program exit.
- **Variable declaration should not be inside loops** (it allocates memory repeatedly).
- `?` declaration method usage: **static vs object declaration**.
- `#region` is important for code organization  
  (e.g., `#region Variable Declaration` → `#endregion`).

```

## Started Conditionals and Loops

### Program (Fine Tuned)

```csharp
using System;

/// <summary>
/// This is Sample Programm
/// </summary>
class Program
{
    /// <summary>
    /// This is checking given number is Even or Odd.
    /// </summary>
    /// <param name="num">This is Input to check even or odd.</param>
    /// <returns>True or False</returns>
    public bool IsEven(int num)
    {
        return num % 2 == 0;
    }

    /// <summary>
    /// This is Main Method of the Program
    /// </summary>
    public static void Main()
    {
        Program pro = new Program();

        #region Variable Initialization and Declarations
        Console.Write("Enter the number to find Odd or Even (q for quit): ");
        string? choice = Console.ReadLine();   // nullable to avoid CS8600
        int lNumber = 0;
        bool checkResult = false;
        string output = string.Empty;
        #endregion

        #region Process and Output
        while (!string.Equals(choice?.Trim(), "q", StringComparison.OrdinalIgnoreCase))
        {
            if (int.TryParse(choice, out lNumber))
            {
                checkResult = pro.IsEven(lNumber);
                output = checkResult ? "Even" : "Odd";
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number or 'q' to quit.");
            }

            Console.Write("Enter the number to find Odd or Even (q for quit): ");
            choice = Console.ReadLine();
        }
        #endregion

        Console.WriteLine("Program exited.");
    }
}
````

---

## Practice on Conditionals

### Practice Q1: Height Category

Accept height in cm:

* `< 150` → Dwarf
* `150–165` → Average
* `165–190` → Tall
* `> 190` → Abnormal

```csharp
/// <summary>
/// This is Programm for Height Category
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    public static void Main(String[] args)
    {
        #region Declaration and User Input
        Console.WriteLine("Enter a Number to check its Category");
        string? input = Console.ReadLine();
        int Height = 0;
        #endregion

        #region Check Condition
        if (int.TryParse(input, out Height))
        {
            if (Height < 150) Console.WriteLine("Dwarf");
            else if (Height >= 150 && Height < 165) Console.WriteLine("Average");
            else if (Height >= 165 && Height < 190) Console.WriteLine("Tall");
            else Console.WriteLine("Abnormal");
        }
        else
        {
            Console.WriteLine("Invalid Input");
        }
        #endregion
    }
}
```

---

### Practice Q2: Largest of Three

Find the maximum using **nested if**.

```csharp
/// <summary>
/// This is Programm for Maximum of three
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    public static void Main(String[] args)
    {
        #region Declaration and User Input
        Console.WriteLine("Enter first Number to check Maximum");
        int firstNumber = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter second Number to check Maximum");
        int secondNumber = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter third Number to check Maximum");
        int thirdNumber = int.Parse(Console.ReadLine()!);
        #endregion

        #region Business Logic
        if (firstNumber > secondNumber)
        {
            if (firstNumber > thirdNumber)
                Console.WriteLine("Maximum is " + firstNumber);
            else
                Console.WriteLine("Maximum is " + thirdNumber);
        }
        else
        {
            if (secondNumber > thirdNumber)
                Console.WriteLine("Maximum is " + secondNumber);
            else
                Console.WriteLine("Maximum is " + thirdNumber);
        }
        #endregion
    }
}
```

---

### Practice Q3: Leap Year Checker

Leap year rule:

* Divisible by **400**
* OR divisible by **4** and **NOT** divisible by **100**

```csharp
/// <summary>
/// This is Programm for Checking Leap Year
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter a year to check Leap Year or Not");
            int year = int.Parse(Console.ReadLine()!);
            #endregion

            #region Business Logic
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 == 0)
                        Console.WriteLine("{0} is a Leap Year", year);
                    else
                        Console.WriteLine("{0} is not a Leap Year", year);
                }
                else
                {
                    Console.WriteLine("{0} is a Leap Year", year);
                }
            }
            else
            {
                Console.WriteLine("{0} is not a Leap Year", year);
            }
            #endregion
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid year.");
        }
    }
}
```

---

### Practice Q4: Quadratic Equation

Calculate roots of
[
ax^2 + bx + c = 0
]

```csharp
/// <summary>
/// This is Programm for Checking Quadratic Equation ax^2 + bx + c = 0
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter FirstNumber of Quadratic Equation:");
            int firstNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter SecondNumber of Quadratic Equation:");
            int secondNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter ThirdNumber of Quadratic Equation:");
            int thirdNumber = int.Parse(Console.ReadLine()!);
            #endregion

            #region Calculation
            double discriminant = (secondNumber * secondNumber) - (4 * firstNumber * thirdNumber);

            if (discriminant > 0)
            {
                double root1 = (-secondNumber + Math.Sqrt(discriminant)) / (2 * firstNumber);
                double root2 = (-secondNumber - Math.Sqrt(discriminant)) / (2 * firstNumber);
                Console.WriteLine($"Roots are real and different. Root1: {root1}, Root2: {root2}");
            }
            else if (discriminant == 0)
            {
                double root = -secondNumber / (2 * firstNumber);
                Console.WriteLine($"Roots are real and same. Root: {root}");
            }
            else
            {
                Console.WriteLine("Roots are complex and different.");
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

### Practice Q5: Admission Eligibility

Criteria:

* Math ≥ 65
* Physics ≥ 55
* Chemistry ≥ 50
* AND `(Total ≥ 180 OR Math + Physics ≥ 140)`

```csharp
/// <summary>
/// This is Programm for Checking Admission Eligibility
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter your Math Marks:");
            int MathMarks = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter your Physics Marks:");
            int PhysicsMarks = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Your Chemistry Marks:");
            int ChemistryMarks = int.Parse(Console.ReadLine()!);

            int sum = MathMarks + PhysicsMarks + ChemistryMarks;
            int PhysicsMathSum = MathMarks + PhysicsMarks;
            #endregion

            #region Calculation
            if (MathMarks >= 65 && PhysicsMarks >= 55 && ChemistryMarks >= 50)
            {
                if (sum >= 180 || PhysicsMathSum >= 140)
                    Console.WriteLine("You are Eligible for admission");
                else
                    Console.WriteLine("You are Not Eligible for admission");
            }
            else
            {
                Console.WriteLine("You are Not Eligible for admission");
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

## Practice on Loops

### PQ1: Fibonacci Series

Display the **first N terms** of the Fibonacci sequence.

```
```
