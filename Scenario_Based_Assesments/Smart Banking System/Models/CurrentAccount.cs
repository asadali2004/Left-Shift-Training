using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models;

public class CurrentAccount : BankAccount
{
    private const double OVERDRAFT_LIMIT = 50000;
    private const double INTEREST_RATE = 0.0; // No interest

    public CurrentAccount(int accountNumber, string customerName, double initialBalance)
        : base(accountNumber, customerName, initialBalance)
    {
        AccountType = "Current Account";
    }

    public override void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Withdrawal amount must be greater than zero!");

        if (amount > Balance + OVERDRAFT_LIMIT)
            throw new InsufficientBalanceException($"Cannot exceed overdraft limit! Available: ${Balance + OVERDRAFT_LIMIT:F2}");

        Balance -= amount;
        TransactionHistory.Add($"[{DateTime.Now}] Withdrawn: ${amount}. New Balance: ${Balance}");
        Console.WriteLine($"✓ Withdrawal successful! New Balance: ${Balance:F2}");
    }

    public override double CalculateInterest()
    {
        return INTEREST_RATE; // No interest for current account
    }
}
