CREATE DATABASE EMP_DEPT;
Go

-- Create Department Table
CREATE TABLE Departments 
( Id INT PRIMARY KEY IDENTITY(1,1), Name NVARCHAR(100) NOT NULL ); -- Create Employee Table CREATE TABLE Employees ( Id INT PRIMARY KEY IDENTITY(1,1), Name NVARCHAR(100) NOT NULL, Email NVARCHAR(150) NOT NULL, Salary DECIMAL(18,2) NOT NULL, DepartmentId INT NOT NULL, CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE CASCADE );


-- Insert Sample Departments
INSERT INTO Departments (Name)
VALUES 
('Human Resources'),
('Information Technology'),
('Finance'),
('Marketing'),
('Sales');


-- Insert Sample Employees
INSERT INTO Employees (Name, Email, Salary, DepartmentId)
VALUES
('John Smith', 'john.smith@company.com', 60000.00, 1),
('Sarah Johnson', 'sarah.johnson@company.com', 75000.00, 2),
('Michael Brown', 'michael.brown@company.com', 82000.00, 2),
('Emily Davis', 'emily.davis@company.com', 67000.00, 3),
('David Wilson', 'david.wilson@company.com', 72000.00, 4),
('Sophia Martinez', 'sophia.martinez@company.com', 68000.00, 5),
('James Anderson', 'james.anderson@company.com', 90000.00, 2),
('Olivia Thomas', 'olivia.thomas@company.com', 58000.00, 1),
('Daniel Taylor', 'daniel.taylor@company.com', 64000.00, 3),
('Isabella Moore', 'isabella.moore@company.com', 71000.00, 4);
