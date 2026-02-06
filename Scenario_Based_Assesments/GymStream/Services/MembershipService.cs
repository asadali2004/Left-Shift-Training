using System;
using GymStream.Entities;
using GymStream.Exceptions;
using GymStream.Interfaces;

namespace GymStream.Services
{
    public class MembershipService : IMembershipService
    {
        public bool ValidateEnrollment(Membership membership)
        {
            // Validate Tier using Enum
            if (membership.Tier != "Basic" && membership.Tier != "Premium" && membership.Tier != "Elite")
            {
                throw new InvalidTierException(
                    "Tier not recognized. Please choose an available membership plan.");
            }

            // Validate Duration
            if (membership.DurationInMonths <= 0)
            {
                throw new Exception("Duration must be at least one month.");
            }

            return true;
        }

        public double CalculateTotalBill(Membership membership)
        {
            double total = membership.DurationInMonths * membership.BasePricePerMonth;

            double discount = membership.Tier!.ToLower() switch
            {
                "basic" => 0.02,
                "premium" => 0.07,
                "elite" => 0.12,
                _ => 0
            };

            double discountAmount = total * discount;

            return total - discountAmount;
        }
    }
}
