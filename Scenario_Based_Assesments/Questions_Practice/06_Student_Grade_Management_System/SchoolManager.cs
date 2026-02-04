using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeManagement
{
    public class SchoolManager
    {
        // TODO: Add collection to store students
        
        public void AddStudent(string name, string gradeLevel)
        {
            // TODO: Auto-generate StudentId
        }

        public void AddGrade(int studentId, string subject, double grade)
        {
            // TODO: Adds grade for student (0-100 scale)
        }

        public SortedDictionary<string, List<Student>> GroupStudentsByGradeLevel()
        {
            // TODO: Groups students by grade level
            return new SortedDictionary<string, List<Student>>();
        }

        public double CalculateStudentAverage(int studentId)
        {
            // TODO: Returns student's average grade
            return 0;
        }

        public Dictionary<string, double> CalculateSubjectAverages()
        {
            // TODO: Returns average grade per subject across all students
            return new Dictionary<string, double>();
        }

        public List<Student> GetTopPerformers(int count)
        {
            // TODO: Returns top N students by average grade
            return new List<Student>();
        }
    }
}
