// Base constraints
public interface IStudent
{
    int StudentId { get; }
    string Name { get; }
    int Semester { get; }
}

public interface ICourse
{
    string CourseCode { get; }
    string Title { get; }
    int MaxCapacity { get; }
    int Credits { get; }
}

// 1. Generic enrollment system
public class EnrollmentSystem<TStudent, TCourse>
    where TStudent : IStudent
    where TCourse : ICourse
{
    private Dictionary<TCourse, List<TStudent>> _enrollments = new();
    
    // TODO: Enroll student with constraints
    public bool EnrollStudent(TStudent student, TCourse course)
    {
        // Rules:
        // - Course not at capacity
        // - Student not already enrolled
        // - Student semester >= course prerequisite (if any)
        // - Return success/failure with reason
        
        // Initialize course list if not exists
        if (!_enrollments.ContainsKey(course))
        {
            _enrollments[course] = new List<TStudent>();
        }
        
        // Check if course is at capacity
        if (_enrollments[course].Count >= course.MaxCapacity)
        {
            Console.WriteLine($"Enrollment failed: Course {course.CourseCode} is at full capacity.");
            return false;
        }
        
        // Check if student is already enrolled
        if (_enrollments[course].Contains(student))
        {
            Console.WriteLine($"Enrollment failed: {student.Name} is already enrolled in {course.CourseCode}.");
            return false;
        }
        
        // Check prerequisite (semester requirement) for LabCourse
        if (course is LabCourse labCourse)
        {
            if (student.Semester < labCourse.RequiredSemester)
            {
                Console.WriteLine($"Enrollment failed: {student.Name} (Semester {student.Semester}) does not meet prerequisite (Semester {labCourse.RequiredSemester}) for {course.CourseCode}.");
                return false;
            }
        }
        
        // Enroll the student
        _enrollments[course].Add(student);
        Console.WriteLine($"✓ Successfully enrolled {student.Name} in {course.CourseCode}.");
        return true;
    }
    
    // TODO: Get students for course
    public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
    {
        // Return immutable list
        if (_enrollments.ContainsKey(course))
        {
            return _enrollments[course].AsReadOnly();
        }
        return new List<TStudent>().AsReadOnly();
    }
    
    // TODO: Get courses for student
    public IEnumerable<TCourse> GetStudentCourses(TStudent student)
    {
        // Return courses student is enrolled in
        return _enrollments
            .Where(kvp => kvp.Value.Contains(student))
            .Select(kvp => kvp.Key);
    }
    
    // TODO: Calculate student workload
    public int CalculateStudentWorkload(TStudent student)
    {
        // Sum credits of all enrolled courses
        return GetStudentCourses(student).Sum(course => course.Credits);
    }
}

// 2. Specialized implementations
public class EngineeringStudent : IStudent
{
    public int StudentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Semester { get; set; }
    public string Specialization { get; set; } = string.Empty;
}

public class LabCourse : ICourse
{
    public string CourseCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public int Credits { get; set; }
    public string LabEquipment { get; set; } = string.Empty;
    public int RequiredSemester { get; set; } // Prerequisite
}

// 3. Generic gradebook
public class GradeBook<TStudent, TCourse>
    where TStudent : IStudent
    where TCourse : ICourse
{
    private Dictionary<(TStudent, TCourse), double> _grades = new();
    
    private EnrollmentSystem<TStudent, TCourse>? _enrollmentSystem;
    
    // Set enrollment system reference for validation
    public void SetEnrollmentSystem(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
    {
        _enrollmentSystem = enrollmentSystem;
    }
    
    // TODO: Add grade with validation
    public void AddGrade(TStudent student, TCourse course, double grade)
    {
        // Grade must be between 0 and 100
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine($"Error: Grade must be between 0 and 100. Provided: {grade}");
            return;
        }
        
        // Student must be enrolled in course
        if (_enrollmentSystem != null)
        {
            var studentCourses = _enrollmentSystem.GetStudentCourses(student);
            if (!studentCourses.Contains(course))
            {
                Console.WriteLine($"Error: {student.Name} is not enrolled in {course.CourseCode}.");
                return;
            }
        }
        
        _grades[(student, course)] = grade;
        Console.WriteLine($"✓ Grade {grade} added for {student.Name} in {course.CourseCode}.");
    }
    
    // TODO: Calculate GPA for student
    public double? CalculateGPA(TStudent student)
    {
        // Weighted by course credits
        // Return null if no grades
        var studentGrades = _grades
            .Where(kvp => kvp.Key.Item1.Equals(student))
            .ToList();
        
        if (studentGrades.Count == 0)
        {
            return null;
        }
        
        double totalWeightedGrade = 0;
        int totalCredits = 0;
        
        foreach (var gradeEntry in studentGrades)
        {
            var course = gradeEntry.Key.Item2;
            var grade = gradeEntry.Value;
            
            totalWeightedGrade += grade * course.Credits;
            totalCredits += course.Credits;
        }
        
        return totalCredits > 0 ? totalWeightedGrade / totalCredits : null;
    }
    
    // TODO: Find top student in course
    public (TStudent student, double grade)? GetTopStudent(TCourse course)
    {
        // Return student with highest grade
        var courseGrades = _grades
            .Where(kvp => kvp.Key.Item2.Equals(course))
            .ToList();
        
        if (courseGrades.Count == 0)
        {
            return null;
        }
        
        var topGrade = courseGrades.OrderByDescending(kvp => kvp.Value).First();
        return (topGrade.Key.Item1, topGrade.Value);
    }
}

