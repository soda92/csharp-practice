using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileio
{
    public class Writer
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyTextFileExamples");
        private static readonly string filePath = Path.Combine(BaseDirectory, "mytextfile.txt");
        private static readonly string appendFilePath = Path.Combine(BaseDirectory, "appendfile.txt");

        public static void RunExamples()
        {
            Console.WriteLine($"\n--- Text File Operations ---");
            EnsureBaseDirectory();

            // --- Writing to a file (creates or overwrites) ---
            Console.WriteLine($"\nWriting to '{filePath}'...");
            using (StreamWriter writer = new StreamWriter(filePath)) // Creates or overwrites by default
            {
                writer.WriteLine("Hello, C# File I/O!");
                writer.WriteLine("This is the second line.");
                writer.Write("This is a partial line."); // Doesn't add a newline
                writer.WriteLine(" And now it's complete.");
            }
            Console.WriteLine("Finished writing.");

            // --- Appending to a file ---
            Console.WriteLine($"\nAppending to '{appendFilePath}'...");
            // true in constructor means append mode; if file doesn't exist, it creates it
            using (StreamWriter appender = new StreamWriter(appendFilePath, true))
            {
                appender.WriteLine("This is the first line to append.");
                appender.WriteLine("This is the second line to append.");
            }
            Console.WriteLine("Finished appending (first time).");

            using (StreamWriter appender = new StreamWriter(appendFilePath, true))
            {
                appender.WriteLine("Another line appended later.");
            }
            Console.WriteLine("Finished appending (second time).");
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
