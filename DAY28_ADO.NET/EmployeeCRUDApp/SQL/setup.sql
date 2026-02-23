IF DB_ID(N'TrainingDB') IS NULL
BEGIN
    CREATE DATABASE TrainingDB;
END;
GO

USE TrainingDB;
GO

IF OBJECT_ID(N'dbo.Employees', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        EmployeeId  INT           IDENTITY(1,1) NOT NULL,
        Name        NVARCHAR(100) NOT NULL,
        Email       NVARCHAR(100) NOT NULL,
        Department  NVARCHAR(50)  NOT NULL,
        Salary      DECIMAL(10,2) NOT NULL,
        IsActive    BIT           NOT NULL CONSTRAINT DF_Employees_IsActive DEFAULT (1),

        CONSTRAINT PK_Employees         PRIMARY KEY (EmployeeId),
        CONSTRAINT UQ_Employees_Email   UNIQUE      (Email),
        CONSTRAINT CK_Employees_Salary  CHECK       (Salary >= 0)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Employees)
BEGIN
    INSERT INTO dbo.Employees (Name, Email, Department, Salary)
    VALUES
        (N'Asad Ali',       N'asad.ali@gmail.com',  N'IT',      45000.00),
        (N'Vishal Sharma',  N'vsharma@gmail.com',   N'HR',      40000.00),
        (N'Anushka Palit',  N'anupalit@gmail.com',  N'Finance', 55000.00);
END;
GO