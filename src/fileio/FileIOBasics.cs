using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fileio
{
    public class FileIOBasics
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyIOExamples");

        public static void RunExamples()
        {
            Console.WriteLine($"Working directory for examples: {BaseDirectory}");

            // Ensure the base directory exists
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
                Console.WriteLine($"Created base directory: {BaseDirectory}");
            }

            // --- File Operations ---
            string filePath = Path.Combine(BaseDirectory, "example.txt");
            string newFilePath = Path.Combine(BaseDirectory, "renamed_example.txt");
            string copiedFilePath = Path.Combine(BaseDirectory, "copied_example.txt");

            // 1. Checking if a file exists
            Console.WriteLine($"\nDoes '{filePath}' exist? {File.Exists(filePath)}");

            // 2. Creating a file (can be empty)
            File.Create(filePath).Dispose(); // .Dispose() closes the handle immediately
            Console.WriteLine($"Created '{filePath}'");
            Console.WriteLine($"Does '{filePath}' exist now? {File.Exists(filePath)}");

            // 3. Getting file information
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"File Name: {fileInfo.Name}");
            Console.WriteLine($"Full Path: {fileInfo.FullName}");
            Console.WriteLine($"Size (bytes): {fileInfo.Length}");
            Console.WriteLine($"Creation Time: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Write Time: {fileInfo.LastWriteTime}");

            // 4. Copying a file
            File.Copy(filePath, copiedFilePath, true); // true for overwrite if exists
            Console.WriteLine($"Copied '{filePath}' to '{copiedFilePath}'");

            // 5. Moving/Renaming a file
            File.Move(copiedFilePath, newFilePath);
            Console.WriteLine($"Moved '{copiedFilePath}' to '{newFilePath}'");
            Console.WriteLine($"Does '{copiedFilePath}' exist after move? {File.Exists(copiedFilePath)}");
            Console.WriteLine($"Does '{newFilePath}' exist after move? {File.Exists(newFilePath)}");

            // 6. Deleting a file
            File.Delete(newFilePath);
            Console.WriteLine($"Deleted '{newFilePath}'");
            Console.WriteLine($"Does '{newFilePath}' exist after delete? {File.Exists(newFilePath)}");

            // Cleanup: Delete the original file we created earlier
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Cleaned up '{filePath}'");
            }


            // --- Directory Operations ---
            string subDirectoryPath = Path.Combine(BaseDirectory, "MySubFolder");
            string anotherSubDirectory = Path.Combine(BaseDirectory, "AnotherFolder");

            // 1. Checking if a directory exists
            Console.WriteLine($"\nDoes '{subDirectoryPath}' exist? {Directory.Exists(subDirectoryPath)}");

            // 2. Creating a directory
            Directory.CreateDirectory(subDirectoryPath);
            Console.WriteLine($"Created directory: '{subDirectoryPath}'");
            Console.WriteLine($"Does '{subDirectoryPath}' exist now? {Directory.Exists(subDirectoryPath)}");

            // 3. Moving/Renaming a directory
            Directory.Move(subDirectoryPath, anotherSubDirectory);
            Console.WriteLine($"Moved '{subDirectoryPath}' to '{anotherSubDirectory}'");

            // 4. Listing files and subdirectories
            // Create some dummy files in the new directory for listing
            File.WriteAllText(Path.Combine(anotherSubDirectory, "file1.txt"), "Content");
            File.WriteAllText(Path.Combine(anotherSubDirectory, "file2.log"), "More Content");
            Directory.CreateDirectory(Path.Combine(anotherSubDirectory, "NestedFolder"));

            Console.WriteLine($"\nFiles in '{anotherSubDirectory}':");
            foreach (string file in Directory.GetFiles(anotherSubDirectory))
            {
                Console.WriteLine($"- {Path.GetFileName(file)}");
            }

            Console.WriteLine($"\nSubdirectories in '{anotherSubDirectory}':");
            foreach (string dir in Directory.GetDirectories(anotherSubDirectory))
            {
                Console.WriteLine($"- {Path.GetFileName(dir)}");
            }

            // Using DirectoryInfo for more info (similar to FileInfo)
            DirectoryInfo dirInfo = new DirectoryInfo(anotherSubDirectory);
            Console.WriteLine($"\nDirectory Name: {dirInfo.Name}");
            Console.WriteLine($"Full Path: {dirInfo.FullName}");
            Console.WriteLine($"Parent Directory: {dirInfo.Parent.Name}");
            Console.WriteLine($"Root: {dirInfo.Root.Name}");

            // 5. Deleting a directory
            // To delete a non-empty directory, you must use the recursive parameter set to true.
            Directory.Delete(anotherSubDirectory, true);
            Console.WriteLine($"Deleted '{anotherSubDirectory}' (and its contents)");
            Console.WriteLine($"Does '{anotherSubDirectory}' exist after delete? {Directory.Exists(anotherSubDirectory)}");

            // Final cleanup of the base directory (optional)
            if (Directory.Exists(BaseDirectory))
            {
                Directory.Delete(BaseDirectory, true);
                Console.WriteLine($"Cleaned up base directory: {BaseDirectory}");
            }
        }
    }
}
