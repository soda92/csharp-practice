using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
namespace fileio
{
    public class ConvenienceFileMethods
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyConvenienceExamples");
        private static readonly string singleTextFile = Path.Combine(BaseDirectory, "single_text.txt");
        private static readonly string linesFile = Path.Combine(BaseDirectory, "multiple_lines.txt");
        private static readonly string appendLinesFile = Path.Combine(BaseDirectory, "append_lines.txt");

        public static void RunExamples()
        {
            Console.WriteLine($"\n--- Convenience File Methods ---");
            EnsureBaseDirectory();

            // --- WriteAllText (creates or overwrites) ---
            File.WriteAllText(singleTextFile, "This is a single line of text written with WriteAllText.");
            Console.WriteLine($"Wrote to '{singleTextFile}' using WriteAllText.");

            // --- ReadAllText ---
            string content = File.ReadAllText(singleTextFile);
            Console.WriteLine($"Read from '{singleTextFile}':\n{content}");

            // --- WriteAllLines (creates or overwrites) ---
            string[] linesToWrite = {
            "First line.",
            "Second line.",
            "Third line."
            };
            File.WriteAllLines(linesFile, linesToWrite);
            Console.WriteLine($"Wrote {linesToWrite.Length} lines to '{linesFile}' using WriteAllLines.");

            // --- ReadAllLines ---
            string[] linesRead = File.ReadAllLines(linesFile);
            Console.WriteLine($"Read {linesRead.Length} lines from '{linesFile}':");
            foreach (string line in linesRead)
            {
                Console.WriteLine($"- {line}");
            }

            // --- AppendAllText ---
            File.AppendAllText(singleTextFile, "\nThis text was appended with AppendAllText.");
            Console.WriteLine($"Appended text to '{singleTextFile}'. New content:\n{File.ReadAllText(singleTextFile)}");

            // --- AppendAllLines ---
            string[] moreLinesToAppend = {
            "Fourth line appended.",
            "Fifth line appended."
            };
            File.AppendAllLines(appendLinesFile, moreLinesToAppend); // Creates if not exists
            Console.WriteLine($"Appended {moreLinesToAppend.Length} lines to '{appendLinesFile}'. New content:");
            File.AppendAllLines(appendLinesFile, new string[] { "Sixth line appended." }); // Append again
            foreach (string line in File.ReadAllLines(appendLinesFile))
            {
                Console.WriteLine($"- {line}");
            }

            // Cleanup
            Directory.Delete(BaseDirectory, true);
            Console.WriteLine($"Cleaned up base directory: {BaseDirectory}");
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
