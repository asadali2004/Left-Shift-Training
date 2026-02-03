namespace EMS
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        
        public Employee(string empid, string name, string department, double salary)
        {
            EmployeeId = empid;
            Name = name;
            Department = department;
            Salary = salary;
            JoiningDate = DateTime.Now;
        }
    }
}