# Question 16: E-Learning Platform

## Scenario
An online learning platform needs to manage courses, students, and progress.

---

## Requirements

### Class: `Course`
- `string CourseCode`
- `string CourseName`
- `string Instructor`
- `int DurationWeeks`
- `double Price`
- `List<string> Modules`

---

### Class: `Enrollment`
- `int EnrollmentId`
- `string StudentId`
- `string CourseCode`
- `DateTime EnrollmentDate`
- `double ProgressPercentage`

---

### Class: `StudentProgress`
- `string StudentId`
- `string CourseCode`
- `Dictionary<string, double> ModuleScores` // Module → Score
- `DateTime LastAccessed`

---

### Class: `LearningManager`

```csharp
public void AddCourse(string code, string name, string instructor,
                     int weeks, double price, List<string> modules);
public bool EnrollStudent(string studentId, string courseCode);
public bool UpdateProgress(string studentId, string courseCode, 
                           string module, double score);
public Dictionary<string, List<Course>> GroupCoursesByInstructor();
public List<Enrollment> GetTopPerformingStudents(string courseCode, int count);
````

---

## Use Cases

* Create courses with modules
* Enroll students
* Track learning progress
* Group courses by instructor
* Identify top performers

