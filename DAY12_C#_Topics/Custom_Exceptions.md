# C# Custom Exceptions

- Objectives: create meaningful custom exceptions and use them to provide user-friendly messages while preserving system errors for logging.

## Overview

- Inherit from `Exception` to define domain-specific errors.
- Prefer including the original cause via `innerException` and avoid swallowing system messages.

## Basic custom exception

```csharp
using System;

namespace Coding_Class
{
    public class AppCustomException : Exception
    {
        public override string Message => "Internal Exception";
    }
}
```

## Logging system errors and returning friendly messages

```csharp
using System;

namespace Coding_Class
{
    public class AppCustomException : Exception
    {
        public AppCustomException(string systemMessage, Exception inner = null)
            : base(systemMessage, inner) {}

        public override string Message => HandleBase(base.Message);

        private static string HandleBase(string systemMessage)
        {
            Console.WriteLine("Log to File : " + systemMessage);
            return "Internal Exception. Contact Admin.";
        }
    }
}
```

## Usage example (division with error handling)

```csharp
using System;

namespace Coding_Class
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                int result = Divide(10, 0);
                Console.WriteLine("The Result is: " + result);
            }
            catch (AppCustomException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static int Divide(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch (Exception e)
            {
                throw new AppCustomException("Division failed: " + e.Message, e);
            }
        }
    }
}
```

## Tips

- Provide multiple constructors (default, message, message + innerException).
- Avoid overusing overrides of `Message`; prefer storing details and exposing safe messages via application logic.
- Log technical details; return friendly messages to users.
