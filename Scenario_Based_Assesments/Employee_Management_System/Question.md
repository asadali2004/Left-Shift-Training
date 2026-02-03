# Question 3: Employee Management System

## 📘 Scenario
A company needs to manage employees and organize them by department.

## 🛠️ Requirements

### In class Employee, implement the following properties:
- `int EmployeeId`
- `string Name`
- `string Department`
- `double Salary`
- `DateTime JoiningDate`

### In class HRManager, implement the following methods:

#### Method 1
```csharp
public void AddEmployee(string name, string dept, double salary)
```
- Auto-generate EmployeeId (E001, E002...)

#### Method 2
```csharp
public SortedDictionary<string, List<Employee>> GroupEmployeesByDepartment()
```
- Groups employees by department

#### Method 3
```csharp
public double CalculateDepartmentSalary(string department)
```
- Returns total salary of a department

#### Method 4
```csharp
public List<Employee> GetEmployeesJoinedAfter(DateTime date)
```
- Returns employees joined after specific date

## Sample Use Cases
1. Add employees to HR/IT/Sales departments
2. Display department-wise employee lists
3. Calculate total salary expenditure per department
4. Find employees who joined recently
