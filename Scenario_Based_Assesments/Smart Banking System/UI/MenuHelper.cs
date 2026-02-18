namespace SmartBankingSystem.UI;

public static class MenuHelper
{
    public static void DisplayMenu()
    {
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine("║          SMART BANKING SYSTEM          ║");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine("║ 1. Create Account                      ║");
        Console.WriteLine("║ 2. Deposit Money                       ║");
        Console.WriteLine("║ 3. Withdraw Money                      ║");
        Console.WriteLine("║ 4. Check Balance                       ║");
        Console.WriteLine("║ 5. Apply Interest                      ║");
        Console.WriteLine("║ 6. Transfer Money                      ║");
        Console.WriteLine("║ 7. View Transaction History            ║");
        Console.WriteLine("║ 8. Display All Accounts                ║");
        Console.WriteLine("║ 9. Accounts with Balance > 50,000      ║");
        Console.WriteLine("║ 10. Total Bank Balance                 ║");
        Console.WriteLine("║ 11. Top 3 Highest Balance Accounts     ║");
        Console.WriteLine("║ 12. Group Accounts by Type             ║");
        Console.WriteLine("║ 13. Customers Starting with 'R'        ║");
        Console.WriteLine("║ 14. Exit                               ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }
}
