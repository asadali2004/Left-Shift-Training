using NUnit.Framework;

namespace Q4_TestCase_Bank_Account
{
	public class Program
	{
		public decimal Balance { get; private set; }

		public Program(decimal initialBalance)
		{
			Balance = initialBalance;
		}

		public void Deposit(decimal amount)
		{
			if (amount < 0)
			{
				throw new Exception("Deposit amount cannot be negative");
			}

			Balance += amount;
		}

		public void Withdraw(decimal amount)
		{
			if (amount > Balance)
			{
				throw new Exception("Insufficient funds.");
			}

			Balance -= amount;
		}
	}

	[TestFixture]
	public class UnitTest
	{
		[Test]
		public void Test_Deposit_ValidAmount()
		{
			var account = new Program(100m);
			account.Deposit(50m);
			Assert.That(account.Balance, Is.EqualTo(150m));
		}

		[Test]
		public void Test_Deposit_NegativeAmount()
		{
			var account = new Program(100m);
			Assert.That(() => account.Deposit(-10m), Throws.Exception.With.Message.EqualTo("Deposit amount cannot be negative"));
		}

		[Test]
		public void Test_Withdraw_ValidAmount()
		{
			var account = new Program(200m);
			account.Withdraw(75m);
			Assert.That(account.Balance, Is.EqualTo(125m));
		}

		[Test]
		public void Test_Withdraw_InsufficientFunds()
		{
			var account = new Program(30m);
			Assert.That(() => account.Withdraw(100m), Throws.Exception.With.Message.EqualTo("Insufficient funds."));
		}
	}
}
