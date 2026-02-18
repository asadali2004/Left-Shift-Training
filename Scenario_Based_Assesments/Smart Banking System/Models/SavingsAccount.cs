using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models;

public class SavingsAccount : BankAccount
{
    private const double MINIMUM_BALANCE = 5000;
    private const double INTEREST_RATE = 0.04; // 4% per annum

    public SavingsAccount(int accountNumber, string customerName, double initialBalance)
        : base(accountNumber, customerName, initialBalance)
    {
        AccountType = "Savings Account";
        if (initialBalance < MINIMUM_BALANCE)
            throw new MinimumBalanceException($"Initial balance must be at least ${MINIMUM_BALANCE}");
    }

    public override void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Withdrawal amount must be greater than zero!");

        if (amount > Balance)
            throw new InsufficientBalanceException($"Insufficient balance! Available: ${Balance:F2}");

        if (Balance - amount < MINIMUM_BALANCE)
            throw new MinimumBalanceException($"Cannot withdraw! Minimum balance of ${MINIMUM_BALANCE} must be maintained.");

        Balance -= amount;
        TransactionHistory.Add($"[{DateTime.Now}] Withdrawn: ${amount}. New Balance: ${Balance}");
        Console.WriteLine($"✓ Withdrawal successful! New Balance: ${Balance:F2}");
    }

    public override double CalculateInterest()
    {
        double interest = Balance * INTEREST_RATE / 12; // Monthly interest
        Balance += interest;
        TransactionHistory.Add($"[{DateTime.Now}] Interest Credited: ${interest:F2}. New Balance: ${Balance}");
        return interest;
    }
}
