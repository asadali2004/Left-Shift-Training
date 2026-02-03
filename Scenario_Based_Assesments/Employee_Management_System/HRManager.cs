namespace EMS
{
    public class HRManager
    {
        public void AddEmployee(string name, string dept, double salary)
        {
            int id = Program.Employees.Count + 1;
            string empId = $"E{id:D3}";
            Program.Employees.Add(empId, new Employee(empId, name, dept, salary));
        }

        public SortedDictionary<string, List<Employee>> GroupEmployeesByDepartment()
        {
            var result = Program.Employees.Values.GroupBy(e => e.Department).ToDictionary(e => e.Key, e => e.ToList());
            return new SortedDictionary<string, List<Employee>>(result);
        }

        public double CalculateDepartmentSalary(string department)
        {
            double result;
            result = Program.Employees.Values.Where(e => e.Department == department).Sum(e => e.Salary);
            return result;
        }
        
        public List<Employee> GetEmployeesJoinedAfter(DateTime date)
        {
            List<Employee> result = new List<Employee>();
            result = Program.Employees.Values.Where(e => e.JoiningDate >= date).ToList();
            return result;
        }
    }
}