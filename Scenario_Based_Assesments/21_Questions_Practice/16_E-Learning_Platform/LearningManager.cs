using System;
using System.Collections.Generic;
using System.Linq;

namespace _16_E_Learning_Platform
{
    public class LearningManager
    {
        public Dictionary<string, Course> Courses = new Dictionary<string, Course>();
        public List<Enrollment> Enrollments = new List<Enrollment>();
        public List<StudentProgress> ProgressRecords = new List<StudentProgress>();

        private int nextEnrollmentId = 1;

        // Add course
        public void AddCourse(string code, string name, string instructor,
                              int weeks, double price, List<string> modules)
        {
            Courses[code] = new Course
            {
                CourseCode = code,
                CourseName = name,
                Instructor = instructor,
                DurationWeeks = weeks,
                Price = price,
                Modules = modules
            };
        }

        // Enroll student
        public bool EnrollStudent(string studentId, string courseCode)
        {
            if (!Courses.ContainsKey(courseCode))
                return false;

            if (Enrollments.Any(e => e.StudentId == studentId && e.CourseCode == courseCode))
                return false;

            Enrollments.Add(new Enrollment
            {
                EnrollmentId = nextEnrollmentId++,
                StudentId = studentId,
                CourseCode = courseCode,
                EnrollmentDate = DateTime.Now,
                ProgressPercentage = 0
            });

            ProgressRecords.Add(new StudentProgress
            {
                StudentId = studentId,
                CourseCode = courseCode,
                LastAccessed = DateTime.Now
            });

            return true;
        }

        // Update progress
        public bool UpdateProgress(string studentId, string courseCode,
                                   string module, double score)
        {
            var course = Courses.GetValueOrDefault(courseCode);
            if (course == null || !course.Modules.Contains(module))
                return false;

            var progress = ProgressRecords
                .FirstOrDefault(p => p.StudentId == studentId && p.CourseCode == courseCode);

            if (progress == null)
                return false;

            progress.ModuleScores[module] = score;
            progress.LastAccessed = DateTime.Now;

            // Update enrollment percentage
            var enrollment = Enrollments
                .First(e => e.StudentId == studentId && e.CourseCode == courseCode);

            enrollment.ProgressPercentage =
                (progress.ModuleScores.Count * 100.0) / course.Modules.Count;

            return true;
        }

        // Group courses by instructor
        public Dictionary<string, List<Course>> GroupCoursesByInstructor()
        {
            return Courses.Values
                          .GroupBy(c => c.Instructor)
                          .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Top performers by average module score
        public List<Enrollment> GetTopPerformingStudents(string courseCode, int count)
        {
            var ranked = ProgressRecords
                .Where(p => p.CourseCode == courseCode && p.ModuleScores.Any())
                .Select(p => new
                {
                    Enrollment = Enrollments.First(e =>
                        e.StudentId == p.StudentId && e.CourseCode == courseCode),
                    AvgScore = p.ModuleScores.Values.Average()
                })
                .OrderByDescending(x => x.AvgScore)
                .Take(count)
                .Select(x => x.Enrollment)
                .ToList();

            return ranked;
        }
    }
}
