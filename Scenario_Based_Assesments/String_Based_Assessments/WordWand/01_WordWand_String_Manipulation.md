# Coding Activity: Word Wand – A Language Model

## Problem Statement

Once upon a time, there was a language model named **Word Wand** that performs special operations on a sentence based on the number of words.

### Rules & Conditions

1. The sentence should contain **only alphabets and spaces**.
2. If the sentence contains **any other characters**, output:
```
Invalid Sentence
```
3. Count the number of words in the sentence.
4. If the number of words is **even**:
- Reverse the **order of words**.
5. If the number of words is **odd**:
- Reverse the **letters of each word**.
6. Display the modified sentence.

---

## Solution (C# Implementation)

```csharp
using System;
using System.Text.RegularExpressions;

namespace WordWand
{
 public class WordWand
 {
     public static void Main(string[] args)
     {
         // Read sentence from the user
         Console.WriteLine("Enter your sentence to check Word Wand's abilities:");
         string sentence = Console.ReadLine();

         // Perform Word Wand operation and display result
         Console.WriteLine(wordWandOperation(sentence));
     }

     public static string wordWandOperation(string words)
     {
         // Validate sentence: only alphabets and spaces allowed
         bool isValid = Regex.IsMatch(words, @"^[A-Za-z ]+$");

         if (!isValid)
         {
             return "Invalid Sentence";
         }

         // Split sentence into words
         string[] wordArray = words.Split(' ');
         int wordCount = wordArray.Length;

         // If word count is even → reverse word positions
         if (wordCount % 2 == 0)
         {
             int left = 0;
             int right = wordCount - 1;

             // Manual array reverse using two pointers
             while (left < right)
             {
                 string temp = wordArray[left];
                 wordArray[left] = wordArray[right];
                 wordArray[right] = temp;

                 left++;
                 right--;
             }
         }
         else
         {
             // If word count is odd → reverse each word
             for (int i = 0; i < wordCount; i++)
             {
                 char[] chars = wordArray[i].ToCharArray();
                 Array.Reverse(chars);
                 wordArray[i] = new string(chars);
             }
         }

         // Join words back into a sentence
         return string.Join(" ", wordArray);
     }
 }
}
```

---

## Example

### Input

```
The Sun Shine
```

### Output

```
ehT nuS enihS
```

### Explanation

* Word count = 3 (odd)
* Each word is reversed individually

---

## Key Concepts Used

* Regular Expressions for validation
* String manipulation
* Arrays and loops
* Conditional logic
* Object-Oriented Programming principles

