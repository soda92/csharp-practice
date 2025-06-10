using System;
using System.IO; // Essential namespace for File I/O

// To run the above example, put this in your Main method
namespace fileio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //FileIOBasics.RunExamples();
            //TextFileOperationsReader.RunReadExamples();
            //Writer.RunExamples();
            //ConvenienceFileMethods.RunExamples();
            //BinaryFileOperations.RunExamples();

            //await AsyncFileOperations.RunExamplesAsync();

            FileErrorHandling.RunExamples();

            Console.WriteLine("\nFile I/O Basics example completed.");
            Console.ReadKey();
        }
    }
}
