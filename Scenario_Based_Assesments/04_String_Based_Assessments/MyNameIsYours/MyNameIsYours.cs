using System;
using System.Text.RegularExpressions;

namespace MyNameIsYours
{
    public class MyNameIsYours
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the man name");
            string man = Console.ReadLine();

            Console.WriteLine("Enter the woman name");
            string woman = Console.ReadLine();

            Console.WriteLine(CheckMatch(man, woman));
        }

        public static string CheckMatch(string man, string woman)
        {
            bool isManValid = Regex.IsMatch(man, @"^[A-Za-z ]+$");
            bool isWomanValid = Regex.IsMatch(woman, @"^[A-Za-z ]+$");

            // Validation cases
            if (!isManValid && !isWomanValid)
                return $"Both {man} and {woman} are invalid names";

            if (!isManValid)
                return $"{man} is an invalid name";

            if (!isWomanValid)
                return $"{woman} is an invalid name";

            // Subsequence check
            if (IsSubsequence(man, woman) || IsSubsequence(woman, man))
                return $"{man} and {woman} are made for each other";

            return $"Both {man} and {woman} are not made for each other";
        }

        // Checks whether first string is subsequence of second
        public static bool IsSubsequence(string a, string b)
        {
            int i = 0, j = 0;

            while (i < a.Length && j < b.Length)
            {
                if (a[i] == b[j])
                    i++;
                j++;
            }
            return i == a.Length;
        }
    }
}
