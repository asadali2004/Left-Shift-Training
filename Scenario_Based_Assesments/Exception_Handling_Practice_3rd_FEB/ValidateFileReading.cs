using System;
using System.IO;

class FileReader
{
    static void Main()
    {
        string filePath = "data.txt";

        // TODO:
        // 1. Read file content
        // 2. Handle FileNotFoundException
        // 3. Handle UnauthorizedAccessException
        // 4. Ensure resource is closed properly

        try
        {
            // File.ReadAllText handles opening/closing internally
            string content = File.ReadAllText(filePath);
            Console.WriteLine(content);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File not found: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Access denied: " + ex.Message);
        }
        
    }
}
