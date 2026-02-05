using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembership
{
    public class GymManager
    {
        // Store all members and classes
        public Dictionary<int, Member> Members = new Dictionary<int, Member>();
        public List<FitnessClass> Classes = new List<FitnessClass>();
        private int nextMemberId = 1;
        
        // Add member with expiry date
        public void AddMember(string name, string membershipType, int months)
        {
            int memberId = nextMemberId++;
            Members.Add(memberId, new Member
            {
                MemberId = memberId,
                Name = name,
                MembershipType = membershipType,
                JoinDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(months)
            });
        }

        // Add new fitness class
        public void AddClass(string className, string instructor, DateTime schedule, int maxParticipants)
        {
            Classes.Add(new FitnessClass
            {
                ClassName = className,
                Instructor = instructor,
                Schedule = schedule,
                MaxParticipants = maxParticipants,
                RegisteredMembers = new List<string>()
            });
        }

        // Register member for class
        public bool RegisterForClass(int memberId, string className)
        {
            if (Members.ContainsKey(memberId))
            {
                var fitnessClass = Classes.FirstOrDefault(c => c.ClassName == className);
                if (fitnessClass != null && fitnessClass.RegisteredMembers.Count < fitnessClass.MaxParticipants)
                {
                    string memberName = Members[memberId].Name;
                    if (!fitnessClass.RegisteredMembers.Contains(memberName))
                    {
                        fitnessClass.RegisteredMembers.Add(memberName);
                        return true;
                    }
                }
            }
            return false;
        }

        // Group members by membership type
        public Dictionary<string, List<Member>> GroupMembersByMembershipType()
        {
            return Members.Values.GroupBy(m => m.MembershipType).ToDictionary(g => g.Key, g => g.ToList());
        }

        // Get classes scheduled for next 7 days
        public List<FitnessClass> GetUpcomingClasses()
        {
            DateTime now = DateTime.Now;
            DateTime weekLater = now.AddDays(7);
            return Classes.Where(c => c.Schedule >= now && c.Schedule <= weekLater).ToList();
        }
    }
}
