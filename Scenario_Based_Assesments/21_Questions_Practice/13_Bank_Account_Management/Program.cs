using System;
using System.Linq;

namespace _13_Bank_Account_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            BankManager manager = new BankManager();

            // ✅ Hardcoded Accounts
            manager.CreateAccount("Asad Ali", "Savings", 5000);
            manager.CreateAccount("Rohit Sharma", "Current", 10000);
            manager.CreateAccount("Anshul", "Savings", 7000);

            // Simulate transactions
            manager.Deposit("AC1001", 1500);
            manager.Withdraw("AC1002", 2000);
            manager.Deposit("AC1003", 1000);

            while (true)
            {
                Console.WriteLine("\n=== Bank Account Management ===");
                Console.WriteLine("1. View All Accounts");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Group Accounts By Type");
                Console.WriteLine("5. Account Statement");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n--- Accounts ---");

                    foreach (var acc in manager.Accounts.Values)
                    {
                        Console.WriteLine($"{acc.AccountNumber} | {acc.AccountHolder} | {acc.AccountType} | Balance: ${acc.Balance:F2}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Account Number: ");
                    string accNo = Console.ReadLine();

                    Console.Write("Amount: ");
                    double amount = double.Parse(Console.ReadLine());

                    if (manager.Deposit(accNo, amount))
                        Console.WriteLine("Deposit successful!");
                    else
                        Console.WriteLine("Deposit failed!");
                }

                else if (choice == "3")
                {
                    Console.Write("Account Number: ");
                    string accNo = Console.ReadLine();

                    Console.Write("Amount: ");
                    double amount = double.Parse(Console.ReadLine());

                    if (manager.Withdraw(accNo, amount))
                        Console.WriteLine("Withdrawal successful!");
                    else
                        Console.WriteLine("Withdrawal failed (Insufficient balance or invalid account).");
                }

                else if (choice == "4")
                {
                    Console.WriteLine("\n--- Accounts By Type ---");

                    var grouped = manager.GroupAccountsByType();

                    foreach (var type in grouped)
                    {
                        Console.WriteLine($"\n{type.Key}:");

                        foreach (var acc in type.Value)
                        {
                            Console.WriteLine($"  {acc.AccountNumber} - {acc.AccountHolder} (${acc.Balance:F2})");
                        }
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Account Number: ");
                    string accNo = Console.ReadLine();

                    Console.Write("From Date (yyyy-mm-dd): ");
                    DateTime from = DateTime.Parse(Console.ReadLine());

                    Console.Write("To Date (yyyy-mm-dd): ");
                    DateTime to = DateTime.Parse(Console.ReadLine());

                    var statement = manager.GetAccountStatement(accNo, from, to);

                    if (!statement.Any())
                    {
                        Console.WriteLine("No transactions found!");
                    }
                    else
                    {
                        Console.WriteLine("\n--- Statement ---");

                        foreach (var t in statement)
                        {
                            Console.WriteLine($"{t.TransactionDate:g} | {t.Type} | ${t.Amount} | {t.Description}");
                        }
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Thank you for using the banking system!");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}
