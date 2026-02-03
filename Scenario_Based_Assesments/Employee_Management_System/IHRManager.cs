namespace EMS
{
    public interface IHRManager
    {
        void AddEmployee(string name, string dept, double salary);
        SortedDictionary<string, List<Employee>> GroupEmployeesByDepartment();
        double CalculateDepartmentSalary(string department);
        public List<Employee> GetEmployeesJoinedAfter(DateTime date);
    }
}