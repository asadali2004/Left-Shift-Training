using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace StudentAjaxDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nothing needed here for now
        }

        // ── Web Method 1: Search by ID ──────────────────────────────
        [WebMethod]
        public static string SearchStudent(int studentId)
        {
            Student student = StudentRepository.GetById(studentId);

            if (student == null)
                return "NOT_FOUND";  // Special signal to JavaScript

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(student);  // Return as JSON string
        }

        // ── Web Method 2: Get All Students ──────────────────────────
        [WebMethod]
        public static string GetAllStudents()
        {
            List<Student> students = StudentRepository.GetAll();

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(students);
        }
    }
}