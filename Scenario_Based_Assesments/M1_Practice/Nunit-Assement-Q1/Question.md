# Bank Account NUnit Test Cases

Write the test case using **NUnit** for the given scenario.

## BankAccount (Program Class)

The `Program` class represents a bank account with a balance:

- **Property**: `Balance` (decimal) stores the current balance.
- **Constructor**: accepts an initial balance and sets `Balance`.

### Methods

1. **Deposit(decimal amount)**
    - Adds the deposit amount to the balance.
    - If `amount` is negative, throw an exception with message: **"Deposit amount cannot be negative"**

2. **Withdraw(decimal amount)**
    - If `amount` is greater than the balance, throw an exception with message: **"Insufficient funds."**
    - Otherwise, subtract the amount from the balance.

## UnitTest Class Requirements

Create the following test methods:

| Method | Rule |
|---|---|
| `Test_Deposit_ValidAmount()` | Deposit should increase the balance correctly. |
| `Test_Deposit_NegativeAmount()` | Deposit with negative value should throw an exception. |
| `Test_Withdraw_ValidAmount()` | Withdraw should decrease the balance correctly. |
| `Test_Withdraw_InsufficientFunds()` | Withdraw more than balance should throw an exception. |

## Notes

1. Add the required **test attribute** for the `UnitTest` class.
2. Add the required **test attribute** for each test method.
3. Each test method must use **only one Assert**.
4. Assert that the **actual** is equal to the **expected**.


