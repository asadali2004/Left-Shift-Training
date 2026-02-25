using Microsoft.Extensions.Logging;
using Serilog;

// ─── STEP 1: Setup Serilog to write logs to date-wise files ───
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()   // also show logs in console
    .WriteTo.File(
        path: "logs/log-.txt",         // folder: logs, file: log-DATE.txt
        rollingInterval: RollingInterval.Day,  // new file every day
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// ─── Add separator for each run ───
Log.Information("========== APPLICATION RUN STARTED: {RunTime} ==========", DateTime.Now);

// ─── STEP 2: Connect Serilog with Microsoft's ILogger ───
using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSerilog();
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

// ─── STEP 3: Simulate app doing some work and logging ───
logger.LogInformation("Application started successfully.");

Console.WriteLine("\n--- Running App Tasks ---\n");

// Simulate Task 1 - Success
logger.LogInformation("Task 1: Processing order #1001 for customer John.");

// Simulate Task 2 - Warning
int stockLevel = 3;
if (stockLevel < 5)
{
    logger.LogWarning("Task 2: Stock level is low ({Stock} items left) for Product A.", stockLevel);
}

// Simulate Task 3 - Error
try
{
    logger.LogInformation("Task 3: Processing payment for order #1001.");
    throw new Exception("Payment gateway timed out!"); // fake error
}
catch (Exception ex)
{
    logger.LogError(ex, "Task 3 FAILED: Could not process payment for order #1001.");
}

// Simulate Task 4 - Critical
try
{
    logger.LogInformation("Task 4: Connecting to database.");
    throw new Exception("Database server is unreachable!"); // fake critical error
}
catch (Exception ex)
{
    logger.LogCritical(ex, "CRITICAL: Database connection lost. App cannot continue.");
}

logger.LogInformation("Application finished.");
Log.Information("========== APPLICATION RUN ENDED: {RunTime} ==========\n", DateTime.Now);

// Flush and close the log file properly
Log.CloseAndFlush();

Console.WriteLine("\nDone! Check the 'logs' folder for your log files.");
Console.ReadKey();