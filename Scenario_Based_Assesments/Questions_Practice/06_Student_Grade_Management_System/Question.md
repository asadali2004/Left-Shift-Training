# Question 6: Student Grade Management System (Advanced)

## Scenario
A school needs to manage student records and calculate performance metrics.

## Requirements

### In class Student:
- `int StudentId`
- `string Name`
- `string GradeLevel` (9th/10th/11th/12th)
- `Dictionary<string, double> Subjects` // Subject->Grade

### In class SchoolManager:

#### Method 1
```csharp
public void AddStudent(string name, string gradeLevel)
```
- Auto-generate StudentId

#### Method 2
```csharp
public void AddGrade(int studentId, string subject, double grade)
```
- Adds grade for student (0-100 scale)

#### Method 3
```csharp
public SortedDictionary<string, List<Student>> GroupStudentsByGradeLevel()
```
- Groups students by grade level

#### Method 4
```csharp
public double CalculateStudentAverage(int studentId)
```
- Returns student's average grade

#### Method 5
```csharp
public Dictionary<string, double> CalculateSubjectAverages()
```
- Returns average grade per subject across all students

#### Method 6
```csharp
public List<Student> GetTopPerformers(int count)
```
- Returns top N students by average grade

## Sample Use Cases:
1. Add students with grades in multiple subjects
2. Display students grouped by grade level
3. Calculate individual student averages
4. Find subject-wise performance
5. Identify top performers
