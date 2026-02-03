namespace Q3_Stream_Buzz
{
	public class CreatorStats
	{
		public string CreatorName { get; set; } = string.Empty;
		public double[] WeeklyLikes { get; set; } = Array.Empty<double>();
	}

	public class Program
	{
		public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();

		public static void Main(string[] args)
		{
			Program app = new Program();
			bool running = true;

			while (running)
			{
				Console.WriteLine("1. Register Creator");
				Console.WriteLine("2. Show Top Posts");
				Console.WriteLine("3. Calculate Average Likes");
				Console.WriteLine("4. Exit");
				Console.WriteLine("Enter your choice:");

				string choice = Console.ReadLine() ?? string.Empty;

				switch (choice)
				{
					case "1":
						Console.WriteLine("Enter Creator Name:");
						string name = Console.ReadLine() ?? string.Empty;

						Console.WriteLine("Enter weekly likes (Week 1 to 4):");
						double[] likes = new double[4];
						for (int i = 0; i < 4; i++)
						{
							likes[i] = double.Parse(Console.ReadLine() ?? "0");
						}

						app.RegisterCreator(new CreatorStats { CreatorName = name, WeeklyLikes = likes });
						Console.WriteLine("Creator registered successfully");
						Console.WriteLine();
						break;

					case "2":
						Console.WriteLine("Enter like threshold:");
						double threshold = double.Parse(Console.ReadLine() ?? "0");

						Dictionary<string, int> topCounts = app.GetTopPostCounts(EngagementBoard, threshold);
						if (topCounts.Count == 0)
						{
							Console.WriteLine("No top-performing posts this week");
						}
						else
						{
							foreach (var kvp in topCounts)
							{
								Console.WriteLine($"{kvp.Key} - {kvp.Value}");
							}
						}
						Console.WriteLine();
						break;

					case "3":
						double avg = app.CalculateAverageLikes();
						Console.WriteLine($"Overall average weekly likes: {avg}");
						Console.WriteLine();
						break;

					case "4":
						Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
						running = false;
						break;
				}
			}
		}

		public void RegisterCreator(CreatorStats record)
		{
			EngagementBoard.Add(record);
		}

		public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
		{
			Dictionary<string, int> result = new Dictionary<string, int>();

			foreach (var creator in records)
			{
				int count = 0;
				foreach (double like in creator.WeeklyLikes)
				{
					if (like >= likeThreshold)
					{
						count++;
					}
				}

				if (count > 0)
				{
					result[creator.CreatorName] = count;
				}
			}

			return result;
		}

		public double CalculateAverageLikes()
		{
			double total = 0;
			int count = 0;

			foreach (var creator in EngagementBoard)
			{
				foreach (double like in creator.WeeklyLikes)
				{
					total += like;
					count++;
				}
			}

			return count == 0 ? 0 : total / count;
		}
	}
}
