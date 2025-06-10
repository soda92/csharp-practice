using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileio
{
    using System;
    using System.IO;

    public class FileErrorHandling
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyErrorHandlingExamples");
        private static readonly string nonExistentFile = Path.Combine(BaseDirectory, "nonexistent.txt");
        private static string readOnlyFile = Path.Combine(BaseDirectory, "readonly.txt");
        private static readonly string inaccessiblePath = "C:\\Windows\\System32\\config\\"; // Restricted path

        public static void RunExamples()
        {
            Console.WriteLine($"\n--- File Error Handling ---");
            EnsureBaseDirectory();

            // Create a read-only file for demonstration
            string tempReadOnlyFile = Path.Combine(BaseDirectory, "temp_readonly.txt");
            File.WriteAllText(tempReadOnlyFile, "Some content.");
            File.SetAttributes(tempReadOnlyFile, FileAttributes.ReadOnly);
            readOnlyFile = tempReadOnlyFile; // Update path to the created one

            // 1. FileNotFoundException
            try
            {
                string content = File.ReadAllText(nonExistentFile);
                Console.WriteLine($"Content: {content}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"ERROR: FileNotFoundException for '{nonExistentFile}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // 2. UnauthorizedAccessException (attempting to write to read-only)
            try
            {
                Console.WriteLine($"\nAttempting to write to read-only file: '{readOnlyFile}'");
                File.WriteAllText(readOnlyFile, "Trying to write to read-only.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"ERROR: UnauthorizedAccessException for '{readOnlyFile}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // 3. UnauthorizedAccessException (accessing restricted directory)
            try
            {
                Console.WriteLine($"\nAttempting to list files in restricted directory: '{inaccessiblePath}'");
                string[] files = Directory.GetFiles(inaccessiblePath);
                Console.WriteLine($"Files found: {files.Length}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"ERROR: UnauthorizedAccessException for '{inaccessiblePath}': {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"ERROR: DirectoryNotFoundException for '{inaccessiblePath}': {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"ERROR: IOException for '{inaccessiblePath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // 4. IOException (e.g., file in use - more complex to simulate reliably without another process)
            // This is a common scenario.
            string lockedFile = Path.Combine(BaseDirectory, "lockedfile.txt");
            FileStream fs = null;
            try
            {
                Console.WriteLine($"\nAttempting to open and lock '{lockedFile}' for exclusive access.");
                fs = new FileStream(lockedFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None); // Lock file
                Console.WriteLine($"'{lockedFile}' is now locked.");
                Console.WriteLine("Attempting to delete the locked file from another stream...");
                File.Delete(lockedFile); // This will throw IOException because the file is locked
            }
            catch (IOException ex)
            {
                Console.WriteLine($"ERROR: IOException trying to delete locked file: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose(); // Release the file lock
                    Console.WriteLine($"File lock on '{lockedFile}' released.");
                }
            }

            // Now that the lock is released, we can delete it
            if (File.Exists(lockedFile))
            {
                File.Delete(lockedFile);
                Console.WriteLine($"Cleaned up '{lockedFile}'.");
            }


            // Cleanup: Reset attributes and delete the read-only file
            if (File.Exists(readOnlyFile))
            {
                File.SetAttributes(readOnlyFile, FileAttributes.Normal); // Remove read-only attribute
                File.Delete(readOnlyFile);
                Console.WriteLine($"Cleaned up '{readOnlyFile}'.");
            }

            // Final cleanup of the base directory
            if (Directory.Exists(BaseDirectory))
            {
                Directory.Delete(BaseDirectory, true);
                Console.WriteLine($"Cleaned up base directory: {BaseDirectory}");
            }
        }

        private static void EnsureBaseDirectory()
        {
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
                Console.WriteLine($"Created base directory: {BaseDirectory}");
            }
        }
    }
}
