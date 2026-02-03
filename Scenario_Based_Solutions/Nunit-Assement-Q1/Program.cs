using NUnit.Framework;

namespace Q4_TestCase_Bank_Account
{
	/// <summary>
	/// Bank account class with deposit and withdraw functionality
	/// </summary>
	public class Program
	{
		public decimal Balance { get; private set; }

		/// <summary>
		/// Initializes a new bank account with an initial balance
		/// </summary>
		/// <param name="initialBalance">Starting balance for the account</param>
		public Program(decimal initialBalance)
		{
			Balance = initialBalance;
		}

		/// <summary>
		/// Deposits money into the account
		/// </summary>
		/// <param name="amount">Amount to deposit</param>
		/// <exception cref="Exception">Thrown when deposit amount is negative</exception>
		public void Deposit(decimal amount)
		{
			// Validate deposit amount is not negative
			if (amount < 0)
			{
				throw new Exception("Deposit amount cannot be negative");
			}

			// Add amount to balance
			Balance += amount;
		}

		/// <summary>
		/// Withdraws money from the account
		/// </summary>
		/// <param name="amount">Amount to withdraw</param>
		/// <exception cref="Exception">Thrown when insufficient funds</exception>
		public void Withdraw(decimal amount)
		{
			// Check if sufficient funds available
			if (amount > Balance)
			{
				throw new Exception("Insufficient funds.");
			}

			// Deduct amount from balance
			Balance -= amount;
		}
	}

	/// <summary>
	/// Unit tests for BankAccount deposit and withdraw operations
	/// </summary>
	[TestFixture]
	public class UnitTest
	{
		/// <summary>
		/// Test that depositing a valid amount increases the balance correctly
		/// </summary>
		[Test]
		public void Test_Deposit_ValidAmount()
		{
			// Arrange: Create account with 100 balance
			var account = new Program(100m);
			
			// Act: Deposit 50
			account.Deposit(50m);
			
			// Assert: Balance should be 150
			Assert.That(account.Balance, Is.EqualTo(150m));
		}

		/// <summary>
		/// Test that depositing a negative amount throws an exception
		/// </summary>
		[Test]
		public void Test_Deposit_NegativeAmount()
		{
			// Arrange: Create account with 100 balance
			var account = new Program(100m);
			
			// Act & Assert: Depositing negative amount should throw exception
			Assert.That(() => account.Deposit(-10m), Throws.Exception.With.Message.EqualTo("Deposit amount cannot be negative"));
		}

		/// <summary>
		/// Test that withdrawing a valid amount decreases the balance correctly
		/// </summary>
		[Test]
		public void Test_Withdraw_ValidAmount()
		{
			// Arrange: Create account with 200 balance
			var account = new Program(200m);
			
			// Act: Withdraw 75
			account.Withdraw(75m);
			
			// Assert: Balance should be 125
			Assert.That(account.Balance, Is.EqualTo(125m));
		}

		/// <summary>
		/// Test that withdrawing more than the balance throws an exception
		/// </summary>
		[Test]
		public void Test_Withdraw_InsufficientFunds()
		{
			// Arrange: Create account with 30 balance
			var account = new Program(30m);
			
			// Act & Assert: Withdrawing 100 should throw exception
			Assert.That(() => account.Withdraw(100m), Throws.Exception.With.Message.EqualTo("Insufficient funds."));
		}
	}
}
