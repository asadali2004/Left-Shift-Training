using System;

public class Program
{
    private static int _tries = 0;                    // Simulation counter

    public static void Main()
    {
        // A function that fails twice, then succeeds
        int result = ExecuteWithRetry(() =>
        {
            _tries++;
            if (_tries <= 2) throw new InvalidOperationException("Temporary failure");
            return 999;
        }, maxAttempts: 3);

        Console.WriteLine(result);                    // Expected: 999
    }

    // ✅ TODO: Students implement only this function
    public static T ExecuteWithRetry<T>(Func<T> work, int maxAttempts)
    {
        // TODO:
        // 1) Validate inputs
        // 2) Try executing work
        // 3) If exception occurs and attempts remain, retry
        // 4) If attempts exhausted, throw last exception
        if (work == null)
        {
            throw new ArgumentNullException(nameof(work));
        }
        if(maxAttempts <= 0)
        {
            throw new ArgumentNullException(nameof(maxAttempts));
        }
        Exception? lastException = null;
    
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                return work();  // Try to execute and return result
            }
            catch (Exception ex)
            {
                lastException = ex;  // Save the exception
                
                if (attempt == maxAttempts)
                {
                    throw;  // Last attempt failed, re-throw the exception
                }
                // Otherwise, loop continues to retry
            }
        }
        
        // This should never be reached, but needed to satisfy compiler
        throw lastException!;
    }
}