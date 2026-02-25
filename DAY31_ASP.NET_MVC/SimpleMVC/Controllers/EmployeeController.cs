using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<Department> Departments = new List<Department>
        {
            new Department { Id = 1, Name = "IT" },
            new Department { Id = 2, Name = "HR" },
            new Department { Id = 3, Name = "Finance" }
        };
        
        public static List<Employee> Employees = new List<Employee>();

        // GET: Index
        public IActionResult Index()
        {
            return View(Employees);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Departments = Departments;
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Find selected department
                var dept = Departments.FirstOrDefault(d => d.Id == employee.DepartmentId);

                if (dept != null)
                {
                    // Set navigation property
                    employee.Department = dept;
                    
                    // Add employee to department's list
                    dept.Employees.Add(employee);
                }

                // Add to employee list
                Employees.Add(employee);

                return RedirectToAction("Index");
            }

            // If validation fails, reload departments
            ViewBag.Departments = Departments;
            return View(employee);
        }
    }
}
