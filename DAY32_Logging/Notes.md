# 📘 .NET Logging — Complete Revision Notes
> **Topic:** Logging in C# .NET using Microsoft.Extensions.Logging + Serilog  
> **Level:** Beginner  
> **Purpose:** Quick revision reference — covers concepts, code, and explanations

---

## 📌 TABLE OF CONTENTS
1. [What is Logging?](#1-what-is-logging)
2. [Log Levels](#2-log-levels)
3. [Key Classes & Interfaces](#3-key-classes--interfaces)
4. [Serilog vs Microsoft Logging](#4-serilog-vs-microsoft-logging)
5. [Project Setup](#5-project-setup)
6. [Full Code Walkthrough](#6-full-code-walkthrough)
7. [Every Object & Class Explained](#7-every-object--class-explained)
8. [Date-wise Log Files Explained](#8-date-wise-log-files-explained)
9. [Output Template Placeholders](#9-output-template-placeholders)
10. [How the Flow Works (Big Picture)](#10-how-the-flow-works-big-picture)
11. [Common Mistakes to Avoid](#11-common-mistakes-to-avoid)
12. [Quick Reference Cheat Sheet](#12-quick-reference-cheat-sheet)

---

## 1. What is Logging?

Logging is the process of **recording events** that happen inside your application.

Think of it as a **diary for your app**. When something goes right or wrong, the app writes it down with a timestamp so you can investigate later.

### Why is it important?
- Helps you **debug errors** without running the app again
- Gives you a **history** of what happened and when
- In production apps, it's the only way to know what went wrong

### Real-world analogy
> A pilot's black box records everything during a flight. If something goes wrong, investigators replay the logs to find the cause. Your app's log file works the same way.

---

## 2. Log Levels

Every log message has a **severity level**. Levels go from least serious to most serious:

| Level | Value | Method | When to Use |
|---|---|---|---|
| `Trace` | 0 | `LogTrace()` | Ultra-detailed steps, every tiny action |
| `Debug` | 1 | `LogDebug()` | Debugging during development |
| `Information` | 2 | `LogInformation()` | Normal app flow ("User logged in") |
| `Warning` | 3 | `LogWarning()` | Something odd but app still works |
| `Error` | 4 | `LogError()` | Something failed, app keeps running |
| `Critical` | 5 | `LogCritical()` | App is about to crash, needs immediate fix |
| `None` | 6 | — | Turns off all logging |

### How Minimum Level works
If you set `MinimumLevel.Warning()`, then only **Warning, Error, and Critical** logs are saved. Trace, Debug, and Information are ignored.

```
MinimumLevel.Debug()  → captures: Debug, Info, Warning, Error, Critical
MinimumLevel.Warning() → captures: Warning, Error, Critical only
```

---

## 3. Key Classes & Interfaces

### From Microsoft (`Microsoft.Extensions.Logging`)

| Name | Type | What it does |
|---|---|---|
| `ILogger` | Interface | The tool you use to write logs |
| `ILogger<T>` | Interface | Same as ILogger but with a category set to class `T` |
| `ILoggerFactory` | Interface | Creates `ILogger` objects |
| `LoggerFactory` | Class | The concrete (real) implementation of `ILoggerFactory` |
| `LogLevel` | Enum | Defines the severity levels (Trace, Debug, Info...) |

### From Serilog

| Name | Type | What it does |
|---|---|---|
| `Log` | Static Class | Global logger, usable anywhere without creating objects |
| `LoggerConfiguration` | Class | Settings form — configure where and how to log |
| `RollingInterval` | Enum | Controls when a new log file is created (Day, Hour, Month...) |

---

## 4. Serilog vs Microsoft Logging

| Feature | Microsoft ILogger | Serilog |
|---|---|---|
| Built into .NET? | ✅ Yes | ❌ NuGet package needed |
| Write to files? | ❌ Not directly | ✅ Yes, with sinks |
| Date-wise files? | ❌ No | ✅ Yes, with RollingInterval |
| Structured logging? | ✅ Basic | ✅ Advanced |
| Best practice | Use ILogger in your classes | Use Serilog as the backend |

> **Best of both worlds:** Use **Microsoft's ILogger** in your code (so your code doesn't depend on Serilog directly), but configure **Serilog as the backend engine** using `builder.AddSerilog()`. This is exactly what we did.

---

## 5. Project Setup

### Step 1 — Create a Console App in Visual Studio
- File → New Project → Console App (C#)
- Framework: .NET 8.0
- Project name: `ErrorLogApp`

### Step 2 — Install NuGet Packages
Go to: **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**

Search and install:
```
Serilog.Sinks.File
Serilog.Extensions.Logging
```

### Step 3 — The `using` statements needed
```csharp
using Microsoft.Extensions.Logging;
using Serilog;
```

---

## 6. Full Code Walkthrough

```csharp
using Microsoft.Extensions.Logging;
using Serilog;

// ─── STEP 1: Configure Serilog ───
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// Mark the start of each app run in the log
Log.Information("========== APPLICATION RUN STARTED: {RunTime} ==========", DateTime.Now);

// ─── STEP 2: Connect Serilog with Microsoft ILogger ───
using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSerilog();
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

// ─── STEP 3: Log app events ───
logger.LogInformation("Application started successfully.");

Console.WriteLine("\n--- Running App Tasks ---\n");

// Task 1 — Normal
logger.LogInformation("Task 1: Processing order #1001 for customer John.");

// Task 2 — Warning
int stockLevel = 3;
if (stockLevel < 5)
{
    logger.LogWarning("Task 2: Stock level is low ({Stock} items left) for Product A.", stockLevel);
}

// Task 3 — Error
try
{
    logger.LogInformation("Task 3: Processing payment for order #1001.");
    throw new Exception("Payment gateway timed out!");
}
catch (Exception ex)
{
    logger.LogError(ex, "Task 3 FAILED: Could not process payment for order #1001.");
}

// Task 4 — Critical
try
{
    logger.LogInformation("Task 4: Connecting to database.");
    throw new Exception("Database server is unreachable!");
}
catch (Exception ex)
{
    logger.LogCritical(ex, "CRITICAL: Database connection lost. App cannot continue.");
}

logger.LogInformation("Application finished.");

Log.Information("========== APPLICATION RUN ENDED: {RunTime} ==========\n", DateTime.Now);

// Always call this at the end!
Log.CloseAndFlush();

Console.WriteLine("\nDone! Check the 'logs' folder for your log files.");
Console.ReadKey();
```

---

## 7. Every Object & Class Explained

### `Log` (Serilog Static Class)
```csharp
Log.Logger = ...
Log.Information("...")
Log.CloseAndFlush()
```
- A **static class** — no need to create an object, just use it directly
- Acts as a **global control panel** for Serilog
- `Log.Logger` is a property where you store your configured logger
- `Log.Information(...)` writes directly using Serilog (bypassing ILogger)
- `Log.CloseAndFlush()` — forces all buffered logs to be written to the file and closes it safely

---

### `LoggerConfiguration` (Serilog Class)
```csharp
new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(...)
    .CreateLogger()
```
- This is the **settings form** for Serilog
- Uses a **Fluent API** — methods chained one after another
- Each method returns the same object so you can keep adding settings
- `.CreateLogger()` — the final step, builds and returns the actual logger

---

### `.MinimumLevel.Debug()`
- Sets the **lowest level** of logs to capture
- `.Debug()` = capture Debug and everything above it
- Swap to `.Warning()` to ignore Debug/Info in production

---

### `.WriteTo` (Sinks)
- **Sink** = a destination for your logs
- `.WriteTo.Console()` → print to the console window
- `.WriteTo.File(...)` → save to a file on disk
- You can add **multiple sinks** — logs go to all of them simultaneously

---

### `RollingInterval.Day` (Enum)
```csharp
rollingInterval: RollingInterval.Day
```
- `RollingInterval` is an **enum** — a fixed list of named options
- `.Day` = create a **new log file every day** automatically
- Other options: `.Hour`, `.Month`, `.Year`, `.Infinite` (never roll)

---

### `ILoggerFactory` and `LoggerFactory`
```csharp
using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
    builder.AddSerilog();
});
```
- `ILoggerFactory` is an **interface** (a contract saying "you must have CreateLogger()")
- `LoggerFactory` is the **real class** that implements that interface
- `.Create(...)` is a static method that builds the factory
- `builder.AddSerilog()` — tells Microsoft's logging: "use Serilog as the engine"
- The `using` keyword ensures it gets **cleaned up automatically** when done

---

### Lambda / `builder =>`
```csharp
LoggerFactory.Create(builder => {
    builder.AddSerilog();
})
```
- `builder =>` is a **lambda** (an inline anonymous function)
- `builder` is of type `ILoggingBuilder` — Microsoft's configuration object
- You receive it, configure it, and the factory uses your configuration

---

### `ILogger<Program>`
```csharp
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
```
- `ILogger<T>` — the logging interface, generic type `T` sets the **category**
- `<Program>` means: "this logger belongs to the Program class"
- The category appears in logs and helps you filter/search
- `loggerFactory.CreateLogger<Program>()` — creates the logger for this category

---

### Log Methods
```csharp
logger.LogInformation("Message here");
logger.LogWarning("Something odd: {Value}", someVariable);
logger.LogError(ex, "Something failed");
logger.LogCritical(ex, "App cannot continue");
```
- Each method corresponds to a log level
- Pass `ex` (exception) as the **first argument** in LogError/LogCritical to capture full error details
- `{Value}` is a **placeholder** — replaced by `someVariable` at runtime

---

### `try / catch / throw`
```csharp
try {
    throw new Exception("Something went wrong!");
}
catch (Exception ex) {
    logger.LogError(ex, "Failed to do something.");
}
```
- `try` block = code that might fail
- `throw new Exception(...)` = manually create and throw an error (simulating real failures)
- `catch (Exception ex)` = catches the error, stores it in `ex`
- `ex` contains the error message + **stack trace** (the path of method calls that caused it)
- Passing `ex` to `LogError` saves all that detail to your log file

---

### `DateTime.Now`
```csharp
Log.Information("Run started: {RunTime}", DateTime.Now);
```
- Built-in C# property that returns the **current date and time**
- Used here to timestamp each application run

---

### `Console.ReadKey()`
```csharp
Console.ReadKey();
```
- Pauses the console window, waits for you to press any key
- Without this, the window closes instantly after the program ends

---

## 8. Date-wise Log Files Explained

### How the file path works
```csharp
path: "logs/log-.txt"
```
- `logs/` = creates a folder called `logs`
- `log-` = file name prefix
- The `-` before `.txt` is where Serilog **inserts the date automatically**
- Result: `logs/log-20260225.txt`

### New file every day
```csharp
rollingInterval: RollingInterval.Day
```
- Every time the date changes, Serilog creates a **new file**
- Old files are kept — you get a complete history

### Where to find the files
After running the app in Visual Studio, look here:
```
YourProject/
  └── bin/
       └── Debug/
            └── net8.0/
                 └── logs/
                      └── log-20260225.txt   ← Today's log
                      └── log-20260224.txt   ← Yesterday's log
```

---

## 9. Output Template Placeholders

```
"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
```

| Placeholder | Meaning | Example Output |
|---|---|---|
| `{Timestamp:yyyy-MM-dd HH:mm:ss}` | Date and time of the log | `2026-02-25 10:30:01` |
| `{Level:u3}` | Log level, 3 uppercase letters | `INF`, `WRN`, `ERR`, `CRT` |
| `{Message:lj}` | The log message text | `Application started` |
| `{NewLine}` | Line break after message | (enters new line) |
| `{Exception}` | Full exception + stack trace if any | long error details |

### Example output in the file
```
2026-02-25 10:30:01 [INF] Application started successfully.
2026-02-25 10:30:02 [WRN] Stock level is low (3 items left) for Product A.
2026-02-25 10:30:03 [ERR] Task 3 FAILED: Could not process payment for order #1001.
System.Exception: Payment gateway timed out!
   at Program.<Main>$(String[] args) in Program.cs:line 42
```

---

## 10. How the Flow Works (Big Picture)

```
Your Code
    │
    ▼
logger.LogError(ex, "Something failed")   ← You call this
    │
    ▼
ILogger<Program>                           ← Microsoft's interface
    │
    ▼
ILoggerFactory (with AddSerilog)           ← Routes to Serilog
    │
    ▼
Serilog Logger                             ← Processes the log
    │
    ├──────────────────────────────────────► Console Sink
    │                                            └── Prints to screen
    │
    └──────────────────────────────────────► File Sink
                                                 └── logs/log-20260225.txt
```

### Why this two-layer approach?
- Your code only depends on **Microsoft's ILogger** (standard .NET)
- If you ever want to switch from Serilog to something else, you only change the setup — **not all your code**
- This is called **Dependency Inversion** — a good coding principle

---

## 11. Common Mistakes to Avoid

| Mistake | Problem | Fix |
|---|---|---|
| Forgetting `Log.CloseAndFlush()` | Last few logs might not be saved | Always add it at the very end |
| Not passing `ex` to `LogError` | Exception details lost | Use `logger.LogError(ex, "message")` |
| Using string interpolation in log messages | Performance issue | Use placeholders: `"Value is {Val}", val` |
| Forgetting `using` on `ILoggerFactory` | Memory leak | Always use `using ILoggerFactory ...` |
| Setting `MinimumLevel.Trace()` in production | Huge log files, performance hit | Use `Warning` or `Error` in production |

---

## 12. Quick Reference Cheat Sheet

### Setup Serilog (do once at the start)
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

### Connect Serilog to Microsoft ILogger
```csharp
using ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddSerilog());
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
```

### Write different log levels
```csharp
logger.LogTrace("Very detailed step");
logger.LogDebug("Debug info: {Value}", someVar);
logger.LogInformation("Normal event: {Event}", eventName);
logger.LogWarning("Watch out: {Issue}", issueDetails);
logger.LogError(ex, "Error occurred: {Context}", context);
logger.LogCritical(ex, "Critical failure: {Reason}", reason);
```

### Always end with
```csharp
Log.CloseAndFlush();
```

### NuGet packages needed
```
Serilog.Sinks.File
Serilog.Extensions.Logging
```

---
