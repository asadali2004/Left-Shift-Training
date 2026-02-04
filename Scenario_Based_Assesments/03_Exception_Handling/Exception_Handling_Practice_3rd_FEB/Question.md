# C# Exception Handling: Coding Exercises

**Instruction**: Associates must implement the exception handling parts (marked as TODO). Do not add solutions here.

---

## 1️⃣ Banking Withdrawal Validation

```csharp
using System;

class BankAccount
{
    static void Main()
    {
        int balance = 10000;

        Console.WriteLine("Enter withdrawal amount:");
        int amount = int.Parse(Console.ReadLine());

        // TODO:
        // 1. Throw exception if amount <= 0
        // 2. Throw exception if amount > balance
        // 3. Deduct amount if valid
        // 4. Use finally block to log transaction
    }
}
```

**Key Concepts**: Input validation, exception throwing, finally block

---

## 2️⃣ File Reading with Resource Safety

```csharp
using System;
using System.IO;

class FileReader
{
    static void Main()
    {
        string filePath = "data.txt";

        // TODO:
        // 1. Read file content
        // 2. Handle FileNotFoundException
        // 3. Handle UnauthorizedAccessException
        // 4. Ensure resource is closed properly
    }
}
```

**Key Concepts**: File I/O, specific exception handling, resource management

---

## 3️⃣ Employee Bonus Processing

```csharp
using System;

class BonusCalculator
{
    static void Main()
    {
        int[] salaries = { 5000, 0, 7000 };

        // TODO:
        // 1. Loop through salaries
        // 2. Divide bonus by salary
        // 3. Handle DivideByZeroException
        // 4. Continue processing remaining employees
    }
}
```

**Key Concepts**: DivideByZeroException, loop exception handling, continued execution

---

## 4️⃣ Custom Exception – Login Attempts

```csharp
using System;

class LoginSystem
{
    static void Main()
    {
        int attempts = 0;

        // TODO:
        // 1. Allow only 3 login attempts
        // 2. Create and throw custom exception after limit
        // 3. Handle exception and terminate application
    }
}
```

**Key Concepts**: Custom exceptions, exception creation, application termination

---

## 5️⃣ Exception Propagation Across Layers

```csharp
using System;

class Controller
{
    static void Main()
    {
        // TODO:
        // Call Service method
        // Handle exception here
    }
}

class Service
{
    public static void Process()
    {
        // TODO:
        // Call Repository method
        // Catch, log and rethrow exception
    }
}

class Repository
{
    public static void GetData()
    {
        // TODO:
        // Throw an exception here
    }
}
```

**Key Concepts**: Exception propagation, layered architecture, exception re-throwing

---

## 6️⃣ Safe Numeric Input Handling

```csharp
using System;

class InputHandler
{
    static void Main()
    {
        // TODO:
        // 1. Read input from user
        // 2. Handle invalid numeric input
        // 3. Keep asking until valid number is entered
    }
}
```

**Key Concepts**: FormatException, input validation loop

---

## 7️⃣ Database Connection Simulation

```csharp
using System;

class DatabaseConnection
{
    static void Main()
    {
        // TODO:
        // 1. Open connection
        // 2. Simulate operation failure
        // 3. Ensure connection is closed properly
    }
}
```

**Key Concepts**: Try-catch-finally, resource cleanup

---

## 8️⃣ File Upload Validation

```csharp
using System;

class FileUpload
{
    static void Main()
    {
        string fileName = "report.exe";
        int fileSize = 8; // MB

        // TODO:
        // 1. Validate file extension
        // 2. Validate file size
        // 3. Throw and handle appropriate exceptions
    }
}
```

**Key Concepts**: Data validation, custom exceptions

---

## 9️⃣ Rethrowing Exception Correctly

```csharp
using System;

class ExceptionRethrow
{
    static void Main()
    {
        try
        {
            ProcessData();
        }
        catch (Exception)
        {
            // TODO:
            // Handle final exception
        }
    }

    static void ProcessData()
    {
        try
        {
            int.Parse("ABC");
        }
        catch (Exception)
        {
            // TODO:
            // Log exception
            // Rethrow while preserving stack trace
        }
    }
}
```

**Key Concepts**: Exception rethrowing, stack trace preservation, logging

---

## 🔟 Order Processing System

```csharp
using System;

class OrderProcessor
{
    static void Main()
    {
        int[] orders = { 101, -1, 103 };

        // TODO:
        // 1. Process each order
        // 2. Throw exception for invalid order ID
        // 3. Ensure one failure does not stop processing
    }
}
```

**Key Concepts**: Loop exception handling, graceful error recovery

---

## 📚 Learning Objectives

✅ Understand when and how to throw exceptions  
✅ Handle specific exception types appropriately  
✅ Use finally blocks for cleanup operations  
✅ Create custom exceptions  
✅ Implement exception propagation across layers  
✅ Practice defensive programming with input validation  
✅ Master stack trace preservation when rethrowing  
✅ Implement robust error recovery mechanisms
