namespace SimpleMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One Department → Many Employees
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
