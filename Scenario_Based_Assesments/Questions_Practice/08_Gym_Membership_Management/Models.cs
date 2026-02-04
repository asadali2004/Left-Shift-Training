using System;
using System.Collections.Generic;

namespace GymMembership
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string MembershipType { get; set; }  // Basic/Premium/Platinum
        public DateTime JoinDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class FitnessClass
    {
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public DateTime Schedule { get; set; }
        public int MaxParticipants { get; set; }
        public List<string> RegisteredMembers { get; set; }
    }
}
