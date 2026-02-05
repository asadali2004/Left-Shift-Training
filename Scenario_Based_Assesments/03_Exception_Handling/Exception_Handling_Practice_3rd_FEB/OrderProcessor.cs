using System;

class OrderProcessor
{
    static void Main()
    {
        int[] orders = { 101, -1, 103 };
        int successfulOrders = 0;
        int failedOrders = 0;

        for (int i = 0; i < orders.Length; i++)
        {
            try
            {
                ProcessOrder(orders[i]);
                successfulOrders++;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error processing order {i + 1}: {ex.Message}");
                failedOrders++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error for order {i + 1}: {ex.Message}");
                failedOrders++;
            }
        }

        Console.WriteLine($"\nProcessing Summary:");
        Console.WriteLine($"Successful Orders: {successfulOrders}");
        Console.WriteLine($"Failed Orders: {failedOrders}");
    }

    static void ProcessOrder(int orderId)
    {
        if (orderId <= 0)
        {
            throw new InvalidOperationException($"Invalid order ID: {orderId}. Order ID must be positive.");
        }

        Console.WriteLine($"Processing order {orderId}...");
        Console.WriteLine($"Order {orderId} processed successfully!");
    }
}
