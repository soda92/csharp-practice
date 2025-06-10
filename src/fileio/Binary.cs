using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace fileio
{

    public class BinaryFileOperations
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyBinaryFileExamples");
        private static readonly string binaryFilePath = Path.Combine(BaseDirectory, "mybinarydata.bin");

        public static void RunExamples()
        {
            Console.WriteLine($"\n--- Binary File Operations ---");
            EnsureBaseDirectory();

            // --- Writing Binary Data ---
            Console.WriteLine($"\nWriting binary data to '{binaryFilePath}'...");
            using (FileStream fs = new FileStream(binaryFilePath, FileMode.Create)) // Create or overwrite
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(12345);         // int
                writer.Write(3.14159f);      // float
                writer.Write(true);          // bool
                writer.Write("Hello Binary World!"); // string (prefixed with length)
                writer.Write(new byte[] { 0x01, 0x02, 0x03, 0x04 }); // byte array
            }
            Console.WriteLine("Finished writing binary data.");

            // --- Reading Binary Data ---
            Console.WriteLine($"\nReading binary data from '{binaryFilePath}'...");
            using (FileStream fs = new FileStream(binaryFilePath, FileMode.Open)) // Open existing file
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int myInt = reader.ReadInt32();
                float myFloat = reader.ReadSingle();
                bool myBool = reader.ReadBoolean();
                string myString = reader.ReadString();
                byte[] myBytes = reader.ReadBytes(4); // Read 4 bytes

                Console.WriteLine($"Read Int: {myInt}");
                Console.WriteLine($"Read Float: {myFloat}");
                Console.WriteLine($"Read Bool: {myBool}");
                Console.WriteLine($"Read String: {myString}");
                Console.WriteLine($"Read Bytes: {BitConverter.ToString(myBytes)}"); // Hex representation
            }
            Console.WriteLine("Finished reading binary data.");

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
