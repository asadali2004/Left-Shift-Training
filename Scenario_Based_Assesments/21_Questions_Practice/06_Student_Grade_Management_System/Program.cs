using System;
using System.Linq;

namespace StudentGradeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            SchoolManager manager = new SchoolManager();
            
            // Add some hardcoded test data
            manager.AddStudent("Alice Johnson", "9th");
            manager.AddGrade(1, "Math", 92);
            manager.AddGrade(1, "Science", 88);
            manager.AddGrade(1, "English", 85);
            
            manager.AddStudent("Bob Smith", "10th");
            manager.AddGrade(2, "Math", 78);
            manager.AddGrade(2, "Science", 82);
            manager.AddGrade(2, "English", 90);
            
            manager.AddStudent("Charlie Brown", "9th");
            manager.AddGrade(3, "Math", 95);
            manager.AddGrade(3, "Science", 93);
            manager.AddGrade(3, "English", 89);
            
            manager.AddStudent("Diana Prince", "11th");
            manager.AddGrade(4, "Math", 88);
            manager.AddGrade(4, "Science", 91);
            manager.AddGrade(4, "English", 87);
            
            manager.AddStudent("Ethan Hunt", "10th");
            manager.AddGrade(5, "Math", 85);
            manager.AddGrade(5, "Science", 86);
            manager.AddGrade(5, "English", 92);
            
            manager.AddStudent("Fiona Green", "12th");
            manager.AddGrade(6, "Math", 96);
            manager.AddGrade(6, "Science", 94);
            manager.AddGrade(6, "English", 95);
            
            while (true)
            {
                Console.WriteLine("\n=== Student Grade Management System ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Grade");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. View Students by Grade Level");
                Console.WriteLine("5. Calculate Student Average");
                Console.WriteLine("6. View Subject Averages");
                Console.WriteLine("7. View Top Performers");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice (1-8): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add Student
                    Console.WriteLine("\n--- Add Student ---");
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Grade Level (9th/10th/11th/12th): ");
                    string gradeLevel = Console.ReadLine();
                    
                    manager.AddStudent(name, gradeLevel);
                    Console.WriteLine("Student added successfully!");
                }
                else if (choice == "2")
                {
                    // Add Grade
                    Console.WriteLine("\n--- Add Grade ---");
                    Console.Write("Enter Student ID: ");
                    int studentId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Subject: ");
                    string subject = Console.ReadLine();
                    Console.Write("Enter Grade (0-100): ");
                    double grade = double.Parse(Console.ReadLine());
                    
                    manager.AddGrade(studentId, subject, grade);
                    Console.WriteLine("Grade added successfully!");
                }
                else if (choice == "3")
                {
                    // View All Students
                    Console.WriteLine("\n--- All Students ---");
                    if (manager.Students.Count == 0)
                    {
                        Console.WriteLine("No students found!");
                    }
                    else
                    {
                        foreach (var student in manager.Students.Values)
                        {
                            Console.WriteLine($"\nID: {student.StudentId} - {student.Name} ({student.GradeLevel})");
                            if (student.Subjects.Count > 0)
                            {
                                Console.WriteLine("  Subjects:");
                                foreach (var subject in student.Subjects)
                                {
                                    Console.WriteLine($"    {subject.Key}: {subject.Value}");
                                }
                                double avg = manager.CalculateStudentAverage(student.StudentId);
                                Console.WriteLine($"  Average: {avg:F2}");
                            }
                            else
                            {
                                Console.WriteLine("  No grades yet");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // View Students by Grade Level
                    Console.WriteLine("\n--- Students by Grade Level ---");
                    if (manager.Students.Count == 0)
                    {
                        Console.WriteLine("No students found!");
                    }
                    else
                    {
                        var grouped = manager.GroupStudentsByGradeLevel();
                        foreach (var level in grouped)
                        {
                            Console.WriteLine($"\n{level.Key} Grade ({level.Value.Count} students):");
                            foreach (var student in level.Value)
                            {
                                double avg = manager.CalculateStudentAverage(student.StudentId);
                                Console.WriteLine($"  {student.StudentId}. {student.Name} - Avg: {avg:F2}");
                            }
                        }
                    }
                }
                else if (choice == "5")
                {
                    // Calculate Student Average
                    Console.WriteLine("\n--- Calculate Student Average ---");
                    Console.Write("Enter Student ID: ");
                    int studentId = int.Parse(Console.ReadLine());
                    
                    if (manager.Students.ContainsKey(studentId))
                    {
                        double avg = manager.CalculateStudentAverage(studentId);
                        if (avg > 0)
                        {
                            Console.WriteLine($"Average for {manager.Students[studentId].Name}: {avg:F2}");
                        }
                        else
                        {
                            Console.WriteLine("No grades available!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student not found!");
                    }
                }
                else if (choice == "6")
                {
                    // View Subject Averages
                    Console.WriteLine("\n--- Subject Averages ---");
                    var subjectAvgs = manager.CalculateSubjectAverages();
                    if (subjectAvgs.Count > 0)
                    {
                        Console.WriteLine("{0,-20} {1,-10}", "Subject", "Average");
                        Console.WriteLine(new string('-', 35));
                        foreach (var subject in subjectAvgs)
                        {
                            Console.WriteLine("{0,-20} {1,-10:F2}", subject.Key, subject.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No grades available!");
                    }
                }
                else if (choice == "7")
                {
                    // View Top Performers
                    Console.WriteLine("\n--- Top Performers ---");
                    Console.Write("Enter number of top students: ");
                    int count = int.Parse(Console.ReadLine());
                    
                    var topStudents = manager.GetTopPerformers(count);
                    if (topStudents.Count > 0)
                    {
                        Console.WriteLine($"\nTop {topStudents.Count} Students:");
                        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10}", "ID", "Name", "Grade", "Average");
                        Console.WriteLine(new string('-', 50));
                        foreach (var student in topStudents)
                        {
                            double avg = manager.CalculateStudentAverage(student.StudentId);
                            Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10:F2}", 
                                student.StudentId, student.Name, student.GradeLevel, avg);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No students with grades found!");
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
