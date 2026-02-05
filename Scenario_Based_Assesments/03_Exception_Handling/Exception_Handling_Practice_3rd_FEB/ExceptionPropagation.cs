using System;

class ExceptionPropagation
{
    static void Main()
    {
        try
        {
            Controller.ProcessRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Final Exception Caught: {ex.Message}");
        }
    }
}

class Controller
{
    public static void ProcessRequest()
    {
        try
        {
            Service.Process();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Controller caught: {ex.Message}");
            throw;
        }
    }
}

class Service
{
    public static void Process()
    {
        try
        {
            Repository.GetData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Service caught and logging: {ex.Message}");
            throw;
        }
    }
}

class Repository
{
    public static void GetData()
    {
        throw new InvalidOperationException("Database error: Connection failed");
    }
}
