using System;
using System.Collections.Generic;

namespace _16_E_Learning_Platform
{
    // Represents a course
    public class Course
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Instructor { get; set; }
        public int DurationWeeks { get; set; }
        public double Price { get; set; }
        public List<string> Modules { get; set; } = new List<string>();
    }

    // Represents enrollment
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string StudentId { get; set; }
        public string CourseCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public double ProgressPercentage { get; set; }
    }

    // Tracks module-level progress
    public class StudentProgress
    {
        public string StudentId { get; set; }
        public string CourseCode { get; set; }
        public Dictionary<string, double> ModuleScores { get; set; }
            = new Dictionary<string, double>();

        public DateTime LastAccessed { get; set; }
    }
}
