namespace Q1_Factory_Robot_Hazar_Analyzer
{
	public class RobotSafetyException : Exception
	{
		public RobotSafetyException(string message) : base(message)
		{
		}
	}

	public class RobotHazardAuditor
	{
		public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
		{
			if (armPrecision < 0.0 || armPrecision > 1.0)
			{
				throw new RobotSafetyException("Error:  Arm precision must be 0.0-1.0");
			}

			if (workerDensity < 1 || workerDensity > 20)
			{
				throw new RobotSafetyException("Error: Worker density must be 1-20");
			}

			double machineRiskFactor = machineryState switch
			{
				"Worn" => 1.3,
				"Faulty" => 2.0,
				"Critical" => 3.0,
				_ => throw new RobotSafetyException("Error: Unsupported machinery state")
			};

			return ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
			double armPrecision = double.Parse(Console.ReadLine() ?? "0");

			Console.WriteLine("Enter Worker Density (1 - 20):");
			int workerDensity = int.Parse(Console.ReadLine() ?? "0");

			Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
			string machineryState = Console.ReadLine() ?? string.Empty;

			RobotHazardAuditor auditor = new RobotHazardAuditor();

			try
			{
				double risk = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);
				Console.WriteLine($"Robot Hazard Risk Score: {risk}");
			}
			catch (RobotSafetyException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
