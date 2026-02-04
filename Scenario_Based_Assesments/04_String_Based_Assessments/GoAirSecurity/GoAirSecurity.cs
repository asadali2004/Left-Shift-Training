// GoAir Security Entry Validation System
// Validates employee IDs and entry duration for security personnel

using System;
using System.Text.RegularExpressions;

namespace GoAirSecurity
{
    // Utility class for validating employee entry details
    public class EntryUtility
    {
        // Validates employee ID format (GOAIR/####)
        public bool validateEmployeeId(string employeeId)
        {
            // Employee ID must be exactly 10 characters
            if (employeeId.Length == 10)
            {
                // Regex to check format GOAIR/ followed by 4 digits
                if (Regex.IsMatch(employeeId, @"^GOAIR/[0-9]{4}$"))
                {
                    return true;
                }
            }

            // Throw exception if validation fails
            throw new InvalidEntryException("Invalid employee ID");
        }

        //Validates duration between 1 and 5 hours

        public bool validateDuration(int duration)
        {
            // Duration must be between 1 and 5 inclusive
            if (duration >= 1 && duration <= 5)
            {
                return true;
            }

            // Throw exception if validation fails
            throw new InvalidEntryException("Invalid duration");
        }
    }

    /// <summary>
    /// Custom exception class for entry validation errors
    /// </summary>
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// User interface class to accept input and display output
    /// </summary>
    public class UserInterface
    {
         public static void Main(string[] args)
        {
            EntryUtility util = new EntryUtility();

            Console.WriteLine("Enter the number of entries");
            int n = int.Parse(Console.ReadLine()!);

            string[] entries = new string[n];

            // INPUT PHASE ONLY
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter entry {i + 1} details");
                entries[i] = Console.ReadLine()!;
            }

            // OUTPUT PHASE ONLY
            foreach (string entry in entries)
            {
                try
                {
                    string[] parts = entry.Split(':');

                    util.validateEmployeeId(parts[0]);
                    util.validateDuration(int.Parse(parts[2]));

                    Console.WriteLine("Valid entry details");
                }
                catch
                {
                    Console.WriteLine("Invalid entry details");
                }
            }
        }
    }
}
