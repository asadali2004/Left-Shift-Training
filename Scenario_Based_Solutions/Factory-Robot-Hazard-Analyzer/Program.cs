namespace Q1_Factory_Robot_Hazar_Analyzer
{
	/// <summary>
	/// Custom exception for robot safety validation failures
	/// </summary>
	public class RobotSafetyException : Exception
	{
		public RobotSafetyException(string message) : base(message)
		{
		}
	}

	/// <summary>
	/// Auditor class to calculate hazard risk for factory robots
	/// </summary>
	public class RobotHazardAuditor
	{
		/// <summary>
		/// Calculates hazard risk score based on arm precision, worker density, and machinery state
		/// </summary>
		/// <param name="armPrecision">Robot arm precision (0.0 to 1.0)</param>
		/// <param name="workerDensity">Number of workers nearby (1 to 20)</param>
		/// <param name="machineryState">State of machinery: Worn, Faulty, or Critical</param>
		/// <returns>Calculated hazard risk score</returns>
		public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
		{
			// Validate arm precision is within acceptable range
			if (armPrecision < 0.0 || armPrecision > 1.0)
			{
				throw new RobotSafetyException("Error:  Arm precision must be 0.0-1.0");
			}

			// Validate worker density is within acceptable range
			if (workerDensity < 1 || workerDensity > 20)
			{
				throw new RobotSafetyException("Error: Worker density must be 1-20");
			}

			// Determine machine risk factor based on machinery state
			double machineRiskFactor = machineryState switch
			{
				"Worn" => 1.3,        // Low risk factor
				"Faulty" => 2.0,      // Medium risk factor
				"Critical" => 3.0,    // High risk factor
				_ => throw new RobotSafetyException("Error: Unsupported machinery state")
			};

			// Calculate hazard risk: lower precision + higher density = higher risk
			return ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
		}
	}

	/// <summary>
	/// Main program to run the Factory Robot Hazard Analyzer
	/// </summary>
	public class Program
	{
		public static void Main(string[] args)
		{
			// Prompt and read arm precision input
			Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
			double armPrecision = double.Parse(Console.ReadLine() ?? "0");

			// Prompt and read worker density input
			Console.WriteLine("Enter Worker Density (1 - 20):");
			int workerDensity = int.Parse(Console.ReadLine() ?? "0");

			// Prompt and read machinery state input
			Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
			string machineryState = Console.ReadLine() ?? string.Empty;

			// Create auditor instance
			RobotHazardAuditor auditor = new RobotHazardAuditor();

			try
			{
				// Calculate and display hazard risk score
				double risk = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);
				Console.WriteLine($"Robot Hazard Risk Score: {risk}");
			}
			catch (RobotSafetyException ex)
			{
				// Display validation error message
				Console.WriteLine(ex.Message);
			}
		}
	}
}
