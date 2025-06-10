using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileio
{
    public class TextFileOperationsReader
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyTextFileExamples");
        private static readonly string filePath = Path.Combine(BaseDirectory, "mytextfile.txt");
        private static readonly string appendFilePath = Path.Combine(BaseDirectory, "appendfile.txt");

        public static void RunReadExamples()
        {
            Console.WriteLine($"\n--- Reading Text Files ---");
            EnsureBaseDirectory(); // Make sure files exist for reading

            // Ensure content is in the files for reading demonstrations
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Line 1 from ReadExample\nLine 2 from ReadExample\nLine 3 from ReadExample");
            }
            if (!File.Exists(appendFilePath))
            {
                File.WriteAllText(appendFilePath, "Initial content for append read.");
            }

            // --- Reading line by line ---
            Console.WriteLine($"\nReading '{filePath}' line by line:");
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) // ReadLine returns null at end of file
                {
                    Console.WriteLine($"> {line}");
                }
            }
            Console.WriteLine("Finished reading line by line.");

            // --- Reading the entire content ---
            Console.WriteLine($"\nReading all content from '{appendFilePath}':");
            using (StreamReader reader = new StreamReader(appendFilePath))
            {
                string allContent = reader.ReadToEnd(); // Reads everything to the end
                Console.WriteLine(allContent);
            }
            Console.WriteLine("Finished reading all content.");
        }

        private static void EnsureBaseDirectory()
        {
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
            }
        }
    }
}
