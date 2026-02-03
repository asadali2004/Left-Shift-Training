namespace EMS
{
    public class Program
    {
        public static SortedDictionary<string, Employee> Employees = new SortedDictionary<string, Employee>();

        public static void Main(string[] args)
        {
            HRManager manager = new HRManager();
            
            // Hardcoded employee data for testing
            manager.AddEmployee("Avisek", "IT", 25000);
            manager.AddEmployee("Asad", "IT", 35000);
            manager.AddEmployee("Rajesh", "HR", 28000);
            manager.AddEmployee("Priya", "HR", 32000);
            manager.AddEmployee("Vikram", "Sales", 30000);
            manager.AddEmployee("Sneha", "Sales", 27000);
            
            int choice;
            bool run = true;

            while (run)
            {
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Group Employees By Department");
                Console.WriteLine("3. Calculate Department Salary");
                Console.WriteLine("4. Get Employees Joined After Date");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter your choice");
                choice = int.Parse(Console.ReadLine()!);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter employee name");
                        string name = Console.ReadLine()!;
                        Console.WriteLine("Enter department");
                        string dept = Console.ReadLine()!;
                        Console.WriteLine("Enter salary");
                        double salary = double.Parse(Console.ReadLine()!);
                        manager.AddEmployee(name, dept, salary);
                        Console.WriteLine("Employee added successfully");
                        Console.WriteLine();
                        break;

                    case 2:
                        var groupedEmployees = manager.GroupEmployeesByDepartment();
                        foreach (var department in groupedEmployees)
                        {
                            Console.WriteLine(department.Key);
                            foreach (var emp in department.Value)
                            {
                                Console.WriteLine($"{emp.EmployeeId} - {emp.Name}");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter department");
                        string deptName = Console.ReadLine()!;
                        double totalSalary = manager.CalculateDepartmentSalary(deptName);
                        Console.WriteLine($"Total salary for {deptName}: {totalSalary}");
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.WriteLine("Enter date (yyyy-MM-dd)");
                        DateTime date = DateTime.Parse(Console.ReadLine()!);
                        var recentEmployees = manager.GetEmployeesJoinedAfter(date);
                        foreach (var emp in recentEmployees)
                        {
                            Console.WriteLine($"{emp.EmployeeId} - {emp.Name} - {emp.JoiningDate}");
                        }
                        Console.WriteLine();
                        break;

                    case 5:
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}