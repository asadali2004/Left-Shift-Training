using System;
using System.Text.RegularExpressions;

namespace WordWand
{
    public class WordWand
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the sentence");
            string sentence = Console.ReadLine();

            string result = WordWandOperation(sentence);
            Console.WriteLine(result);
        }

        public static string WordWandOperation(string sentence)
        {
            // Validate sentence (only alphabets and spaces allowed)
            if (!Regex.IsMatch(sentence, @"^[A-Za-z ]+$"))
            {
                return "Invalid Sentence";
            }

            string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;

            // If word count is even → reverse word order
            if (wordCount % 2 == 0)
            {
                Array.Reverse(words);
            }
            else
            {
                // If word count is odd → reverse each word
                for (int i = 0; i < words.Length; i++)
                {
                    char[] ch = words[i].ToCharArray();
                    Array.Reverse(ch);
                    words[i] = new string(ch);
                }
            }

            string modifiedSentence = string.Join(" ", words);

            return $"Word Count: {wordCount}\n{modifiedSentence}";
        }
    }
}
