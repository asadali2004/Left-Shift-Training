namespace SmartBankingSystem.Exceptions;

public class MinimumBalanceException : Exception
{
    public MinimumBalanceException(string message) : base(message) { }
}
