using System;

class BonusCalculator
{
    static void Main()
    {
        int[] salaries = { 5000, 10, 7000 };
        double bonus = 1000.0; // default bonus if not provided

        // TODO:
        // 1. Loop through salaries
        // 2. Divide bonus by salary
        // 3. Handle DivideByZeroException
        // 4. Continue processing remaining employees
        for(int i=0; i<salaries.Length; i++)
        {
            try
            {
                double ratio = bonus / salaries[i];
                Console.WriteLine($"Employee {i + 1}: Bonus ratio = {ratio:F2}");
            }catch(DivideByZeroException ex)
            {
                System.Console.WriteLine(ex.Message);
                continue;
            }
        }
    }
}