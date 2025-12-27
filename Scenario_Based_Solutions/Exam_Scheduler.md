# Exam Scheduler System

## 📋 Problem Statement

### Scenario
Design and implement a comprehensive **Exam Scheduling System** for a university department that manages:
- **Multiple semesters** (Sem1 through Sem8)
- **Students** enrolled in different sections
- **Examinations** scheduled for each semester
- **Examiners** assigned to oversee exams
- **Head of Department (HOD)** who manages the entire examination process
- **Sections** grouping students by department and semester

### Requirements
1. A department head should be able to schedule exams for different semesters
2. Each exam must be assigned to a specific examiner
3. Exams should be associated with particular sections
4. The system should support multiple roles for individuals (Person can be HOD, Examiner, etc.)
5. Maintain proper relationships between departments, sections, students, and exams
6. Display comprehensive exam scheduling details

### Business Rules
- Each examination belongs to a specific semester
- One examiner is assigned per examination
- Sections are department and semester-specific
- HOD has authority to schedule exams and assign examiners
- Students are grouped into sections

---

## 💡 Solution Architecture

### Key Design Patterns Used
1. **Role-Based Design Pattern**: Separation of Person entity from their roles (HOD, Examiner)
2. **Encapsulation**: Private fields with controlled access
3. **Composition**: Complex objects built from simpler ones
4. **Single Responsibility Principle**: Each class has one clear purpose

### Class Diagram Structure
```
Person → Role (Abstract)
           ↓
    ├── HOD
    └── Examiner

Department → Section → Student
                  ↓
             Examination
```

---

## 🔧 Implementation

