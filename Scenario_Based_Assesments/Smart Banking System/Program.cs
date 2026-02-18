using SmartBankingSystem.Services;
using SmartBankingSystem.UI;
using SmartBankingSystem.Models;

class Program
{
    static void Main()
    {
        BankingSystem bank = new BankingSystem();
        
        // Initialize sample data for testing
        InitializeSampleData(bank);
        
        bool running = true;

        while (running)
        {
            MenuHelper.DisplayMenu();
            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AccountOperations.CreateAccount(bank);
                        break;
                    case "2":
                        AccountOperations.PerformDeposit(bank);
                        break;
                    case "3":
                        AccountOperations.PerformWithdraw(bank);
                        break;
                    case "4":
                        AccountOperations.DisplayBalance(bank);
                        break;
                    case "5":
                        AccountOperations.ApplyInterest(bank);
                        break;
                    case "6":
                        AccountOperations.TransferMoney(bank);
                        break;
                    case "7":
                        AccountOperations.ViewTransactionHistory(bank);
                        break;
                    case "8":
                        bank.DisplayAllAccounts();
                        break;
                    case "9":
                        bank.DisplayHighBalanceAccounts();
                        break;
                    case "10":
                        bank.DisplayTotalBankBalance();
                        break;
                    case "11":
                        bank.DisplayTop3HighestBalanceAccounts();
                        break;
                    case "12":
                        bank.DisplayAccountsByType();
                        break;
                    case "13":
                        bank.DisplayCustomersStartingWithR();
                        break;
                    case "14":
                        running = false;
                        Console.WriteLine("Thank you for using Smart Banking System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }
    }

    static void InitializeSampleData(BankingSystem bank)
    {
        Console.WriteLine("Initializing sample data for testing...\n");

        try
        {
            // Create Savings Accounts
            var savings1 = new SavingsAccount(1001, "Rahul Kumar", 75000);
            savings1.Deposit(10000);
            savings1.Withdraw(5000);
            bank.AddAccount(savings1);

            var savings2 = new SavingsAccount(1002, "Priya Sharma", 120000);
            savings2.Deposit(30000);
            bank.AddAccount(savings2);

            var savings3 = new SavingsAccount(1003, "Amit Patel", 25000);
            savings3.Deposit(5000);
            savings3.Withdraw(8000);
            bank.AddAccount(savings3);

            var savings4 = new SavingsAccount(1004, "Rajesh Singh", 55000);
            bank.AddAccount(savings4);

            // Create Current Accounts
            var current1 = new CurrentAccount(2001, "Tech Solutions Pvt Ltd", 200000);
            current1.Deposit(50000);
            current1.Withdraw(80000);
            bank.AddAccount(current1);

            var current2 = new CurrentAccount(2002, "Global Enterprises", 45000);
            current2.Deposit(15000);
            bank.AddAccount(current2);

            var current3 = new CurrentAccount(2003, "Innovation Corp", 30000);
            bank.AddAccount(current3);

            var current4 = new CurrentAccount(2004, "Rohan Traders", 85000);
            current4.Deposit(20000);
            bank.AddAccount(current4);

            // Create Loan Accounts
            var loan1 = new LoanAccount(3001, "Sneha Reddy", 500000);
            loan1.Withdraw(50000); // Partial payment
            bank.AddAccount(loan1);

            var loan2 = new LoanAccount(3002, "Vikram Malhotra", 300000);
            loan2.Withdraw(30000);
            bank.AddAccount(loan2);

            var loan3 = new LoanAccount(3003, "Kavya Iyer", 150000);
            bank.AddAccount(loan3);

            Console.WriteLine(" Sample data initialized successfully!");
            Console.WriteLine(" Created 11 accounts: 4 Savings, 4 Current, 3 Loan\n");
            Console.WriteLine(" Sample Account Numbers to Test:");
            Console.WriteLine(" Savings: 1001, 1002, 1003, 1004");
            Console.WriteLine(" Current: 2001, 2002, 2003, 2004");
            Console.WriteLine(" Loan: 3001, 3002, 3003\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing sample data: {ex.Message}\n");
        }
    }
}