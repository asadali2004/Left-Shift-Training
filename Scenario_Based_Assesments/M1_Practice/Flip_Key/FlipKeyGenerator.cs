namespace Q2_Flip_Key
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Enter the word");
			string input = Console.ReadLine() ?? string.Empty;

			string key = new Program().CleanseAndInvert(input);
			Console.WriteLine(string.IsNullOrEmpty(key) ? "Invalid Input" : $"The generated key is - {key}");
		}

		public string CleanseAndInvert(string input)
		{
			if (string.IsNullOrEmpty(input) || input.Length < 6)
			{
				return string.Empty;
			}

			foreach (char c in input)
			{
				if (!char.IsLetter(c))
				{
					return string.Empty;
				}
			}

			string lower = input.ToLower();
			List<char> filtered = new List<char>();

			foreach (char c in lower)
			{
				if (((int)c % 2) != 0)
				{
					filtered.Add(c);
				}
			}

			filtered.Reverse();

			for (int i = 0; i < filtered.Count; i++)
			{
				if (i % 2 == 0)
				{
					filtered[i] = char.ToUpper(filtered[i]);
				}
			}

			return new string(filtered.ToArray());
		}
	}
}
