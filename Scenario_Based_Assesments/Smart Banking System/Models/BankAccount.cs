using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models;

public abstract class BankAccount
{
    public int AccountNumber { get; set; }
    public string CustomerName { get; set; } = "";
    public double Balance { get; set; }
    public List<string> TransactionHistory { get; set; }
    public string AccountType { get; set; } = "";

    public BankAccount(int accountNumber, string customerName, double initialBalance)
    {
        AccountNumber = accountNumber;
        CustomerName = customerName;
        Balance = initialBalance;
        TransactionHistory = new List<string>();
        TransactionHistory.Add($"[{DateTime.Now}] Account Created with Initial Balance: ${initialBalance}");
    }

    public virtual void Deposit(double amount)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Deposit amount must be greater than zero!");

        Balance += amount;
        TransactionHistory.Add($"[{DateTime.Now}] Deposited: ${amount}. New Balance: ${Balance}");
        Console.WriteLine($"✓ Deposit successful! New Balance: ${Balance:F2}");
    }

    public virtual void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Withdrawal amount must be greater than zero!");

        if (amount > Balance)
            throw new InsufficientBalanceException($"Insufficient balance! Available: ${Balance:F2}");

        Balance -= amount;
        TransactionHistory.Add($"[{DateTime.Now}] Withdrawn: ${amount}. New Balance: ${Balance}");
        Console.WriteLine($"✓ Withdrawal successful! New Balance: ${Balance:F2}");
    }

    public abstract double CalculateInterest();

    public void DisplayTransactionHistory()
    {
        Console.WriteLine($"\n--- Transaction History for {CustomerName} (Account: {AccountNumber}) ---");
        foreach (var transaction in TransactionHistory)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine();
    }
}
