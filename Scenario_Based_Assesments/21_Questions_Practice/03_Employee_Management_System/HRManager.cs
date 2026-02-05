using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem
{
    public class HRManager
    {
        // Add employee with auto-generated ID
        public void AddEmployee(string name, string dept, double salary)
        {
            int id = Program.Employees.Count + 1;
            string empId = $"E{id:D3}";
            Program.Employees.Add(empId, new Employee(empId, name, dept, salary));
        }

        // Group employees by department
        public SortedDictionary<string, List<Employee>> GroupEmployeesByDepartment()
        {
            var result = Program.Employees.Values.GroupBy(e => e.Department).ToDictionary(e => e.Key, e => e.ToList());
            return new SortedDictionary<string, List<Employee>>(result);
        }

        // Calculate total salary for a department
        public double CalculateDepartmentSalary(string department)
        {
            return Program.Employees.Values.Where(e => e.Department == department).Sum(e => e.Salary);
        }
        
        // Get employees who joined after a specific date
        public List<Employee> GetEmployeesJoinedAfter(DateTime date)
        {
            return Program.Employees.Values.Where(e => e.JoiningDate >= date).ToList();
        }
    }
}
