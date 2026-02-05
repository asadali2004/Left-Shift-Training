using System;
using System.Collections.Generic;

namespace StudentGradeManagement
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string GradeLevel { get; set; }  // 9th/10th/11th/12th
        public Dictionary<string, double> Subjects { get; set; }  // Subject->Grade
    }
}
