namespace StudentApp
{
	public class Student
	{
		public delegate void CheckResult();

		public int StudentId { get; set; }
		public string StudentName { get; set; } = string.Empty;
		public int StudentMark { get; set; }

		public override string ToString()
		{
			return $"Student's Id: {StudentId}, Student's Name: {StudentName}, Student's Mark: {StudentMark}";
		}

		public void PassStudent()
		{
			Console.WriteLine("Congratulations!!! You Pass");
		}

		public void FailedStudent()
		{
			Console.WriteLine("Oh! You Failed. Try Again");
		}

		public void CheckingResult(Student s)
		{
			CheckResult result = s.StudentMark >= 40 ? PassStudent : FailedStudent;
			Console.Write($"{s.StudentName}: ");
			result.Invoke();
		}

		public Action<string> PrintMessage => message => Console.WriteLine(message);

		public Func<int, string> GetGrade => mark =>
		{
			if (mark >= 90) return "O";
			if (mark >= 80) return "A";
			if (mark >= 60) return "B";
			if (mark >= 40) return "C";
			return "F";
		};

		public Predicate<Student> IsPassed => student => student.StudentMark >= 40;
	}
}