using System;
using System.Collections.Generic;
using System.Linq;

namespace _16_E_Learning_Platform
{
    class Program
    {
        static void Main(string[] args)
        {
            LearningManager manager = new LearningManager();

            // ✅ Hardcoded Courses
            manager.AddCourse("C101", "C# Mastery", "Asad",
                8, 4999, new List<string> { "Basics", "OOP", "LINQ", "ASP.NET" });

            manager.AddCourse("C102", "SQL Bootcamp", "Rohit",
                6, 2999, new List<string> { "Queries", "Joins", "Indexes" });

            // ✅ Enroll Students
            manager.EnrollStudent("S1", "C101");
            manager.EnrollStudent("S2", "C101");
            manager.EnrollStudent("S3", "C102");

            // Simulate progress
            manager.UpdateProgress("S1", "C101", "Basics", 85);
            manager.UpdateProgress("S1", "C101", "OOP", 90);
            manager.UpdateProgress("S2", "C101", "Basics", 70);

            while (true)
            {
                Console.WriteLine("\n=== E-Learning Platform ===");
                Console.WriteLine("1. View Courses");
                Console.WriteLine("2. Enroll Student");
                Console.WriteLine("3. Update Progress");
                Console.WriteLine("4. Group Courses By Instructor");
                Console.WriteLine("5. Top Performing Students");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var c in manager.Courses.Values)
                    {
                        Console.WriteLine($"{c.CourseCode} | {c.CourseName} | Instructor: {c.Instructor} | Modules: {c.Modules.Count}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Student ID: ");
                    string sid = Console.ReadLine();

                    Console.Write("Course Code: ");
                    string code = Console.ReadLine();

                    if (manager.EnrollStudent(sid, code))
                        Console.WriteLine("Enrollment successful!");
                    else
                        Console.WriteLine("Enrollment failed!");
                }

                else if (choice == "3")
                {
                    Console.Write("Student ID: ");
                    string sid = Console.ReadLine();

                    Console.Write("Course Code: ");
                    string code = Console.ReadLine();

                    Console.Write("Module: ");
                    string module = Console.ReadLine();

                    Console.Write("Score: ");
                    double score = double.Parse(Console.ReadLine());

                    if (manager.UpdateProgress(sid, code, module, score))
                        Console.WriteLine("Progress updated!");
                    else
                        Console.WriteLine("Update failed!");
                }

                else if (choice == "4")
                {
                    var grouped = manager.GroupCoursesByInstructor();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nInstructor: {g.Key}");

                        foreach (var course in g.Value)
                            Console.WriteLine($"  {course.CourseName}");
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Course Code: ");
                    string code = Console.ReadLine();

                    var top = manager.GetTopPerformingStudents(code, 3);

                    if (!top.Any())
                        Console.WriteLine("No data available!");
                    else
                    {
                        foreach (var e in top)
                        {
                            Console.WriteLine($"{e.StudentId} | Progress: {e.ProgressPercentage:F1}%");
                        }
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Thank you for using Learning Platform!");
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
