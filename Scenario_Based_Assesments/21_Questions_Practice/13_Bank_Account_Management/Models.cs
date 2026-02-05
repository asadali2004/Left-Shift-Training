using System;
using System.Collections.Generic;

namespace _13_Bank_Account_Management
{
    // Represents a bank account
    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string AccountType { get; set; } // Savings / Current / Fixed
        public double Balance { get; set; }

        // Stores all transactions
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
    }

    // Represents a transaction
    public class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; } // Deposit / Withdrawal / Transfer
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
