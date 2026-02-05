using System;
using System.Collections.Generic;
using System.Linq;

namespace _13_Bank_Account_Management
{
    public class BankManager
    {
        // Storage
        public Dictionary<string, Account> Accounts = new Dictionary<string, Account>();

        private int nextAccountNumber = 1001;
        private int nextTransactionId = 1;

        // Create account
        public void CreateAccount(string holder, string type, double initialDeposit)
        {
            string accountNumber = "AC" + nextAccountNumber++;

            Account account = new Account
            {
                AccountNumber = accountNumber,
                AccountHolder = holder,
                AccountType = type,
                Balance = initialDeposit
            };

            // Add initial deposit transaction
            account.TransactionHistory.Add(new Transaction
            {
                TransactionId = "T" + nextTransactionId++,
                TransactionDate = DateTime.Now,
                Type = "Deposit",
                Amount = initialDeposit,
                Description = "Initial Deposit"
            });

            Accounts.Add(accountNumber, account);
        }

        // Deposit money
        public bool Deposit(string accountNumber, double amount)
        {
            if (!Accounts.ContainsKey(accountNumber) || amount <= 0)
                return false;

            var account = Accounts[accountNumber];
            account.Balance += amount;

            account.TransactionHistory.Add(new Transaction
            {
                TransactionId = "T" + nextTransactionId++,
                TransactionDate = DateTime.Now,
                Type = "Deposit",
                Amount = amount,
                Description = "Amount Deposited"
            });

            return true;
        }

        // Withdraw money
        public bool Withdraw(string accountNumber, double amount)
        {
            if (!Accounts.ContainsKey(accountNumber) || amount <= 0)
                return false;

            var account = Accounts[accountNumber];

            if (account.Balance < amount)
                return false;

            account.Balance -= amount;

            account.TransactionHistory.Add(new Transaction
            {
                TransactionId = "T" + nextTransactionId++,
                TransactionDate = DateTime.Now,
                Type = "Withdrawal",
                Amount = amount,
                Description = "Amount Withdrawn"
            });

            return true;
        }

        // Group accounts by type
        public Dictionary<string, List<Account>> GroupAccountsByType()
        {
            return Accounts.Values
                           .GroupBy(a => a.AccountType)
                           .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Get account statement
        public List<Transaction> GetAccountStatement(string accountNumber,
                                                    DateTime from, DateTime to)
        {
            if (!Accounts.ContainsKey(accountNumber))
                return new List<Transaction>();

            return Accounts[accountNumber]
                    .TransactionHistory
                    .Where(t => t.TransactionDate >= from &&
                                t.TransactionDate <= to)
                    .OrderBy(t => t.TransactionDate)
                    .ToList();
        }
    }
}
