using System;

// Custom Exception for Login Attempts
public class MaxLoginAttemptsExceededException : Exception
{
    public MaxLoginAttemptsExceededException(string message) : base(message)
    {
    }
}

class LoginSystem
{
    static void Main()
    {
        int attempts = 0;
        int maxAttempts = 3;

        try
        {
            while (attempts < maxAttempts)
            {
                attempts++;
                Console.WriteLine($"Login Attempt {attempts}");
                
                // Simulate failed login
                if (attempts == maxAttempts)
                {
                    throw new MaxLoginAttemptsExceededException("Maximum login attempts exceeded!");
                }
            }
        }
        catch (MaxLoginAttemptsExceededException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Application terminated.");
        }
    }
}
