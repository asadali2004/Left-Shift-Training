using System;

class InputHandler
{
    static void Main()
    {
        int number = 0;
        bool validInput = false;

        while (!validInput)
        {
            try
            {
                Console.WriteLine("Please enter a valid number:");
                string? input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }

                number = int.Parse(input);
                validInput = true;
                
                Console.WriteLine($"You entered: {number}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a numeric value.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number is too large or too small. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        Console.WriteLine("Program completed successfully.");
    }
}
