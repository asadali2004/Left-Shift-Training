using SmartBankingSystem.Models;

namespace SmartBankingSystem.Services;

public class BankingSystem
{
    private List<BankAccount> accounts = new List<BankAccount>();

    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
        Console.WriteLine($"✓ Account created successfully!\n");
    }

    public BankAccount? GetAccount(int accountNumber)
    {
        return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    // LINQ: Get accounts with balance > 50,000
    public void DisplayHighBalanceAccounts()
    {
        var highBalanceAccounts = accounts.Where(a => a.Balance > 50000).ToList();
        if (highBalanceAccounts.Count == 0)
        {
            Console.WriteLine("No accounts with balance > 50,000 found.\n");
            return;
        }
        Console.WriteLine("\n--- Accounts with Balance > $50,000 ---");
        foreach (var acc in highBalanceAccounts)
        {
            Console.WriteLine($"Account: {acc.AccountNumber} | Name: {acc.CustomerName} | Type: {acc.AccountType} | Balance: ${acc.Balance:F2}");
        }
        Console.WriteLine();
    }

    // LINQ: Get total bank balance
    public void DisplayTotalBankBalance()
    {
        double totalBalance = accounts.Sum(a => a.Balance);
        Console.WriteLine($"\n--- Total Bank Balance: ${totalBalance:F2} ---\n");
    }

    // LINQ: Top 3 highest balance accounts
    public void DisplayTop3HighestBalanceAccounts()
    {
        var top3 = accounts.OrderByDescending(a => a.Balance).Take(3).ToList();
        if (top3.Count == 0)
        {
            Console.WriteLine("No accounts found.\n");
            return;
        }
        Console.WriteLine("\n--- Top 3 Highest Balance Accounts ---");
        int rank = 1;
        foreach (var acc in top3)
        {
            Console.WriteLine($"{rank}. Account: {acc.AccountNumber} | Name: {acc.CustomerName} | Type: {acc.AccountType} | Balance: ${acc.Balance:F2}");
            rank++;
        }
        Console.WriteLine();
    }

    // LINQ: Group accounts by type
    public void DisplayAccountsByType()
    {
        var groupedAccounts = accounts.GroupBy(a => a.AccountType).ToList();
        Console.WriteLine("\n--- Accounts Grouped by Type ---");
        foreach (var group in groupedAccounts)
        {
            Console.WriteLine($"\n{group.Key} ({group.Count()}):");
            foreach (var acc in group)
            {
                Console.WriteLine($"  Account: {acc.AccountNumber} | Name: {acc.CustomerName} | Balance: ${acc.Balance:F2}");
            }
        }
        Console.WriteLine();
    }

    // LINQ: Find customers whose name starts with "R"
    public void DisplayCustomersStartingWithR()
    {
        var customersWithR = accounts.Where(a => a.CustomerName.StartsWith("R", StringComparison.OrdinalIgnoreCase)).ToList();
        if (customersWithR.Count == 0)
        {
            Console.WriteLine("\nNo customers found with names starting with 'R'.\n");
            return;
        }
        Console.WriteLine("\n--- Customers with Names Starting with 'R' ---");
        foreach (var acc in customersWithR)
        {
            Console.WriteLine($"Account: {acc.AccountNumber} | Name: {acc.CustomerName} | Type: {acc.AccountType} | Balance: ${acc.Balance:F2}");
        }
        Console.WriteLine();
    }

    // Transfer money between accounts
    public void TransferMoney(int fromAccountNumber, int toAccountNumber, double amount)
    {
        var fromAccount = GetAccount(fromAccountNumber);
        var toAccount = GetAccount(toAccountNumber);

        if (fromAccount == null || toAccount == null)
        {
            Console.WriteLine("❌ One or both accounts not found!");
            return;
        }

        try
        {
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
            Console.WriteLine($"✓ Transfer successful! ${amount:F2} transferred from Account {fromAccountNumber} to Account {toAccountNumber}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Transfer failed: {ex.Message}\n");
        }
    }

    // Display all accounts
    public void DisplayAllAccounts()
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts found.\n");
            return;
        }
        Console.WriteLine("\n--- All Accounts ---");
        foreach (var acc in accounts)
        {
            Console.WriteLine($"Account: {acc.AccountNumber} | Name: {acc.CustomerName} | Type: {acc.AccountType} | Balance: ${acc.Balance:F2}");
        }
        Console.WriteLine();
    }
}
