using EmployeeApp.Data;
using EmployeeApp.Models;

namespace EmployeeApp;

class Program
{
    static readonly EmployeeRepository Repo = new(
        "Server=localhost\\SQLEXPRESS;Database=TrainingDB;" +
        "Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;"
    );

    static void Main()
    {
        while (true)
        {
            ShowMenu();
            Console.Write("Select option: ");
            string? choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1": AddEmployee();    break;
                    case "2": ListEmployees();  break;
                    case "3": FindEmployee();   break;
                    case "4": EditEmployee();   break;
                    case "5": RemoveEmployee(); break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    // ── Menu ─────────────────────────────────────────────────────────────────

    static void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("      Employee Management System        ");
        Console.WriteLine("========================================");
        Console.WriteLine("  1. Add Employee");
        Console.WriteLine("  2. View All Employees");
        Console.WriteLine("  3. Find Employee by ID");
        Console.WriteLine("  4. Update Employee");
        Console.WriteLine("  5. Delete Employee");
        Console.WriteLine("  6. Exit");
        Console.WriteLine("========================================");
    }

    // ── CRUD Handlers ────────────────────────────────────────────────────────

    static void AddEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");

        Employee emp = new()
        {
            Name       = ReadRequired("Name"),
            Email      = ReadRequired("Email"),
            Department = ReadRequired("Department"),
            Salary     = ReadDecimal("Salary")
        };

        int newId = Repo.Add(emp);

        Console.WriteLine(newId > 0
            ? $"Employee added. ID = {newId}"
            : "Failed to add employee.");
    }

    static void ListEmployees()
    {
        Console.WriteLine("--- All Active Employees ---");

        List<Employee> list = Repo.GetAll();

        if (list.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (Employee emp in list)
            PrintEmployee(emp);
    }

    static void FindEmployee()
    {
        Console.WriteLine("--- Find Employee ---");
        int id          = ReadInt("Employee ID");
        Employee? emp   = Repo.GetById(id);

        if (emp is null)
            Console.WriteLine("Employee not found.");
        else
            PrintEmployee(emp);
    }

    static void EditEmployee()
    {
        Console.WriteLine("--- Update Employee ---");
        int id        = ReadInt("Employee ID");
        Employee? emp = Repo.GetById(id);

        if (emp is null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine("Press Enter to keep the existing value.\n");

        emp.Name       = ReadWithDefault("Name",       emp.Name);
        emp.Email      = ReadWithDefault("Email",      emp.Email);
        emp.Department = ReadWithDefault("Department", emp.Department);
        emp.Salary     = ReadDecimalWithDefault("Salary", emp.Salary);

        Console.WriteLine(Repo.Update(emp)
            ? "Employee updated successfully."
            : "Update failed.");
    }

    static void RemoveEmployee()
    {
        Console.WriteLine("--- Delete Employee ---");
        int id = ReadInt("Employee ID");

        Console.WriteLine(Repo.Delete(id)
            ? "Employee deleted."
            : "Employee not found.");
    }

    // ── Display ───────────────────────────────────────────────────────────────

    static void PrintEmployee(Employee emp)
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"  ID         : {emp.EmployeeId}");
        Console.WriteLine($"  Name       : {emp.Name}");
        Console.WriteLine($"  Email      : {emp.Email}");
        Console.WriteLine($"  Department : {emp.Department}");
        Console.WriteLine($"  Salary     : {emp.Salary:F2}");
    }

    // ── Input Helpers ─────────────────────────────────────────────────────────

    static string ReadRequired(string field)
    {
        while (true)
        {
            Console.Write($"{field}: ");
            string? value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
                return value.Trim();

            Console.WriteLine($"{field} is required.");
        }
    }

    static int ReadInt(string field)
    {
        while (true)
        {
            Console.Write($"{field}: ");
            if (int.TryParse(Console.ReadLine(), out int v) && v > 0)
                return v;

            Console.WriteLine("Please enter a valid positive number.");
        }
    }

    static decimal ReadDecimal(string field)
    {
        while (true)
        {
            Console.Write($"{field}: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal v) && v >= 0)
                return v;

            Console.WriteLine("Please enter a valid amount.");
        }
    }

    static string ReadWithDefault(string field, string current)
    {
        Console.Write($"{field} ({current}): ");
        string? input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? current : input.Trim();
    }

    static decimal ReadDecimalWithDefault(string field, decimal current)
    {
        while (true)
        {
            Console.Write($"{field} ({current:F2}): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return current;

            if (decimal.TryParse(input, out decimal v) && v >= 0)
                return v;

            Console.WriteLine("Please enter a valid amount.");
        }
    }
}