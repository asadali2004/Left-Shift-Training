using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeManagement
{
    public class SchoolManager
    {
        // Store all students
        public Dictionary<int, Student> Students = new Dictionary<int, Student>();
        private int nextId = 1;
        
        // Add student with auto-generated ID
        public void AddStudent(string name, string gradeLevel)
        {
            int studentId = nextId++;
            Students.Add(studentId, new Student
            {
                StudentId = studentId,
                Name = name,
                GradeLevel = gradeLevel,
                Subjects = new Dictionary<string, double>()
            });
        }

        // Add grade for a student
        public void AddGrade(int studentId, string subject, double grade)
        {
            if (Students.ContainsKey(studentId) && grade >= 0 && grade <= 100)
            {
                Students[studentId].Subjects[subject] = grade;
            }
        }

        // Group students by grade level
        public SortedDictionary<string, List<Student>> GroupStudentsByGradeLevel()
        {
            var result = Students.Values.GroupBy(s => s.GradeLevel).ToDictionary(g => g.Key, g => g.ToList());
            return new SortedDictionary<string, List<Student>>(result);
        }

        // Calculate student average
        public double CalculateStudentAverage(int studentId)
        {
            if (Students.ContainsKey(studentId) && Students[studentId].Subjects.Count > 0)
            {
                return Students[studentId].Subjects.Values.Average();
            }
            return 0;
        }

        // Calculate subject averages across all students
        public Dictionary<string, double> CalculateSubjectAverages()
        {
            var allSubjects = Students.Values.SelectMany(s => s.Subjects).GroupBy(s => s.Key);
            return allSubjects.ToDictionary(g => g.Key, g => g.Average(s => s.Value));
        }

        // Get top N performers by average grade
        public List<Student> GetTopPerformers(int count)
        {
            return Students.Values
                .Where(s => s.Subjects.Count > 0)
                .OrderByDescending(s => s.Subjects.Values.Average())
                .Take(count)
                .ToList();
        }
    }
}
