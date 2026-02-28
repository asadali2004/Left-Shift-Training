using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static List<Employee> empList = new List<Employee>
    {
    new Employee() {EmployeeID = 1001,FirstName = "Malcolm",LastName = "Daruwalla",Title = "Manager",DOB = DateTime.Parse("1984-01-02"),DOJ = DateTime.Parse("2011-08-09"),City = "Mumbai"},
    new Employee() {EmployeeID = 1002,FirstName = "Asdin",LastName = "Dhalla",Title = "AsstManager",DOB = DateTime.Parse("1984-08-20"),DOJ = DateTime.Parse("2012-7-7"),City = "Mumbai"},
    new Employee() {EmployeeID = 1003,FirstName = "Madhavi",LastName = "Oza",Title = "Consultant",DOB = DateTime.Parse("1987-11-14"),DOJ = DateTime.Parse("2105-12-04"),City = "Pune"},
    new Employee() {EmployeeID = 1004,FirstName = "Saba",LastName = "Shaikh",Title = "SE",DOB = DateTime.Parse("6/3/1990"),DOJ = DateTime.Parse("2/2/2016"),City = "Pune"},
    new Employee() {EmployeeID = 1005,FirstName = "Nazia",LastName = "Shaikh",Title = "SE",DOB = DateTime.Parse("3/8/1991"),DOJ = DateTime.Parse("2/2/2016"),City = "Mumbai"},
    new Employee() {EmployeeID = 1006,FirstName = "Suresh",LastName = "Pathak",Title = "Consultant",DOB = DateTime.Parse("11/7/1989"),DOJ = DateTime.Parse("8/8/2014"),City = "Chennai"},
    new Employee() {EmployeeID = 1007,FirstName = "Vijay",LastName = "Natrajan",Title = "Consultant",DOB = DateTime.Parse("12/2/1989"),DOJ = DateTime.Parse("6/1/2015"),City = "Mumbai"},
    new Employee() {EmployeeID = 1008,FirstName = "Rahul",LastName = "Dubey",Title = "Associate",DOB = DateTime.Parse("11/11/1993"),DOJ = DateTime.Parse("11/6/2014"),City = "Chennai"},
    new Employee() {EmployeeID = 1009,FirstName = "Amit",LastName = "Mistry",Title = "Associate",DOB = DateTime.Parse("8/12/1992"),DOJ = DateTime.Parse("12/3/2014"),City = "Chennai"},
    new Employee() {EmployeeID = 1010,FirstName = "Sumit",LastName = "Shah",Title = "Manager",DOB = DateTime.Parse("4/12/1991"),DOJ = DateTime.Parse("1/2/2016"),City = "Pune"},
    };

    static void Main()
    {
        // a) All Employees
        Console.WriteLine("All Employees:");
        foreach (var emp in empList)
        {
            PrintDetails(emp);
        }

        // b) Employees in Mumbai
        Console.WriteLine("\nEmployees not in Mumbai:");
        var notMumbai = empList.Where(e => e.City != "Mumbai");
        foreach (var emp in notMumbai) 
        {
            PrintDetails(emp);
        }

        // c) Employees with Title AsstManager
        Console.WriteLine("\nEmployees with Title AsstManager:");
        var asstManagers = empList.Where(e => e.Title == "AsstManager");
        foreach (var emp in asstManagers)
        {
            PrintDetails(emp);
        }

        // d) Employees whose LastName starts with S
        Console.WriteLine("\nEmployees whose LastName starts with S:");
        var lastNameS = empList.Where(e => e.LastName.StartsWith("S"));
        foreach (var emp in lastNameS)
        {
            PrintDetails(emp);
        }

        // e) Employees who joined before 1/1/2015
        Console.WriteLine("\nEmployees who joined before 1/1/2015:");
        var joinedBefore2015 = empList.Where(e => e.DOJ < new DateTime(2015, 1, 1));
        foreach (var emp in joinedBefore2015)
        {
            PrintDetails(emp);
        }

        // f) Employee whose DOB is after 1/1/1990
        Console.WriteLine("\nEmployee whose DOB is after 1/1/1990");
        var DobAfter1990 = empList.Where(e => e.DOB > new DateTime(1990, 1, 1));
        foreach(var emp in DobAfter1990)
        {
            PrintDetails(emp); 
        }

        // g) Employee who is Consultant and Associate
        Console.WriteLine("\nEmployee who is Consultant and Associate");
        var ConsultAssoc = empList.Where(e => e.Title == "Consultant" || e.Title == "Associate");
        foreach(var emp in ConsultAssoc)
        {
            PrintDetails(emp);
        }

        // h) Total Number of Employee
        var CountOfEmp = empList.Count();
        Console.WriteLine($"\nTotal Number of Employee: {CountOfEmp}");

        //i) Employee From Chennai
         var EmpFromChennai = empList.Where(e=>e.City == "Chennai").Count();
        Console.WriteLine($"\nNumber of Employee from Chennai: {EmpFromChennai}");

        // j) Highest Employee id From Employee
        var HighestId = empList.Max(e=>e.EmployeeID);
        Console.WriteLine($"\nHighest Employee id From Employee: {HighestId}");

        //k) Number of Employee who joined after 1/1/2015
        var NoOfEmp = empList.Where(e => e.DOJ  > new DateTime(2015,1,1)).Count();
        Console.WriteLine($"\nNumber of Employee who joined after 1/1/2015: {NoOfEmp}");

        //l) Total Number of Employee who is not Associate
        var EmpNotAssociateCount = empList.Where(e => e.Title != "Associate").Count();
        Console.WriteLine($"\nTotal Number of Employee who is not Associate: {EmpNotAssociateCount}");

        // m) No of Emplyee Based on City
        Console.WriteLine("\nTotal Employees Based on City:");
        var NoOfEmpCity = empList
                            .GroupBy(e => e.City)
                            .Select(g => new
                            {
                                City = g.Key,
                                Count = g.Count()
                            });

        foreach (var item in NoOfEmpCity)
        {
            Console.WriteLine($"{item.City} : {item.Count}");
        }

        // n) Total Number of Employee based on City and title
        Console.WriteLine("\nTotal Number of Employee based on City and title:");
        var NoofEmpCityTitle = empList.GroupBy(e => new { e.City,e.Title}).Select(g => new { g.Key.City, g.Key.Title, count = g.Count() });
        foreach (var item in NoofEmpCityTitle)
        {
            Console.WriteLine($"{item.City} - {item.Title}: {item.count}");
        }

        // o) Total Number of employee who is youngest
        var youngestDOB = empList.Max(e => e.DOB);
        var youngestEmployees = empList.Where(e => e.DOB == youngestDOB);
        Console.WriteLine("\nYoungest Employee(s):");
        foreach (var emp in youngestEmployees)
        {
            PrintDetails(emp);
        }
    }

    // For Printing Employee details
    static void PrintDetails(Employee emp)
    {
        Console.WriteLine($"{emp.EmployeeID} | {emp.FirstName} {emp.LastName} | {emp.Title} | {emp.City} | DOJ: {emp.DOJ.ToShortDateString()}");
    }
}