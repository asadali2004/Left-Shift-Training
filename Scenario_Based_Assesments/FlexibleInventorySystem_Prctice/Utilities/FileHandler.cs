using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlexibleInventorySystem_Practice.Models;

namespace FlexibleInventorySystem_Practice.Utilities
{
    /// <summary>
    /// Handles file operations for saving and loading inventory data
    /// </summary>
    public class FileHandler
    {
        private readonly string _filePath;

        public FileHandler(string filePath = "inventory.txt")
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Saves inventory report to file
        /// </summary>
        public bool SaveReport(string reportContent)
        {
            try
            {
                // Ensure directory exists
                string? directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                File.WriteAllText(_filePath, reportContent);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving report: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Loads content from file
        /// </summary>
        public string? LoadReport()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return null;

                return File.ReadAllText(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading report: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Appends content to file
        /// </summary>
        public bool AppendReport(string reportContent)
        {
            try
            {
                // Ensure directory exists
                string? directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                File.AppendAllText(_filePath, reportContent + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to report: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Clears the file
        /// </summary>
        public bool ClearFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing file: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        public bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }
}
