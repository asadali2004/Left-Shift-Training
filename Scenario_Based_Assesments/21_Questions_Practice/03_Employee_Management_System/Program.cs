using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem
{
    class Program
    {
        // Store all employees
        public static Dictionary<string, Employee> Employees = new Dictionary<string, Employee>();
        
        static void Main(string[] args)
        {
            HRManager hrManager = new HRManager();
            
            // Add some hardcoded test data
            hrManager.AddEmployee("John Smith", "IT", 75000);
            hrManager.AddEmployee("Sarah Johnson", "HR", 65000);
            hrManager.AddEmployee("Mike Brown", "Sales", 60000);
            hrManager.AddEmployee("Emily Davis", "IT", 80000);
            hrManager.AddEmployee("David Wilson", "Finance", 70000);
            hrManager.AddEmployee("Lisa Anderson", "HR", 62000);
            hrManager.AddEmployee("Tom Martinez", "Sales", 58000);
            hrManager.AddEmployee("Anna Taylor", "IT", 72000);
            hrManager.AddEmployee("James Lee", "Finance", 68000);
            hrManager.AddEmployee("Maria Garcia", "Sales", 55000);
            
            while (true)
            {
                Console.WriteLine("\n=== Employee Management System ===");
                Console.WriteLine("1. Add New Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. View Employees by Department");
                Console.WriteLine("4. Calculate Department Salary");
                Console.WriteLine("5. Find Employees Joined After Date");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    // Add New Employee
                    Console.WriteLine("\n--- Add New Employee ---");
                    Console.Write("Enter Employee Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Department: ");
                    string dept = Console.ReadLine();
                    Console.Write("Enter Salary: ");
                    double salary = double.Parse(Console.ReadLine());
                    
                    hrManager.AddEmployee(name, dept, salary);
                    Console.WriteLine("Employee added successfully!");
                }
                else if (choice == "2")
                {
                    // View All Employees
                    Console.WriteLine("\n--- All Employees ---");
                    if (Employees.Count == 0)
                    {
                        Console.WriteLine("No employees found!");
                    }
                    else
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-12} {4,-12}", "Emp ID", "Name", "Department", "Salary", "Join Date");
                        Console.WriteLine(new string('-', 70));
                        foreach (var emp in Employees.Values)
                        {
                            Console.WriteLine("{0,-10} {1,-20} {2,-15} ${3,-11:F2} {4,-12:d}", emp.EmployeeId, emp.Name, emp.Department, emp.Salary, emp.JoiningDate);
                        }
                    }
                }
                else if (choice == "3")
                {
                    // View Employees by Department
                    Console.WriteLine("\n--- Employees by Department ---");
                    if (Employees.Count == 0)
                    {
                        Console.WriteLine("No employees found!");
                    }
                    else
                    {
                        var grouped = hrManager.GroupEmployeesByDepartment();
                        foreach (var dept in grouped)
                        {
                            Console.WriteLine($"\n{dept.Key} Department ({dept.Value.Count} employees):");
                            foreach (var emp in dept.Value)
                            {
                                Console.WriteLine($"  {emp.EmployeeId} - {emp.Name} - ${emp.Salary:F2}");
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    // Calculate Department Salary
                    Console.WriteLine("\n--- Calculate Department Salary ---");
                    if (Employees.Count == 0)
                    {
                        Console.WriteLine("No employees found!");
                    }
                    else
                    {
                        Console.Write("Enter Department Name: ");
                        string dept = Console.ReadLine();
                        double total = hrManager.CalculateDepartmentSalary(dept);
                        if (total > 0)
                        {
                            Console.WriteLine($"Total Salary for {dept}: ${total:F2}");
                        }
                        else
                        {
                            Console.WriteLine("Department not found!");
                        }
                    }
                }
                else if (choice == "5")
                {
                    // Find Employees Joined After Date
                    Console.WriteLine("\n--- Find Employees Joined After Date ---");
                    if (Employees.Count == 0)
                    {
                        Console.WriteLine("No employees found!");
                    }
                    else
                    {
                        Console.Write("Enter Date (yyyy-mm-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        var result = hrManager.GetEmployeesJoinedAfter(date);
                        
                        if (result.Count > 0)
                        {
                            Console.WriteLine($"\nFound {result.Count} employees:");
                            foreach (var emp in result)
                            {
                                Console.WriteLine($"{emp.EmployeeId} - {emp.Name} - {emp.Department} - Joined: {emp.JoiningDate:d}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employees found!");
                        }
                    }
                }
                else if (choice == "6")
                {
                    // Exit
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