### Complete C# Code Solution

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamSchedulerSystem
{
    #region Enums
    
    /// <summary>
    /// Enumeration representing academic semesters.
    /// </summary>
    public enum Semester
    {
        Sem1, Sem2, Sem3, Sem4, Sem5, Sem6, Sem7, Sem8
    }
    
    #endregion

    #region Core Entities
    
    /// <summary>
    /// Represents a business or organizational department with a unique identifier and name.
    /// </summary>
    public class Department
    {
        public int DepartmentId { get; }
        public string DepartmentName { get; }

        public Department(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name cannot be empty.", nameof(name));
            
            DepartmentId = id;
            DepartmentName = name;
        }

        public override string ToString() => $"[Dept-{DepartmentId}] {DepartmentName}";
    }

    /// <summary>
    /// Represents an individual person with a unique identifier and name.
    /// Can hold multiple roles within the system.
    /// </summary>
    public class Person
    {
        public int PersonId { get; }
        public string Name { get; }
        
        private readonly List<Role> roles = new();
        public IReadOnlyList<Role> Roles => roles.AsReadOnly();

        public Person(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Person name cannot be empty.", nameof(name));
            
            PersonId = id;
            Name = name;
        }

        public void AddRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            
            roles.Add(role);
        }

        public override string ToString() => $"[Person-{PersonId}] {Name}";
    }
    
    #endregion

    #region Role Hierarchy
    
    /// <summary>
    /// Abstract base class for all roles that a person can have.
    /// </summary>
    public abstract class Role
    {
        protected Person Person { get; }

        protected Role(Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }
    }

    /// <summary>
    /// Represents the Head of Department role with authority to manage examinations.
    /// </summary>
    public class HOD : Role
    {
        public Department Department { get; }

        public HOD(Person person, Department department)
            : base(person)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        public void ScheduleExam(Examination exam, DateTime examDate)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            
            Console.WriteLine($"[HOD {Person.Name}] Scheduling {exam.ExamName} on {examDate:dd-MMM-yyyy}");
            exam.Schedule(examDate);
        }

        public void AssignExaminer(Examination exam, Examiner examiner)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            if (examiner == null)
                throw new ArgumentNullException(nameof(examiner));
            
            Console.WriteLine($"[HOD {Person.Name}] Assigning {examiner.Person.Name} to {exam.ExamName}");
            exam.AssignExaminer(examiner);
        }

        public void AssignExamToSection(Examination exam, Section section)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            if (section == null)
                throw new ArgumentNullException(nameof(section));
            
            Console.WriteLine($"[HOD {Person.Name}] Assigning {exam.ExamName} to {section.SectionName}");
            exam.AssignToSection(section);
        }

        public override string ToString() => $"HOD of {Department.DepartmentName}";
    }

    /// <summary>
    /// Represents an examiner role responsible for overseeing examinations.
    /// </summary>
    public class Examiner : Role
    {
        private readonly List<Examination> assignedExams = new();
        public IReadOnlyList<Examination> AssignedExams => assignedExams.AsReadOnly();

        public Examiner(Person person) : base(person) { }

        internal void AssignExam(Examination exam)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            
            assignedExams.Add(exam);
        }

        public void DisplayAssignedExams()
        {
            Console.WriteLine($"\n[Examiner: {Person.Name}]");
            Console.WriteLine($"Total Exams Assigned: {assignedExams.Count}");
            foreach (var exam in assignedExams)
            {
                Console.WriteLine($"  • {exam.ExamName} ({exam.Semester}) - {exam.ExamDate:dd-MMM-yyyy}");
            }
        }

        public override string ToString() => $"Examiner {Person.Name}";
    }
    
    #endregion

    #region Academic Entities
    
    /// <summary>
    /// Represents an academic examination with scheduling and assignment capabilities.
    /// </summary>
    public class Examination
    {
        public int ExamId { get; }
        public string ExamName { get; }
        public Semester Semester { get; }
        public DateTime ExamDate { get; private set; }
        public Examiner AssignedExaminer { get; private set; }
        public Section AssignedSection { get; private set; }
        
        public bool IsScheduled => ExamDate != default;
        public bool HasExaminer => AssignedExaminer != null;
        public bool HasSection => AssignedSection != null;

        public Examination(int id, string name, Semester semester)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Exam name cannot be empty.", nameof(name));
            
            ExamId = id;
            ExamName = name;
            Semester = semester;
        }

        public void Schedule(DateTime date)
        {
            if (date < DateTime.Now)
                throw new ArgumentException("Cannot schedule exam in the past.");
            
            ExamDate = date;
        }

        public void AssignExaminer(Examiner examiner)
        {
            if (examiner == null)
                throw new ArgumentNullException(nameof(examiner));
            
            AssignedExaminer = examiner;
            examiner.AssignExam(this);
        }

        public void AssignToSection(Section section)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));
            
            AssignedSection = section;
            section.AssignExam(this);
        }

        public override string ToString() => 
            $"[Exam-{ExamId}] {ExamName} | {Semester} | {(IsScheduled ? ExamDate.ToString("dd-MMM-yyyy") : "Not Scheduled")}";
    }

    /// <summary>
    /// Represents a student enrolled in the system.
    /// </summary>
    public class Student
    {
        public int StudentId { get; }
        public string StudentName { get; }

        public Student(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Student name cannot be empty.", nameof(name));
            
            StudentId = id;
            StudentName = name;
        }

        public override string ToString() => $"[Student-{StudentId}] {StudentName}";
    }

    /// <summary>
    /// Represents an academic section grouping students and exams.
    /// </summary>
    public class Section
    {
        public int SectionId { get; }
        public string SectionName { get; }
        public Semester Semester { get; }
        public Department Department { get; }

        private readonly List<Student> students = new();
        private readonly List<Examination> exams = new();
        
        public IReadOnlyList<Student> Students => students.AsReadOnly();
        public IReadOnlyList<Examination> Exams => exams.AsReadOnly();

        public Section(int id, string name, Semester semester, Department department)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Section name cannot be empty.", nameof(name));
            
            SectionId = id;
            SectionName = name;
            Semester = semester;
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            
            students.Add(student);
        }

        internal void AssignExam(Examination exam)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            
            exams.Add(exam);
        }

        public void DisplaySectionInfo()
        {
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ Section: {SectionName,-50} ║");
            Console.WriteLine($"╠══════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Department: {Department.DepartmentName,-47} ║");
            Console.WriteLine($"║ Semester: {Semester,-49} ║");
            Console.WriteLine($"║ Total Students: {students.Count,-45} ║");
            Console.WriteLine($"║ Total Exams: {exams.Count,-48} ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════════╝");
            
            Console.WriteLine("\nStudents:");
            foreach (var student in students)
            {
                Console.WriteLine($"  • {student.StudentName}");
            }
            
            Console.WriteLine("\nScheduled Exams:");
            foreach (var exam in exams)
            {
                Console.WriteLine($"  • {exam.ExamName,-20} | {exam.ExamDate:dd-MMM-yyyy} | Examiner: {exam.AssignedExaminer?.Person.Name ?? "TBA"}");
            }
        }

        public override string ToString() => $"[Section] {SectionName} - {Semester}";
    }
    
    #endregion
}
```

---

## 🎯 Program Execution

### Main Program Implementation

```csharp
using System;
using ExamSchedulerSystem;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║       UNIVERSITY EXAM SCHEDULING SYSTEM v2.0             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

        // ═══════════════════════════════════════════════════════════
        // STEP 1: CREATE DEPARTMENT
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 1: Department Setup ───");
        Department cse = new Department(1, "Computer Science & Engineering");
        Console.WriteLine($"✓ Created: {cse}\n");

        // ═══════════════════════════════════════════════════════════
        // STEP 2: CREATE SECTIONS
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 2: Section Creation ───");
        Section sem1Section = new Section(1, "CSE-Sem1-A", Semester.Sem1, cse);
        Section sem2Section = new Section(2, "CSE-Sem2-A", Semester.Sem2, cse);
        Console.WriteLine($"✓ Created: {sem1Section}");
        Console.WriteLine($"✓ Created: {sem2Section}\n");

        // ═══════════════════════════════════════════════════════════
        // STEP 3: ENROLL STUDENTS
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 3: Student Enrollment ───");
        Console.WriteLine("Enrolling Semester 1 Students...");
        for (int i = 1; i <= 5; i++)
        {
            var student = new Student(i, $"Rajesh Kumar {i}");
            sem1Section.AddStudent(student);
            Console.WriteLine($"  ✓ {student.StudentName}");
        }

        Console.WriteLine("\nEnrolling Semester 2 Students...");
        for (int i = 6; i <= 10; i++)
        {
            var student = new Student(i, $"Priya Sharma {i - 5}");
            sem2Section.AddStudent(student);
            Console.WriteLine($"  ✓ {student.StudentName}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════
        // STEP 4: CREATE PERSONNEL (HOD & EXAMINERS)
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 4: Personnel Setup ───");
        Person hodPerson = new Person(1, "Dr. Ramesh Rao");
        Person p1 = new Person(2, "Prof. Anita Desai");
        Person p2 = new Person(3, "Dr. Vikram Singh");
        Person p3 = new Person(4, "Prof. Meena Patel");
        Person p4 = new Person(5, "Dr. Suresh Kumar");
        Person p5 = new Person(6, "Prof. Kavita Reddy");

        HOD hod = new HOD(hodPerson, cse);
        Examiner e1 = new Examiner(p1);
        Examiner e2 = new Examiner(p2);
        Examiner e3 = new Examiner(p3);
        Examiner e4 = new Examiner(p4);
        Examiner e5 = new Examiner(p5);

        hodPerson.AddRole(hod);
        p1.AddRole(e1);
        p2.AddRole(e2);
        p3.AddRole(e3);
        p4.AddRole(e4);
        p5.AddRole(e5);

        Console.WriteLine($"✓ HOD: {hodPerson.Name}");
        Console.WriteLine($"✓ Examiner: {p1.Name}");
        Console.WriteLine($"✓ Examiner: {p2.Name}");
        Console.WriteLine($"✓ Examiner: {p3.Name}");
        Console.WriteLine($"✓ Examiner: {p4.Name}");
        Console.WriteLine($"✓ Examiner: {p5.Name}\n");

        // ═══════════════════════════════════════════════════════════
        // STEP 5: CREATE EXAMINATIONS
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 5: Examination Creation ───");
        Examination exam1 = new Examination(101, "Mathematics-I", Semester.Sem1);
        Examination exam2 = new Examination(102, "Programming in C#", Semester.Sem1);
        Examination exam3 = new Examination(103, "Data Structures & Algorithms", Semester.Sem2);
        Examination exam4 = new Examination(104, "Database Management Systems", Semester.Sem2);
        Examination exam5 = new Examination(105, "Operating Systems", Semester.Sem2);
        
        Console.WriteLine($"✓ {exam1}");
        Console.WriteLine($"✓ {exam2}");
        Console.WriteLine($"✓ {exam3}");
        Console.WriteLine($"✓ {exam4}");
        Console.WriteLine($"✓ {exam5}\n");

        // ═══════════════════════════════════════════════════════════
        // STEP 6: SCHEDULE EXAMS (HOD Action)
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 6: Exam Scheduling ───");
        hod.ScheduleExam(exam1, new DateTime(2025, 3, 10));
        hod.ScheduleExam(exam2, new DateTime(2025, 3, 12));
        hod.ScheduleExam(exam3, new DateTime(2025, 4, 5));
        hod.ScheduleExam(exam4, new DateTime(2025, 4, 8));
        hod.ScheduleExam(exam5, new DateTime(2025, 4, 12));
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════
        // STEP 7: ASSIGN EXAMINERS (HOD Action)
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 7: Examiner Assignment ───");
        hod.AssignExaminer(exam1, e1);
        hod.AssignExaminer(exam2, e2);
        hod.AssignExaminer(exam3, e3);
        hod.AssignExaminer(exam4, e4);
        hod.AssignExaminer(exam5, e5);
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════
        // STEP 8: ASSIGN EXAMS TO SECTIONS (HOD Action)
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("─── Step 8: Section-Exam Assignment ───");
        hod.AssignExamToSection(exam1, sem1Section);
        hod.AssignExamToSection(exam2, sem1Section);
        hod.AssignExamToSection(exam3, sem2Section);
        hod.AssignExamToSection(exam4, sem2Section);
        hod.AssignExamToSection(exam5, sem2Section);
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════
        // SYSTEM SUMMARY & REPORTS
        // ═══════════════════════════════════════════════════════════
        Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    SYSTEM SUMMARY                         ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

        // Display Section Information
        sem1Section.DisplaySectionInfo();
        sem2Section.DisplaySectionInfo();

        // Display Examiner Workload
        Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                  EXAMINER WORKLOAD                        ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
        
        e1.DisplayAssignedExams();
        e2.DisplayAssignedExams();
        e3.DisplayAssignedExams();
        e4.DisplayAssignedExams();
        e5.DisplayAssignedExams();

        Console.WriteLine("\n\n╔═══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║     ✓ Exam Scheduling System Initialized Successfully    ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
    }
}
```

---

## 📊 Expected Output

```
╔═══════════════════════════════════════════════════════════╗
║       UNIVERSITY EXAM SCHEDULING SYSTEM v2.0             ║
╚═══════════════════════════════════════════════════════════╝

─── Step 1: Department Setup ───
✓ Created: [Dept-1] Computer Science & Engineering

─── Step 2: Section Creation ───
✓ Created: [Section] CSE-Sem1-A - Sem1
✓ Created: [Section] CSE-Sem2-A - Sem2

[Additional output showing all steps...]
```

---

## 🎓 Key Learning Outcomes

### Object-Oriented Programming Concepts
1. **Abstraction**: Role class as abstract base
2. **Encapsulation**: Private fields with public properties
3. **Inheritance**: HOD and Examiner inherit from Role
4. **Polymorphism**: Different role behaviors
5. **Composition**: Complex object relationships

### Best Practices Implemented
- ✅ Input validation with null checks
- ✅ Meaningful exception messages
- ✅ Read-only collections for data protection
- ✅ XML documentation comments
- ✅ Proper naming conventions
- ✅ Single Responsibility Principle
- ✅ Defensive programming

### Advanced Features
- Enum for type-safe semester representation
- Readonly properties for immutable data
- Collection initialization and management
- DateTime handling for scheduling
- Comprehensive error handling
- Rich console output formatting

---

## 🔄 Possible Enhancements

1. **Add exam result management**
2. **Implement conflict detection for exam dates**
3. **Add email notifications for schedule changes**
4. **Create exam room allocation system**
5. **Implement student attendance tracking**
6. **Add database persistence layer**
7. **Create web API for remote access**
8. **Implement exam re-scheduling functionality**

---

## 📝 Conclusion

This exam scheduler system demonstrates a robust, scalable solution for managing academic examinations. It employs sound OOP principles, proper separation of concerns, and maintainable code structure suitable for enterprise applications.
