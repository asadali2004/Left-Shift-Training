using SmartBankingSystem.Models;
using SmartBankingSystem.Services;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.UI;

public static class AccountOperations
{
    public static void CreateAccount(BankingSystem bank)
    {
        Console.WriteLine("\n--- Create Account ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Customer Name: ");
        string customerName = Console.ReadLine() ?? "";

        Console.WriteLine("Select Account Type:");
        Console.WriteLine("1. Savings Account (Min Balance: $5000, Interest: 4% p.a.)");
        Console.WriteLine("2. Current Account (Overdraft: $50000, No Interest)");
        Console.WriteLine("3. Loan Account (Interest: 8% p.a.)");
        Console.Write("Enter choice (1-3): ");
        string? accountType = Console.ReadLine();

        Console.Write("Enter Initial Amount: ");
        double amount = double.Parse(Console.ReadLine() ?? "0");

        BankAccount account = accountType switch
        {
            "1" => new SavingsAccount(accountNumber, customerName, amount),
            "2" => new CurrentAccount(accountNumber, customerName, amount),
            "3" => new LoanAccount(accountNumber, customerName, amount),
            _ => throw new InvalidTransactionException("Invalid account type!")
        };

        bank.AddAccount(account);
    }

    public static void PerformDeposit(BankingSystem bank)
    {
        Console.WriteLine("\n--- Deposit Money ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        var account = bank.GetAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        Console.Write("Enter Deposit Amount: ");
        double amount = double.Parse(Console.ReadLine() ?? "0");

        account.Deposit(amount);
    }

    public static void PerformWithdraw(BankingSystem bank)
    {
        Console.WriteLine("\n--- Withdraw Money ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        var account = bank.GetAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        Console.Write("Enter Withdrawal Amount: ");
        double amount = double.Parse(Console.ReadLine() ?? "0");

        account.Withdraw(amount);
    }

    public static void DisplayBalance(BankingSystem bank)
    {
        Console.WriteLine("\n--- Check Balance ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        var account = bank.GetAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        Console.WriteLine($"\nAccount Number: {account.AccountNumber}");
        Console.WriteLine($"Customer Name: {account.CustomerName}");
        Console.WriteLine($"Account Type: {account.AccountType}");
        Console.WriteLine($"Current Balance: ${account.Balance:F2}\n");
    }

    public static void ApplyInterest(BankingSystem bank)
    {
        Console.WriteLine("\n--- Apply Interest ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        var account = bank.GetAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        double interest = account.CalculateInterest();
        Console.WriteLine($"Interest applied: ${interest:F2}. New Balance: ${account.Balance:F2}\n");
    }

    public static void TransferMoney(BankingSystem bank)
    {
        Console.WriteLine("\n--- Transfer Money ---");
        Console.Write("Enter From Account Number: ");
        int fromAccount = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter To Account Number: ");
        int toAccount = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Transfer Amount: ");
        double amount = double.Parse(Console.ReadLine() ?? "0");

        bank.TransferMoney(fromAccount, toAccount, amount);
    }

    public static void ViewTransactionHistory(BankingSystem bank)
    {
        Console.WriteLine("\n--- View Transaction History ---");
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine() ?? "0");

        var account = bank.GetAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        account.DisplayTransactionHistory();
    }
}
