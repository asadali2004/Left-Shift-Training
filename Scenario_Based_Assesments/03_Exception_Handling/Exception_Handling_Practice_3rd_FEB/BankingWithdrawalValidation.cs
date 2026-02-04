using System;

class BankAccount
{
    static void Main()
    {
        int balance = 10000;
        Console.WriteLine("Enter withdrawal amount:");
        int amount = int.Parse(Console.ReadLine()!);

        // TODO:
        // 1. Throw exception if amount <= 0
        // 2. Throw exception if amount > balance
        // 3. Deduct amount if valid
        // 4. Use finally block to log transaction
        try
        {
            // Validate amount
            if (amount <= 0)
            {
                throw new BankValidAmountException("Amount must be greater than 0.");
            }
            if (amount > balance)
            {
                throw new BankValidAmountException("Insufficient balance.");
            }
            // Deduct if valid
            balance -= amount;
            System.Console.WriteLine("Transaction successful.");
            System.Console.WriteLine($"Remaining balance: {balance}");

        }
        catch (BankValidAmountException ex)
        {
            // Handle validation errors
            System.Console.WriteLine(ex.Message);
        }
        finally
        {
            // Always log the transaction
            System.Console.WriteLine("Transaction Logged");
        }
    }
}
class BankValidAmountException: Exception
{
    public BankValidAmountException(string message) : base(message)
    {
        
    }
}