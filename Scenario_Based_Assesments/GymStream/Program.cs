using System;
using GymStream.Entities;
using GymStream.Services;
using GymStream.Exceptions;

namespace GymStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var membership = new Membership();
            var service = new MembershipService();

            try
            {
                Console.WriteLine("--- GymStream Enrollment Portal ---");

                Console.Write("Enter membership tier (Basic/Premium/Elite): ");
                membership.Tier = Console.ReadLine()!;

                Console.Write("Enter duration in months: ");
                membership.DurationInMonths = int.Parse(Console.ReadLine()!);

                Console.Write("Enter base price per month: ");
                membership.BasePricePerMonth = double.Parse(Console.ReadLine()!);

                // Validate first
                if (service.ValidateEnrollment(membership))
                {
                    Console.WriteLine("\nEnrollment Successful!");

                    double finalBill = service.CalculateTotalBill(membership);

                    Console.WriteLine($"Total amount after discount: {finalBill:F2}");
                }
            }
            catch (InvalidTierException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
