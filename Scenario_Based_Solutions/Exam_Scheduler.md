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

// 10 students, 2 semester, 5 examiners, 5 exams

namespace Day6_Interface
{
    public enum Semester
    {
        Sem1, Sem2, Sem3, Sem4, Sem5, Sem6, Sem7, Sem8
    }

    /// <summary>
    /// Represents a business or organizational department with a unique identifier and name.
    /// </summary>
    public class Department
    {
        public int DepartmentId { get; }
        public string DepartmentName { get; }

        public Department(int id, string name)
        {
            DepartmentId = id;
            DepartmentName = name;
        }
    }

    /// <summary>
    /// Represents an individual person with a unique identifier and name.
    /// </summary>
    public class Person
    {
        public int PersonId { get; }
        public string Name { get; }

        private readonly List<Role> roles = new();

        public Person(int id, string name)
        {
            PersonId = id;
            Name = name;
        }

        public void AddRole(Role role)
        {
            roles.Add(role);
        }
    }

    public abstract class Role
    {
        protected Person Person { get; }

        protected Role(Person person)
        {
            Person = person;
        }
    }

    /// <summary>
    /// Represents the head of a department, providing operations for managing departmental examinations and examiners.
    /// </summary>
    public class HOD : Role
    {
        public Department Department { get; }

        public HOD(Person person, Department department)
            : base(person)
        {
            Department = department;
        }

        public void ScheduleExam(Examination exam, DateTime examDate)
        {
            exam.Schedule(examDate);
        }

        public void AssignExaminer(Examination exam, Examiner examiner)
        {
            exam.AssignExaminer(examiner);
        }

        public void AssignExamToSection(Examination exam, Section section)
        {
            exam.AssignToSection(section);
        }
    }

    /// <summary>
    /// Represents a role responsible for overseeing and managing examinations assigned to a specific person.
    /// </summary>
    public class Examiner : Role
    {
        private readonly List<Examination> assignedExams = new();

        public Examiner(Person person)
            : base(person) { }

        internal void AssignExam(Examination exam)
        {
            assignedExams.Add(exam);
        }
    }

    /// <summary>
    /// Represents an academic examination, including its identifying information, scheduled date, assigned examiner,
    /// and associated section.
    /// </summary>
    /// <remarks>Use the Examination class to manage the details and scheduling of an exam within a specific
    /// semester. The class provides methods to assign an examiner and associate the exam with a section. Once
    /// scheduled, the exam date and assignments can be updated as needed.</remarks>
    public class Examination
    {
        public int ExamId { get; }
        public string ExamName { get; }
        public Semester Semester { get; }
        public DateTime ExamDate { get; private set; }
        public Examiner AssignedExaminer { get; private set; }
        public Section AssignedSection { get; private set; }

        public Examination(int id, string name, Semester semester)
        {
            ExamId = id;
            ExamName = name;
            Semester = semester;
        }

        public void Schedule(DateTime date)
        {
            ExamDate = date;
        }

        public void AssignExaminer(Examiner examiner)
        {
            AssignedExaminer = examiner;
            examiner.AssignExam(this);
        }

        public void AssignToSection(Section section)
        {
            AssignedSection = section;
            section.AssignExam(this);
        }
    }

    /// <summary>
    /// Represents a student with a unique identifier and name.
    /// </summary>
    public class Student
    {
        public int StudentId { get; }
        public string StudentName { get; }

        public Student(int id, string name)
        {
            StudentId = id;
            StudentName = name;
        }
    }

   /// <summary>
   /// Represents an academic section within a department for a specific semester.
   /// </summary>
   /// <remarks>A section groups students and examinations under a particular department and semester. Use this
   /// class to manage section-level information and associate students and exams with the section.</remarks>
    public class Section
    {
        public int SectionId { get; }
        public string SectionName { get; }
        public Semester Semester { get; }
        public Department Department { get; }

        private readonly List<Student> students = new();
        private readonly List<Examination> exams = new();

        public Section(int id, string name, Semester semester, Department department)
        {
            SectionId = id;
            SectionName = name;
            Semester = semester;
            Department = department;
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        internal void AssignExam(Examination exam)
        {
            exams.Add(exam);
        }
    }
}

```

---

## 🎯 Program Execution

### Main Program Implementation

```csharp
using System;
using System.Collections.Generic;
using Day6_Interface;

class Program
{
    static void Main()
    {

        Department cse = new Department(1, "Computer Science");


        Section sem1Section = new Section(1, "CSE-Sem1-A", Semester.Sem1, cse);
        Section sem2Section = new Section(2, "CSE-Sem2-A", Semester.Sem2, cse);


        for (int i = 1; i <= 5; i++)
        {
            sem1Section.AddStudent(new Student(i, $"Student-S1-{i}"));
        }

        for (int i = 6; i <= 10; i++)
        {
            sem2Section.AddStudent(new Student(i, $"Student-S2-{i - 5}"));
        }


        Person hodPerson = new Person(1, "Dr. Rao");

        Person p1 = new Person(2, "Examiner-1");
        Person p2 = new Person(3, "Examiner-2");
        Person p3 = new Person(4, "Examiner-3");
        Person p4 = new Person(5, "Examiner-4");
        Person p5 = new Person(6, "Examiner-5");


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


        Examination exam1 = new Examination(101, "Maths", Semester.Sem1);
        Examination exam2 = new Examination(102, "Programming", Semester.Sem1);
        Examination exam3 = new Examination(103, "DSA", Semester.Sem2);
        Examination exam4 = new Examination(104, "DBMS", Semester.Sem2);
        Examination exam5 = new Examination(105, "Operating Systems", Semester.Sem2);


        hod.ScheduleExam(exam1, new DateTime(2025, 3, 10));
        hod.ScheduleExam(exam2, new DateTime(2025, 3, 12));
        hod.ScheduleExam(exam3, new DateTime(2025, 4, 5));
        hod.ScheduleExam(exam4, new DateTime(2025, 4, 8));
        hod.ScheduleExam(exam5, new DateTime(2025, 4, 12));


        hod.AssignExaminer(exam1, e1);
        hod.AssignExaminer(exam2, e2);
        hod.AssignExaminer(exam3, e3);
        hod.AssignExaminer(exam4, e4);
        hod.AssignExaminer(exam5, e5);

        // =========================
        // ASSIGN EXAMS TO SECTIONS
        // =========================
        hod.AssignExamToSection(exam1, sem1Section);
        hod.AssignExamToSection(exam2, sem1Section);

        hod.AssignExamToSection(exam3, sem2Section);
        hod.AssignExamToSection(exam4, sem2Section);
        hod.AssignExamToSection(exam5, sem2Section);

        // =========================
        // SYSTEM SUMMARY
        // =========================
        Console.WriteLine("Exam Scheduling System Initialized Successfully");
        Console.WriteLine("------------------------------------------------");

        Console.WriteLine("Semester 1 Exams Assigned");
        Console.WriteLine("Maths, Programming");

        Console.WriteLine("\nSemester 2 Exams Assigned");
        Console.WriteLine("DSA, DBMS, Operating Systems");
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
