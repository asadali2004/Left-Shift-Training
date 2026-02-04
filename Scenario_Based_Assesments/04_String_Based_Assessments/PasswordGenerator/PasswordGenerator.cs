using System;
using System.Text.RegularExpressions;

namespace PasswordGenerator
{
    public class PasswordGenerator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the username");
            string username = Console.ReadLine();

            Console.WriteLine(ValidateUsername(username));
        }

        public static string ValidateUsername(string username)
        {
            // Check length
            if (username.Length != 8)
                return $"{username} is an invalid username";

            // Regex validation
            if (!Regex.IsMatch(username, @"^[A-Z]{4}@(10[1-9]|11[0-5])$"))
                return $"{username} is an invalid username";

            string prefix = "TECH_";
            string firstFour = username.Substring(0, 4).ToLower();

            int sum = 0;
            foreach (char ch in firstFour)
                sum += ch;

            string lastTwoDigits = username.Substring(6, 2);

            return $"Password: {prefix}{sum}{lastTwoDigits}";
        }
    }
}
