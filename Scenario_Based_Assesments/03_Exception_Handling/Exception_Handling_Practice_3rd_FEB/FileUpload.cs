using System;

public class InvalidFileExtensionException : Exception
{
    public InvalidFileExtensionException(string message) : base(message)
    {
    }
}

public class FileSizeExceededException : Exception
{
    public FileSizeExceededException(string message) : base(message)
    {
    }
}

class FileUpload
{
    static void Main()
    {
        string fileName = "report.exe";
        int fileSize = 8; // MB
        int maxFileSize = 5; // MB
        string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".txt" };

        try
        {
            ValidateFileUpload(fileName, fileSize, maxFileSize, allowedExtensions);
            Console.WriteLine("File uploaded successfully!");
        }
        catch (InvalidFileExtensionException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
        }
        catch (FileSizeExceededException ex)
        {
            Console.WriteLine($"Size Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    static void ValidateFileUpload(string fileName, int fileSize, int maxFileSize, string[] allowedExtensions)
    {
        // Validate file extension
        string extension = System.IO.Path.GetExtension(fileName);
        bool isValidExtension = false;

        foreach (string ext in allowedExtensions)
        {
            if (extension.ToLower() == ext)
            {
                isValidExtension = true;
                break;
            }
        }

        if (!isValidExtension)
        {
            throw new InvalidFileExtensionException($"File extension '{extension}' is not allowed.");
        }

        // Validate file size
        if (fileSize > maxFileSize)
        {
            throw new FileSizeExceededException($"File size {fileSize}MB exceeds maximum allowed size {maxFileSize}MB.");
        }
    }
}
