using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembership
{
    public class GymManager
    {
        // TODO: Add collections to store members and classes
        
        public void AddMember(string name, string membershipType, int months)
        {
            // TODO: Creates membership with expiry date
        }

        public void AddClass(string className, string instructor, DateTime schedule, int maxParticipants)
        {
            // TODO: Adds new fitness class
        }

        public bool RegisterForClass(int memberId, string className)
        {
            // TODO: Registers member if class has space
            return false;
        }

        public Dictionary<string, List<Member>> GroupMembersByMembershipType()
        {
            // TODO: Groups members by their plan
            return new Dictionary<string, List<Member>>();
        }

        public List<FitnessClass> GetUpcomingClasses()
        {
            // TODO: Returns classes scheduled for next 7 days
            return new List<FitnessClass>();
        }
    }
}
