# Question 13: Bank Account Management

## Scenario
A banking system needs to manage accounts, transactions, and customer data.

---

## Requirements

### Class: `Account`
- `string AccountNumber`
- `string AccountHolder`
- `string AccountType` (Savings / Current / Fixed)
- `double Balance`
- `List<Transaction> TransactionHistory`

---

### Class: `Transaction`
- `string TransactionId`
- `DateTime TransactionDate`
- `string Type` (Deposit / Withdrawal / Transfer)
- `double Amount`
- `string Description`

---

### Class: `BankManager`

```csharp
public void CreateAccount(string holder, string type, double initialDeposit);
public bool Deposit(string accountNumber, double amount);
public bool Withdraw(string accountNumber, double amount);
public Dictionary<string, List<Account>> GroupAccountsByType();
public List<Transaction> GetAccountStatement(
    string accountNumber,
    DateTime from,
    DateTime to
);

---
##Use Cases

*Open different types of accounts
*Process deposits and withdrawals
*Group accounts by type
*Generate account statements
*Check account balances