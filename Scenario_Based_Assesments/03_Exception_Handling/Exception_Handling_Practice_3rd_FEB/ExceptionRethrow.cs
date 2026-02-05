using System;

class ExceptionRethrow
{
    static void Main()
    {
        try
        {
            ProcessData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Main caught final exception: {ex.Message}");
            Console.WriteLine($"Stack trace preserved: {ex.StackTrace}");
        }
    }

    static void ProcessData()
    {
        try
        {
            int.Parse("ABC");
        }
        catch (Exception ex)
        {
            // Log exception
            Console.WriteLine($"ProcessData logging exception: {ex.Message}");
            Console.WriteLine($"Exception type: {ex.GetType().Name}");

            // Rethrow while preserving stack trace
            throw;
        }
    }
}
