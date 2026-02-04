namespace StudentApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Student helper = new Student();
			List<Student> students = new()
			{
				new Student { StudentId = 101, StudentName = "Asad", StudentMark = 89 },
				new Student { StudentId = 103, StudentName = "Ali", StudentMark = 68 },
				new Student { StudentId = 102, StudentName = "Harish", StudentMark = 74 },
				new Student { StudentId = 104, StudentName = "Vikas", StudentMark = 81 },
				new Student { StudentId = 105, StudentName = "Barshit", StudentMark = 35 }
			};

			Console.WriteLine("===================================================");
			foreach (var student in students)
			{
				Console.WriteLine(student);
			}

			Console.WriteLine("====================================================");
			var studentsMarkWise = students.OrderByDescending(s => s.StudentMark).ToList();
			foreach (var student in studentsMarkWise)
			{
				Console.WriteLine(student);
				helper.CheckingResult(student);
			}

			Console.WriteLine("====================================================");
			Action<string> print = helper.PrintMessage;
			print("Action: Printing a custom message");

			Console.WriteLine("====================================================");
			Func<int, string> gradeFunc = helper.GetGrade;
			foreach (var student in students)
			{
				Console.WriteLine($"{student.StudentName} Grade: {gradeFunc(student.StudentMark)}");
			}

			Console.WriteLine("====================================================");
			Predicate<Student> isPassed = helper.IsPassed;
			var passedStudents = students.FindAll(isPassed);
			Console.WriteLine("Passed Students:");
			foreach (var student in passedStudents)
			{
				Console.WriteLine(student);
			}
		}
	}
}