// 4. TEST SCENARIO: Create a simulation
// a) Create 3 EngineeringStudent instances
// b) Create 2 LabCourse instances with prerequisites
// c) Demonstrate:
//    - Successful enrollment
//    - Failed enrollment (capacity, prerequisite)
//    - Grade assignment
//    - GPA calculation
//    - Top student per course

class Program
{
    static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("  University Course Registration System");
        Console.WriteLine("========================================\n");
        
        // a) Create 3 EngineeringStudent instances
        Console.WriteLine("=== Creating Students ===");
        var student1 = new EngineeringStudent
        {
            StudentId = 1001,
            Name = "Alice Johnson",
            Semester = 3,
            Specialization = "Computer Science"
        };
        Console.WriteLine($"Created: {student1.Name} (ID: {student1.StudentId}, Semester: {student1.Semester}, Specialization: {student1.Specialization})");
        
        var student2 = new EngineeringStudent
        {
            StudentId = 1002,
            Name = "Bob Smith",
            Semester = 2,
            Specialization = "Electrical Engineering"
        };
        Console.WriteLine($"Created: {student2.Name} (ID: {student2.StudentId}, Semester: {student2.Semester}, Specialization: {student2.Specialization})");
        
        var student3 = new EngineeringStudent
        {
            StudentId = 1003,
            Name = "Charlie Davis",
            Semester = 4,
            Specialization = "Mechanical Engineering"
        };
        Console.WriteLine($"Created: {student3.Name} (ID: {student3.StudentId}, Semester: {student3.Semester}, Specialization: {student3.Specialization})");
        
        // b) Create 2 LabCourse instances with prerequisites
        Console.WriteLine("\n=== Creating Courses ===");
        var course1 = new LabCourse
        {
            CourseCode = "CS301",
            Title = "Data Structures Lab",
            MaxCapacity = 2, // Small capacity to test capacity constraint
            Credits = 4,
            LabEquipment = "Computers, IDEs",
            RequiredSemester = 3 // Requires semester 3
        };
        Console.WriteLine($"Created: {course1.CourseCode} - {course1.Title}");
        Console.WriteLine($"  Max Capacity: {course1.MaxCapacity}, Credits: {course1.Credits}, Required Semester: {course1.RequiredSemester}");
        
        var course2 = new LabCourse
        {
            CourseCode = "EE401",
            Title = "Advanced Electronics Lab",
            MaxCapacity = 3,
            Credits = 3,
            LabEquipment = "Oscilloscopes, Breadboards",
            RequiredSemester = 4 // Requires semester 4
        };
        Console.WriteLine($"Created: {course2.CourseCode} - {course2.Title}");
        Console.WriteLine($"  Max Capacity: {course2.MaxCapacity}, Credits: {course2.Credits}, Required Semester: {course2.RequiredSemester}");
        
        // Create enrollment system and gradebook
        var enrollmentSystem = new EnrollmentSystem<EngineeringStudent, LabCourse>();
        var gradeBook = new GradeBook<EngineeringStudent, LabCourse>();
        gradeBook.SetEnrollmentSystem(enrollmentSystem);
        
        // c) Demonstrate: Successful enrollment
        Console.WriteLine("\n=== Testing Successful Enrollments ===");
        enrollmentSystem.EnrollStudent(student1, course1); // Should succeed (semester 3, course requires 3)
        enrollmentSystem.EnrollStudent(student3, course1); // Should succeed (semester 4, course requires 3)
        enrollmentSystem.EnrollStudent(student3, course2); // Should succeed (semester 4, course requires 4)
        
