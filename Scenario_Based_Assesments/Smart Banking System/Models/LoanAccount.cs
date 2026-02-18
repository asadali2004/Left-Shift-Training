using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models;

public class LoanAccount : BankAccount
{
    private const double INTEREST_RATE = 0.08; // 8% per annum
    public double LoanAmount { get; set; }

    public LoanAccount(int accountNumber, string customerName, double loanAmount)
        : base(accountNumber, customerName, 0)
    {
        AccountType = "Loan Account";
        LoanAmount = loanAmount;
        Balance = loanAmount; // Balance represents outstanding loan
        TransactionHistory.Add($"[{DateTime.Now}] Loan Issued: ${loanAmount}");
    }

    public override void Deposit(double amount)
    {
        throw new InvalidTransactionException("Cannot deposit into Loan Account! Use Withdraw to pay loan.");
    }

    public override void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Loan payment amount must be greater than zero!");

        if (amount > Balance)
            throw new InsufficientBalanceException($"Payment cannot exceed outstanding loan! Outstanding: ${Balance:F2}");

        Balance -= amount;
        TransactionHistory.Add($"[{DateTime.Now}] Loan Payment: ${amount}. Remaining Outstanding: ${Balance}");
        Console.WriteLine($"✓ Loan payment successful! Remaining Outstanding: ${Balance:F2}");
    }

    public override double CalculateInterest()
    {
        double interest = Balance * INTEREST_RATE / 12; // Monthly interest
        Balance += interest;
        TransactionHistory.Add($"[{DateTime.Now}] Interest Applied: ${interest:F2}. New Outstanding: ${Balance}");
        return interest;
    }
}
