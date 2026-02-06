using GymStream.Entities;

namespace GymStream.Interfaces
{
    public interface IMembershipService
    {
        bool ValidateEnrollment(Membership membership);
        double CalculateTotalBill(Membership membership);
    }
}