        // Demonstrate: Failed enrollment (prerequisite)
        Console.WriteLine("\n=== Testing Failed Enrollment (Prerequisite) ===");
        enrollmentSystem.EnrollStudent(student2, course1); // Should fail (semester 2, course requires 3)
        enrollmentSystem.EnrollStudent(student2, course2); // Should fail (semester 2, course requires 4)
        
        // Demonstrate: Failed enrollment (capacity)
        Console.WriteLine("\n=== Testing Failed Enrollment (Capacity) ===");
        enrollmentSystem.EnrollStudent(student2, course1); // Still fails due to prerequisite
        
        // Enroll student2 in a way that will cause capacity issue
        // First, let's modify student2's semester to meet prerequisite
        student2.Semester = 3;
        Console.WriteLine($"\nUpdated {student2.Name} to Semester {student2.Semester}");
        enrollmentSystem.EnrollStudent(student2, course1); // Should fail (capacity is 2, already full)
        
        // Demonstrate: Duplicate enrollment
        Console.WriteLine("\n=== Testing Duplicate Enrollment ===");
        enrollmentSystem.EnrollStudent(student1, course1); // Should fail (already enrolled)
        
        // Display enrolled students
        Console.WriteLine("\n=== Enrolled Students per Course ===");
        Console.WriteLine($"\nCourse: {course1.CourseCode} - {course1.Title}");
        var enrolledInCourse1 = enrollmentSystem.GetEnrolledStudents(course1);
        foreach (var student in enrolledInCourse1)
        {
            Console.WriteLine($"  - {student.Name} (Semester {student.Semester})");
        }
        
        Console.WriteLine($"\nCourse: {course2.CourseCode} - {course2.Title}");
        var enrolledInCourse2 = enrollmentSystem.GetEnrolledStudents(course2);
        foreach (var student in enrolledInCourse2)
        {
            Console.WriteLine($"  - {student.Name} (Semester {student.Semester})");
        }
        
        // Display student workloads
        Console.WriteLine("\n=== Student Workloads (Total Credits) ===");
        Console.WriteLine($"{student1.Name}: {enrollmentSystem.CalculateStudentWorkload(student1)} credits");
        Console.WriteLine($"{student2.Name}: {enrollmentSystem.CalculateStudentWorkload(student2)} credits");
        Console.WriteLine($"{student3.Name}: {enrollmentSystem.CalculateStudentWorkload(student3)} credits");
        
        // Demonstrate: Grade assignment
        Console.WriteLine("\n=== Adding Grades ===");
        gradeBook.AddGrade(student1, course1, 85.5);
        gradeBook.AddGrade(student3, course1, 92.0);
        gradeBook.AddGrade(student3, course2, 88.5);
        
        // Test invalid grade
        Console.WriteLine("\n=== Testing Invalid Grade ===");
        gradeBook.AddGrade(student1, course1, 105); // Should fail (grade > 100)
        gradeBook.AddGrade(student1, course1, -10); // Should fail (grade < 0)
        
        // Test grade for non-enrolled student
        Console.WriteLine("\n=== Testing Grade for Non-Enrolled Student ===");
        gradeBook.AddGrade(student2, course1, 80); // Should fail (student2 not enrolled in course1)
        
        // Demonstrate: GPA calculation
        Console.WriteLine("\n=== Calculating GPAs (Weighted by Credits) ===");
        var gpa1 = gradeBook.CalculateGPA(student1);
        Console.WriteLine($"{student1.Name}: GPA = {(gpa1.HasValue ? gpa1.Value.ToString("F2") : "No grades")}");
        
        var gpa2 = gradeBook.CalculateGPA(student2);
        Console.WriteLine($"{student2.Name}: GPA = {(gpa2.HasValue ? gpa2.Value.ToString("F2") : "No grades")}");
        
        var gpa3 = gradeBook.CalculateGPA(student3);
        Console.WriteLine($"{student3.Name}: GPA = {(gpa3.HasValue ? gpa3.Value.ToString("F2") : "No grades")}");
        
        // Demonstrate: Top student per course
        Console.WriteLine("\n=== Top Students per Course ===");
        var topInCourse1 = gradeBook.GetTopStudent(course1);
        if (topInCourse1.HasValue)
        {
            Console.WriteLine($"Course {course1.CourseCode}: {topInCourse1.Value.student.Name} with grade {topInCourse1.Value.grade}");
        }
        
        var topInCourse2 = gradeBook.GetTopStudent(course2);
        if (topInCourse2.HasValue)
        {
            Console.WriteLine($"Course {course2.CourseCode}: {topInCourse2.Value.student.Name} with grade {topInCourse2.Value.grade}");
        }
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("  Demo Completed Successfully!");
        Console.WriteLine("========================================");
    }
}
