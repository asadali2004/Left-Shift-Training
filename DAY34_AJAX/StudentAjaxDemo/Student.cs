using System.Collections.Generic;
using System.Linq;

namespace StudentAjaxDemo
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int Age { get; set; }
    }

    // Fake database — replace with real DB later
    public static class StudentRepository
    {
        public static List<Student> GetAll()
        {
            return new List<Student>
            {
                new Student { Id=1, Name="Alice Johnson", Email="alice@mail.com", Course="Computer Science", Age=20 },
                new Student { Id=2, Name="Bob Smith",     Email="bob@mail.com",   Course="Mathematics",      Age=22 },
                new Student { Id=3, Name="Carol White",   Email="carol@mail.com", Course="Physics",          Age=21 },
                new Student { Id=4, Name="David Brown",   Email="david@mail.com", Course="Chemistry",        Age=23 },
                new Student { Id=5, Name="Eva Green",     Email="eva@mail.com",   Course="Computer Science", Age=20 }
            };
        }

        public static Student GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }
    }
}