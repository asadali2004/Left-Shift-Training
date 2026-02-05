using System;
using System.Linq;

namespace GymMembership
{
    class Program
    {
        static void Main(string[] args)
        {
            GymManager manager = new GymManager();
            
            // Add some hardcoded test data
            manager.AddMember("John Doe", "Basic", 6);
            manager.AddMember("Sarah Smith", "Premium", 12);
            manager.AddMember("Mike Johnson", "Platinum", 12);
            manager.AddMember("Emily Brown", "Basic", 3);
            manager.AddMember("David Lee", "Premium", 6);
            manager.AddMember("Lisa Wilson", "Platinum", 12);
            
            // Add fitness classes
            manager.AddClass("Yoga", "Jennifer", DateTime.Now.AddDays(1).AddHours(9), 20);
            manager.AddClass("Spinning", "Mark", DateTime.Now.AddDays(2).AddHours(18), 15);
            manager.AddClass("CrossFit", "Tom", DateTime.Now.AddDays(3).AddHours(17), 12);
            manager.AddClass("Zumba", "Maria", DateTime.Now.AddDays(4).AddHours(19), 25);
            manager.AddClass("Pilates", "Anna", DateTime.Now.AddDays(5).AddHours(10), 18);
            manager.AddClass("Boxing", "Steve", DateTime.Now.AddDays(6).AddHours(20), 10);
            
            // Pre-register some members
            manager.RegisterForClass(1, "Yoga");
            manager.RegisterForClass(2, "Yoga");
            manager.RegisterForClass(3, "Spinning");
            manager.RegisterForClass(4, "CrossFit");
            
            while (true)
            {
                Console.WriteLine("\n=== Gym Membership Management ===");
                Console.WriteLine("1. Add Member");
                Console.WriteLine("2. View All Members");
                Console.WriteLine("3. View Members by Membership Type");
                Console.WriteLine("4. Add Fitness Class");
                Console.WriteLine("5. View All Classes");
                Console.WriteLine("6. View Upcoming Classes");
                Console.WriteLine("7. Register for Class");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice (1-8): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Member
                    Console.WriteLine("\n--- Add Member ---");
                    Console.Write("Enter Member Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Membership Type (Basic/Premium/Platinum): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter Duration (months): ");
                    int months = int.Parse(Console.ReadLine());
                    
                    manager.AddMember(name, type, months);
                    Console.WriteLine("Member added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Members
                    Console.WriteLine("\n--- All Members ---");
                    if (manager.Members.Count == 0)
                    {
                        Console.WriteLine("No members found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12} {4,-12}", "ID", "Name", "Type", "Join Date", "Expiry");
                        Console.WriteLine(new string('-', 70));
                        foreach (var member in manager.Members.Values)
                        {
                            Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12:d} {4,-12:d}", 
                                member.MemberId, member.Name, member.MembershipType, member.JoinDate, member.ExpiryDate);
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Members by Membership Type
                    Console.WriteLine("\n--- Members by Membership Type ---");
                    if (manager.Members.Count == 0)
                    {
                        Console.WriteLine("No members found!");
                    }
                    else
                    {
                        var grouped = manager.GroupMembersByMembershipType();
                        foreach (var type in grouped)
                        {
                            Console.WriteLine($"\n{type.Key} Members ({type.Value.Count}):");
                            foreach (var member in type.Value)
                            {
                                Console.WriteLine($"  {member.MemberId}. {member.Name} - Expires: {member.ExpiryDate:d}");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // Add Fitness Class
                    Console.WriteLine("\n--- Add Fitness Class ---");
                    Console.Write("Enter Class Name: ");
                    string className = Console.ReadLine();
                    Console.Write("Enter Instructor Name: ");
                    string instructor = Console.ReadLine();
                    Console.Write("Enter Schedule (days from now): ");
                    int days = int.Parse(Console.ReadLine());
                    Console.Write("Enter Time (hour 0-23): ");
                    int hour = int.Parse(Console.ReadLine());
                    DateTime schedule = DateTime.Now.AddDays(days).Date.AddHours(hour);
                    Console.Write("Enter Max Participants: ");
                    int max = int.Parse(Console.ReadLine());
                    
                    manager.AddClass(className, instructor, schedule, max);
                    Console.WriteLine("Class added successfully!");
                }
                else if (choice == "5")
                {
                    // View All Classes
                    Console.WriteLine("\n--- All Classes ---");
                    if (manager.Classes.Count == 0)
                    {
                        Console.WriteLine("No classes found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-15}", "Class", "Instructor", "Schedule", "Occupancy");
                        Console.WriteLine(new string('-', 70));
                        foreach (var fitnessClass in manager.Classes)
                        {
                            Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-15}", 
                                fitnessClass.ClassName, fitnessClass.Instructor, fitnessClass.Schedule.ToString("g"),
                                $"{fitnessClass.RegisteredMembers.Count}/{fitnessClass.MaxParticipants}");
                        }
                    }
                }
                else if (choice == "6")
                {
                    // View Upcoming Classes
                    Console.WriteLine("\n--- Upcoming Classes (Next 7 Days) ---");
                    var upcoming = manager.GetUpcomingClasses();
                    if (upcoming.Count == 0)
                    {
                        Console.WriteLine("No upcoming classes found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-15}", "Class", "Instructor", "Schedule", "Occupancy");
                        Console.WriteLine(new string('-', 70));
                        foreach (var fitnessClass in upcoming)
                        {
                            Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-15}", 
                                fitnessClass.ClassName, fitnessClass.Instructor, fitnessClass.Schedule.ToString("g"),
                                $"{fitnessClass.RegisteredMembers.Count}/{fitnessClass.MaxParticipants}");
                        }
                    }
                }
                else if (choice == "7")
                {
                    // Register for Class
                    Console.WriteLine("\n--- Register for Class ---");
                    if (manager.Members.Count == 0 || manager.Classes.Count == 0)
                    {
                        Console.WriteLine("No members or classes available!");
                    }
                    else
                    {
                        Console.Write("Enter Member ID: ");
                        int memberId = int.Parse(Console.ReadLine());
                        
                        if (manager.Members.ContainsKey(memberId))
                        {
                            Console.WriteLine("\nAvailable Classes:");
                            for (int i = 0; i < manager.Classes.Count; i++)
                            {
                                var fc = manager.Classes[i];
                                Console.WriteLine($"{i + 1}. {fc.ClassName} - {fc.Instructor} - {fc.Schedule:g} - {fc.RegisteredMembers.Count}/{fc.MaxParticipants}");
                            }
                            
                            Console.Write("\nEnter Class Name: ");
                            string className = Console.ReadLine();
                            
                            if (manager.RegisterForClass(memberId, className))
                            {
                                Console.WriteLine("Registration successful!");
                            }
                            else
                            {
                                Console.WriteLine("Registration failed! Class full or already registered.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Member not found!");
                        }
                    }
                }
                else if (choice == "8")
                {
                    Console.WriteLine("Thank you!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}
