using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Threading.Tasks;

namespace fileio
{
    public class AsyncFileOperations
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyAsyncFileExamples");
        private static readonly string asyncTextFile = Path.Combine(BaseDirectory, "async_text.txt");
        private static readonly string asyncBinaryFile = Path.Combine(BaseDirectory, "async_binary.bin");

        public static async Task RunExamplesAsync() // Note the 'async Task'
        {
            Console.WriteLine($"\n--- Asynchronous File Operations ---");
            EnsureBaseDirectory();

            // --- Asynchronous Writing Text ---
            Console.WriteLine($"\nWriting text asynchronously to '{asyncTextFile}'...");
            // FileStreamOptions allows specifying FileOptions directly
            await using (StreamWriter writer = new StreamWriter(new FileStream(asyncTextFile, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous)))
            {
                await writer.WriteLineAsync("This is the first line written asynchronously.");
                await writer.WriteLineAsync("This is the second line written asynchronously.");
                await writer.FlushAsync(); // Ensures all buffered data is written to the underlying stream
            }
            Console.WriteLine("Finished writing text asynchronously.");

            // --- Asynchronous Reading Text ---
            Console.WriteLine($"\nReading text asynchronously from '{asyncTextFile}'...");
            using (StreamReader reader = new StreamReader(new FileStream(asyncTextFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous)))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null) // Note ReadLineAsync
                {
                    Console.WriteLine($"> {line}");
                }
            }
            Console.WriteLine("Finished reading text asynchronously.");

            // --- Asynchronous Writing Binary ---
            Console.WriteLine($"\nWriting binary data asynchronously to '{asyncBinaryFile}'...");
            byte[] dataToWrite = Encoding.UTF8.GetBytes("Async Binary Data Test!");
            await using (FileStream fs = new FileStream(asyncBinaryFile, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous))
            {
                await fs.WriteAsync(dataToWrite, 0, dataToWrite.Length); // Note WriteAsync
            }
            Console.WriteLine("Finished writing binary data asynchronously.");

            // --- Asynchronous Reading Binary ---
            Console.WriteLine($"\nReading binary data asynchronously from '{asyncBinaryFile}'...");
            byte[] buffer = new byte[100]; // A buffer to read into
            await using (FileStream fs = new FileStream(asyncBinaryFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous))
            {
                int bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length); // Note ReadAsync
                Console.WriteLine($"Read {bytesRead} bytes: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}");
            }
            Console.WriteLine("Finished reading binary data asynchronously.");

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
