using System;

class Program
{
    static void Main()
    {
        var service = new ApplicantService();

        while (true)
        {
            Console.WriteLine("\n1. Add Applicant");
            Console.WriteLine("2. View All");
            Console.WriteLine("3. Search");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Exit");

            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        var applicant = ReadApplicant();
                        service.Add(applicant);
                        Console.WriteLine("Applicant added!");
                        break;

                    case "2":
                        foreach (var a in service.GetAll())
                            Print(a);
                        break;

                    case "3":
                        Console.Write("Enter ID: ");
                        var id = Console.ReadLine()!;
                        var found = service.GetById(id);
                        if (found == null) Console.WriteLine("Not found");
                        else Print(found);
                        break;

                    case "4":
                        Console.Write("Enter ID to update: ");
                        id = Console.ReadLine()!;
                        var updated = ReadApplicant();
                        service.Update(id, updated);
                        Console.WriteLine("Updated!");
                        break;

                    case "5":
                        Console.Write("Enter ID to delete: ");
                        id = Console.ReadLine()!;
                        service.Delete(id);
                        Console.WriteLine("Deleted!");
                        break;

                    case "6":
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static Applicant ReadApplicant()
    {
        return new Applicant
        {
            ApplicantId = Prompt("ID: "),
            Name = Prompt("Name: "),
            CurrentLocation = Prompt("Current Location: "),
            PreferredLocation = Prompt("Preferred Location: "),
            CoreCompetency = Prompt("Core Competency: "),
            PassingYear = int.Parse(Prompt("Passing Year: "))
        };
    }

    static string Prompt(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine()!;
    }

    static void Print(Applicant a)
    {
        Console.WriteLine($"{a.ApplicantId} | {a.Name} | {a.CoreCompetency} | {a.PassingYear}");
    }
}
