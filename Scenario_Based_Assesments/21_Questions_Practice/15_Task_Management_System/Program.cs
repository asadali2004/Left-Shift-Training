using System;
using System.Linq;

namespace _15_Task_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager manager = new TaskManager();

            // ✅ Hardcoded Projects
            manager.CreateProject("Banking App", "Asad",
                DateTime.Today.AddDays(-10),
                DateTime.Today.AddMonths(2));

            manager.CreateProject("Flight System", "Rohit",
                DateTime.Today,
                DateTime.Today.AddMonths(3));

            // ✅ Hardcoded Tasks
            manager.AddTask(1, "Design Database", "Create DB schema",
                "High", DateTime.Today.AddDays(-1), "Anshul");

            manager.AddTask(1, "API Development", "Build REST APIs",
                "Medium", DateTime.Today.AddDays(5), "Yash");

            manager.AddTask(2, "UI Design", "Create wireframes",
                "Low", DateTime.Today.AddDays(7), "Anshul");

            while (true)
            {
                Console.WriteLine("\n=== Task Management System ===");
                Console.WriteLine("1. View Projects");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Group Tasks By Priority");
                Console.WriteLine("4. View Overdue Tasks");
                Console.WriteLine("5. View Tasks By Assignee");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n--- Projects ---");

                    foreach (var p in manager.Projects.Values)
                    {
                        Console.WriteLine($"{p.ProjectId} | {p.ProjectName} | Manager: {p.ProjectManager} | Tasks: {p.Tasks.Count}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("Project ID: ");
                    int pid = int.Parse(Console.ReadLine());

                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Description: ");
                    string desc = Console.ReadLine();

                    Console.Write("Priority (High/Medium/Low): ");
                    string priority = Console.ReadLine();

                    Console.Write("Due Date (yyyy-mm-dd): ");
                    DateTime due = DateTime.Parse(Console.ReadLine());

                    Console.Write("Assign To: ");
                    string assignee = Console.ReadLine();

                    manager.AddTask(pid, title, desc, priority, due, assignee);

                    Console.WriteLine("Task added successfully!");
                }

                else if (choice == "3")
                {
                    var grouped = manager.GroupTasksByPriority();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nPriority: {g.Key}");

                        foreach (var t in g.Value)
                        {
                            Console.WriteLine($"  {t.TaskId} - {t.Title} ({t.Status})");
                        }
                    }
                }

                else if (choice == "4")
                {
                    var overdue = manager.GetOverdueTasks();

                    if (!overdue.Any())
                    {
                        Console.WriteLine("No overdue tasks!");
                    }
                    else
                    {
                        Console.WriteLine("\n--- Overdue Tasks ---");

                        foreach (var t in overdue)
                        {
                            Console.WriteLine($"{t.TaskId} | {t.Title} | Assigned to: {t.AssignedTo} | Due: {t.DueDate:d}");
                        }
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Assignee Name: ");
                    string name = Console.ReadLine();

                    var tasks = manager.GetTasksByAssignee(name);

                    if (!tasks.Any())
                    {
                        Console.WriteLine("No tasks found!");
                    }
                    else
                    {
                        foreach (var t in tasks)
                        {
                            Console.WriteLine($"{t.TaskId} | {t.Title} | Priority: {t.Priority} | Status: {t.Status}");
                        }
                    }
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Exiting Task Manager!");
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
