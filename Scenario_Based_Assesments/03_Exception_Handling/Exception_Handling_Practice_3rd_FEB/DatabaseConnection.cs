using System;

class DatabaseConnection
{
    static void Main()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SimulateOperation();
    }

    public void SimulateOperation()
    {
        string? connection = null;

        try
        {
            connection = OpenConnection();
            Console.WriteLine("Connection opened successfully.");
            
            // Simulate operation failure
            PerformOperation();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during operation: {ex.Message}");
        }
        finally
        {
            if (connection != null)
            {
                CloseConnection();
            }
        }
    }

    private string OpenConnection()
    {
        Console.WriteLine("Opening database connection...");
        return "Connected";
    }

    private void PerformOperation()
    {
        throw new InvalidOperationException("Operation failed: Timeout occurred");
    }

    private void CloseConnection()
    {
        Console.WriteLine("Connection closed properly.");
    }
}
