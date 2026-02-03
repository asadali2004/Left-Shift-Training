using System.Reflection.Metadata;
using System.Text.RegularExpressions;

// Gadget warranty validation system for TechStore
namespace ShopValidator
{
    // Utility class to validate gadget entries
    public class EntryValidator
    {
        // Validates gadget ID format: one uppercase letter followed by 3 digits
        public bool validateGadgetID(string gadgetId)
        {
            if (Regex.IsMatch(gadgetId, @"^[A-Z][0-9]{3}$"))
            {
                return true;
            }

            throw new InvalidGadgetException("Invalid gadget ID");
        }

        // Validates warranty period is between 6 and 36 months
        public bool validateWarrantyPeriod(int period)
        {
            if (period >= 6 && period <= 36)
            {
                return true;
            }

            throw new InvalidGadgetException("Invalid warranty period");
        }
    }

    // Custom exception for invalid gadget validation errors
    public class InvalidGadgetException : Exception
    {
        public InvalidGadgetException(string message) : base(message)
        {

        }
    }
    
    // User interface class for gadget entry validation
    public class UserInterface
    {
        // Main entry point
        public static void Main(string[] args)
        {
            EntryValidator validator = new EntryValidator();

            // Read number of entries
            Console.WriteLine("Enter the number of gadget entries");
            int numOfEntries = int.Parse(Console.ReadLine()!);

            // Process each gadget entry
            for (int i = 1; i <= numOfEntries; i++)
            {
                Console.WriteLine($"Enter gadget {i} details");
                string entry = Console.ReadLine()!;

                try
                {
                    // Split entry into components: gadgetID:gadgetType:warrantyPeriod
                    string[] entryArray = entry.Split(':');

                    // Validate input format
                    if (entryArray.Length != 3)
                        throw new InvalidGadgetException("Invalid gadget ID");

                    // Validate gadget ID and warranty period
                    validator.validateGadgetID(entryArray[0]);
                    validator.validateWarrantyPeriod(int.Parse(entryArray[2]));

                    Console.WriteLine("Warranty accepted, stock updated");
                }
                catch (InvalidGadgetException ex)
                {
                    // Display custom exception message
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    // Handle any other exceptions
                    Console.WriteLine("Invalid gadget ID");
                }
            }
        }
    }
}